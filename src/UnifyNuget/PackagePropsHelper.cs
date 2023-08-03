using System.Xml;
using System.Xml.Serialization;

namespace UnifyNuget;

public class PackagePropsHelper
{
    private Project _project = new();

    public void Build(IEnumerable<PackageVersion> nugetPackages)
    {
        var nugetPackagesMaxVersions = nugetPackages
            .GroupBy(s => s.Include)
            .Select(g => g.MaxBy(np => new SematicVersion(np.Version)))
            .OrderBy(s => s.Include)
            .ToList();

        if (nugetPackagesMaxVersions.Any())
        {
            Console.WriteLine("No packages found in csproj files.");
        }

        var project = new Project();
        project.PropertyGroup.ManagePackageVersionsCentrally = true;
        project.ItemGroup.PackageVersion.AddRange(nugetPackagesMaxVersions!);

        _project = project;
    }

    public void Write(string propsFileName)
    {
        // Remove Declaration  
        var settings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = true
        };

        // Remove Namespace  
        var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
        using var stream = new StringWriter();
        using var writer = XmlWriter.Create(stream, settings);
        var serializer = new XmlSerializer(typeof(Project));
        serializer.Serialize(writer, _project, ns);
        File.WriteAllText(propsFileName, stream.ToString());
    }
}