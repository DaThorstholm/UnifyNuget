using UnifyNuget;

var propsFileName = "Directory.Packages.props";

Console.WriteLine("UnifyNuget started");
Console.WriteLine();
Console.WriteLine($"This program will create a new file in the root directory called {propsFileName}");
Console.WriteLine("Afterwards it will update all csproj files to remove the nuget package version numbers");
Console.WriteLine("The program must be run from the root folder of the project");
Console.WriteLine("Do you want to continue? (Y)es or press any other key to cancel");
var doMagic = Console.ReadKey();
Console.WriteLine();

if (doMagic.Key != ConsoleKey.Y)
{
    Console.WriteLine("UnifyNuget cancelled");
    
    return;
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
    
    return;
}

var packageVersions = csProjHelper.GetAllPackageVersions();
if (!packageVersions.Any())
{
    Console.WriteLine("Didn't find any nuget packages in csproj files");
    
    return;
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