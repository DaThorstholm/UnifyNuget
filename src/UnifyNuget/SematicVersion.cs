namespace UnifyNuget;

public class SematicVersion : Comparer<SematicVersion>, IComparable
{
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }
    public int Prerelease { get; set; }

    public SematicVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            throw new ArgumentException($"{nameof(version)} is null or empty");
        }

        var versionSplit = version.Split('.');

        if (int.TryParse(versionSplit[0], out var major))
        {
            Major = major;
        }

        if (versionSplit.Length > 1 && int.TryParse(versionSplit[1], out var minor))
        {
            Minor = minor;
        }

        if (versionSplit.Length > 2 && int.TryParse(versionSplit[2], out var patch))
        {
            Patch = patch;
        }

        if (versionSplit.Length > 3 && int.TryParse(versionSplit[3], out var preRelease))
        {
            Prerelease = preRelease;
        }
    }

    public override int Compare(SematicVersion? x, SematicVersion? y)
    {
        if (x.Major.CompareTo(y.Major) != 0)
        {
            return x.Major.CompareTo(y.Major);
        }

        if (x.Minor.CompareTo(y.Minor) != 0)
        {
            return x.Minor.CompareTo(y.Minor);
        }

        if (x.Patch.CompareTo(y.Patch) != 0)
        {
            return x.Patch.CompareTo(y.Patch);
        }

        if (x.Prerelease.CompareTo(y.Prerelease) != 0)
        {
            return x.Prerelease.CompareTo(y.Prerelease);
        }

        return 0;
    }

    public int CompareTo(object? obj)
    {
        return Compare(this, (SematicVersion) obj!);
    }
}