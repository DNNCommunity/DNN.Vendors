using System;
using System.Linq;
using Microsoft.Build.Tasks;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

    string ModuleName => "Dnn.Modules.Vendors";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath DnnBinDirectory => RootDirectory.Parent.Parent / "bin";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoRestore());
        });

    Target Deploy => _ => _
        .DependsOn(Compile)
        .OnlyWhenDynamic(() => DnnBinDirectory.Exists())
        .Executes(() =>
        {
            var assembly = RootDirectory / "bin" / Configuration / $"{ModuleName}.dll";
            CopyFileToDirectory(assembly, DnnBinDirectory, FileExistsPolicy.Overwrite);
        });

    Target Package => _ => _
        .DependsOn(Clean)
        .DependsOn(Compile)
        .Executes(() => {
            var resourcesDir = ArtifactsDirectory / "Resources";
            var resourceFiles = GlobFiles(RootDirectory, "*.txt", "*.ascx", "*.aspx", "*.png", "*.css");
            resourceFiles.ForEach(file => CopyFileToDirectory(file, resourcesDir, FileExistsPolicy.Overwrite, createDirectories: true));

            var localizationFiles = GlobFiles(RootDirectory / "App_LocalResources", "*.*");
            localizationFiles.ForEach(file => CopyFileToDirectory(file, resourcesDir / "App_LocalResources", FileExistsPolicy.Overwrite, createDirectories: true));

            CompressionTasks.CompressZip(resourcesDir, ArtifactsDirectory / "Resources.zip");
            DeleteDirectory(resourcesDir);

            var assembly = RootDirectory / "bin" / Configuration / $"{ModuleName}.dll";
            CopyFileToDirectory(assembly, ArtifactsDirectory / "bin");

            CopyDirectoryRecursively(RootDirectory / "Providers" / "DataProviders" / "SqlDataProvider",
                ArtifactsDirectory / "Providers" / "DataProviders" / "SqlDataProvider");

            var txtFiles = GlobFiles(RootDirectory / "*.txt");
            txtFiles.ForEach(file => CopyFileToDirectory(file, ArtifactsDirectory));

            var symbols = GlobFiles(RootDirectory / "bin" / Configuration, $"{ModuleName}.pdb");
            symbols.ForEach(file => CopyFileToDirectory(file, ArtifactsDirectory / "Symbols", FileExistsPolicy.Overwrite, createDirectories: true));
            CompressionTasks.CompressZip(ArtifactsDirectory / "Symbols", ArtifactsDirectory / "Symbols.zip");
            DeleteDirectory(ArtifactsDirectory / "Symbols");
        });
}
