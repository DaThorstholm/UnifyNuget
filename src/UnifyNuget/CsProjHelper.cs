using System.Xml;

namespace UnifyNuget;

public class CsProjHelper
{
    private List<CsProj> CsProjs { get; set; } = new();

    public bool FindAll()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var files = Directory.GetFiles(currentDirectory, "*.csproj", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var csProj = new CsProj();
            csProj.FilePath = file;
            csProj.FileContent = File.ReadAllText(file);

            CsProjs.Add(csProj);
        }

        return CsProjs.Any();
    }
    
    public IEnumerable<PackageVersion> GetAllPackageVersions()
    {
        var packageVersions = new List<PackageVersion>();

        foreach (var csProj in CsProjs)
        {
            var doc = new XmlDocument();
            doc.Load(csProj.FilePath);
            
            var elemList = doc.GetElementsByTagName("PackageReference");
            foreach (var element in elemList)
            {
                var xmlElement = (XmlElement)element;

                var packageVersion = new PackageVersion();
                packageVersion.Include = xmlElement.GetAttribute("Include");
                packageVersion.Version = xmlElement.GetAttribute("Version");

                if (string.IsNullOrWhiteSpace(packageVersion.Version) )
                {
                    continue;
                }
                
                packageVersions.Add(packageVersion);
            }
        }
        
        return packageVersions;
    }

    public void RemoveVersions()
    {
        foreach (var csProj in CsProjs)
        {
            var doc = new XmlDocument();
            doc.Load(csProj.FilePath);
            
            var elemList = doc.GetElementsByTagName("PackageReference");
            foreach (var element in elemList)
            {
                var xmlElement = (XmlElement)element;
                if (xmlElement?.GetAttribute("Version") is null)
                {
                    continue;
                }
                
                xmlElement.RemoveAttribute("Version");
            }
            
            doc.Save(csProj.FilePath);
        }
    }
}

public class CsProj
{
    public string FilePath { get; set; }
    public string FileContent { get; set; }
}