#addin nuget:?package=SharpZipLib
#addin nuget:?package=Cake.Compression


///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var version = "";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
.Does(()=>{
	CleanDirectory("./Install");
	CleanDirectory("./Install/Package");
	CleanDirectory("./Install/Package/bin");
	CleanDirectory("./Install/Package/Providers/DataProviders/SqlDataProvider");
});

Task("GetInfo")
.Does(() => {
	version = XmlPeek("./dnn_Vendors.dnn", "dotnetnuke/packages/package[1]/@version");
	Information("Version is: " + version);
});

Task("ReleasePackage")
.IsDependentOn("Clean")
.IsDependentOn("GetInfo")
.Does(() => {	

	// GET THE RESOURCE FILES
	var resourceFiles = GetFiles("./*.ascx");
	resourceFiles.Add(GetFiles("./*.aspx"));
	resourceFiles.Add(GetFiles("./App_LocalResources/*.resx"));
	resourceFiles.Add(GetFiles("./*.css"));
	resourceFiles.Add(GetFiles("./*.txt"));
	resourceFiles.Add(GetFiles("./*.png"));
	foreach (var file in resourceFiles)
	{
		Information("Zipping resource file: " + file);
	}
	Zip("./", "./Install/Package/Resources.zip", resourceFiles);

	// GET THE SQL SCRIPTS
	var sqlScripts = GetFiles("./Providers/DataProviders/SqlDataProvider/*.SqlDataProvider");
	CopyFiles(sqlScripts, "./Install/Package/Providers/DataProviders/SqlDataProvider");

	// COPY THE BINARIES
	CopyFile("../../../../bin/Dnn.Modules.Vendors.dll", "./Install/Package/bin/Dnn.Modules.Vendors.dll");

	// COPY THE SYMBOLS
	var symbols = GetFiles("../../../../bin/Dnn.Modules.Vendors.pdb");
	Zip("../../../../bin", "./Install/Package/Symbols.zip", symbols);

	// COPY RELEASE NOTES AND LICENSE
	CopyFiles("./*.txt", "./Install/Package");

	// COPY THE MANIFEST
	CopyFiles("./*.dnn", "./Install/Package");

	// ZIP THE PACKAGE
	Zip("./Install/Package", "./Install/" + "Dnn.Modules.Vendors_" + version + "_Install.zip");
	
	// CLEANUP TEMP FOLDER
	DeleteDirectory("./Install/Package", true);
});

Task("Default")
.IsDependentOn("ReleasePackage");

RunTarget(target);