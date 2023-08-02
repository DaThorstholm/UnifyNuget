using System.Xml.Serialization;

namespace UnifyNuget;

[XmlRoot(ElementName = "PropertyGroup")]
public class PropertyGroup
{
    [XmlElement(ElementName = "ManagePackageVersionsCentrally")]
    public bool ManagePackageVersionsCentrally { get; set; }
}

[XmlRoot(ElementName = "PackageVersion")]
public class PackageVersion
{
    [XmlAttribute(AttributeName = "Include")]
    public string Include { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public string Version { get; set; }
}

[XmlRoot(ElementName = "ItemGroup")]
public class ItemGroup
{
    [XmlElement(ElementName = "PackageVersion")]
    public List<PackageVersion> PackageVersion { get; set; } = new();
}

[XmlRoot(ElementName = "Project")]
public class Project
{
    [XmlElement(ElementName = "PropertyGroup")]
    public PropertyGroup PropertyGroup { get; set; } = new();

    [XmlElement(ElementName = "ItemGroup")]
    public ItemGroup ItemGroup { get; set; } = new();
}