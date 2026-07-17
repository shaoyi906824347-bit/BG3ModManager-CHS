using System.Text.RegularExpressions;

namespace DivinityModManager.Models.App;

public readonly record struct LocalizationReleaseVersion(Version BaseVersion, int Revision) : IComparable<LocalizationReleaseVersion>
{
	private static readonly Regex ReleaseTagPattern = new(
		@"^v?(?<version>\d+\.\d+\.\d+\.\d+)-zh-cn(?:-r(?<revision>\d+))?$",
		RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

	public static bool TryParseTag(string tag, out LocalizationReleaseVersion release)
	{
		release = default;
		if (String.IsNullOrWhiteSpace(tag)) return false;

		var match = ReleaseTagPattern.Match(tag.Trim());
		if (!match.Success || !Version.TryParse(match.Groups["version"].Value, out var version)) return false;

		var revision = 1;
		if (match.Groups["revision"].Success && !Int32.TryParse(match.Groups["revision"].Value, out revision)) return false;
		if (revision < 1) return false;

		release = new LocalizationReleaseVersion(version, revision);
		return true;
	}

	public int CompareTo(LocalizationReleaseVersion other)
	{
		var versionComparison = BaseVersion.CompareTo(other.BaseVersion);
		return versionComparison != 0 ? versionComparison : Revision.CompareTo(other.Revision);
	}
}
