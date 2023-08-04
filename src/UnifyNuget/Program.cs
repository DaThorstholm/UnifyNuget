using UnifyNuget;

var propsFileName = "Directory.Packages.props";

Splash.Print();
Console.WriteLine();
Console.WriteLine("Unify nuget packages from csproj files found in directory and subdirectories.");
Console.WriteLine("Do you want to continue? (Y)es or press any other key to cancel");
var doMagic = Console.ReadKey();
Console.WriteLine();

if (doMagic.Key != ConsoleKey.Y)
{
    Console.WriteLine("UnifyNuget cancelled");
    
    return -1;
}

if (File.Exists(propsFileName))
{
    throw new Exception($"File {propsFileName} already exists. Delete the file and run again");
}

// Get all nuget packages and versions
var csProjHelper = new CsProjHelper();
var foundCsProjs = csProjHelper.FindAll();
if (!foundCsProjs)
{
    Console.WriteLine("Didn't find any csproj files");
    
    return -1;
}

var packageVersions = csProjHelper.GetAllPackageVersions();
if (!packageVersions.Any())
{
    Console.WriteLine("Didn't find any nuget packages in csproj files");
    
    return -1;
}

// Create Directory.Packages.props
var packagePropsHelper = new PackagePropsHelper();
packagePropsHelper.Build(packageVersions);
packagePropsHelper.Write(propsFileName);

Console.WriteLine($"Finished creating {propsFileName}");

// Update all csproj files
csProjHelper.RemoveVersions();
Console.WriteLine("Updated csproj files");

Console.WriteLine("UnifyNuget Completed");

return 0;