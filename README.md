# UnifyNuget
This program will create a new file in the root directory called Directory.Packages.props,  
Afterwards it will update all csproj files to remove the nuget package version numbers.  

## Installation
Go to the root folder and pack the program using `dotnet pack`  
Then run the command in `dotnet tool install --global --add-source <folder path to nuget package> unify`  

## Running the progam
`cd` to the project root folder that you want to unify.  
The program must be run from the root folder of the project using the command `unify`.  

[dotnet tools how to](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools-how-to-create)  
