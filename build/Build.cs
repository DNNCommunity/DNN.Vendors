using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Extensions;
using Microsoft.Build.Tasks;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
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

[GitHubActions(
    "continous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push },
    InvokedTargets = new [] { nameof(Package) })]
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

    GitHubActions GitHubActions => GitHubActions.Instance;

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
    Target SetManifestVersions => _ => _
        .Executes(() =>
        {
            // Sets the module version
            var manifest = GlobFiles(RootDirectory, "*.dnn").SingleOrDefault();
            XDocument doc = XDocument.Load(manifest);
            var packages = doc.Descendants("package");
            foreach (var package in packages)
            {
                package.Attribute("version").Value = GitVersion.MajorMinorPatch;
            }

            // Sets the minimum DNN version
            var binary = GlobFiles(RootDirectory / "bin" / Configuration, $"{ModuleName}.dll").SingleOrDefault();
            var assembly = Assembly.LoadFile(binary);

            var referencedAssemblies = assembly.GetReferencedAssemblies();
            var dnnReference = referencedAssemblies.SingleOrDefault(a => a.Name == "DotNetNuke");
            var dependencies = doc.Descendants("dependency").Where(n => n.Attribute("type").Value == "coreVersion");
            foreach (var dependency in dependencies)
            {
                dependency.Value = dnnReference.Version.DnnMajorMinorPatch();
            }
            Serilog.Log.Information($"Updated DNN requirement for {dnnReference.Version.DnnMajorMinorPatch()}");

            doc.Save(manifest);
        });
        

    Target Compile => _ => _
        .DependsOn(Restore)
        .DependsOn(SetManifestVersions)
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
            var stagingDirectory = ArtifactsDirectory / "Staging";
            var resourcesDir = stagingDirectory / "Resources";
            var resourceFiles = GlobFiles(RootDirectory, "*.txt", "*.ascx", "*.aspx", "*.png", "*.css");
            resourceFiles.ForEach(file => CopyFileToDirectory(file, resourcesDir, FileExistsPolicy.Overwrite, createDirectories: true));

            var localizationFiles = GlobFiles(RootDirectory / "App_LocalResources", "*.*");
            localizationFiles.ForEach(file => CopyFileToDirectory(file, resourcesDir / "App_LocalResources", FileExistsPolicy.Overwrite, createDirectories: true));

            CompressionTasks.CompressZip(resourcesDir, stagingDirectory / "Resources.zip");
            DeleteDirectory(resourcesDir);

            var assembly = RootDirectory / "bin" / Configuration / $"{ModuleName}.dll";
            CopyFileToDirectory(assembly, stagingDirectory / "bin");

            CopyDirectoryRecursively(RootDirectory / "Providers" / "DataProviders" / "SqlDataProvider",
                stagingDirectory / "Providers" / "DataProviders" / "SqlDataProvider");

            var installFiles = GlobFiles(RootDirectory, "*.txt", "*.dnn");
            installFiles.ForEach(file => CopyFileToDirectory(file, stagingDirectory));

            var symbols = GlobFiles(RootDirectory / "bin" / Configuration, $"{ModuleName}.pdb");
            symbols.ForEach(file => CopyFileToDirectory(file, stagingDirectory / "Symbols", FileExistsPolicy.Overwrite, createDirectories: true));
            CompressionTasks.CompressZip(stagingDirectory / "Symbols", stagingDirectory / "Symbols.zip");
            DeleteDirectory(stagingDirectory / "Symbols");

            CompressionTasks.CompressZip(stagingDirectory, ArtifactsDirectory / $"{ModuleName}_v{GitVersion.MajorMinorPatch}.zip");
            DeleteDirectory(stagingDirectory);
        });
}
