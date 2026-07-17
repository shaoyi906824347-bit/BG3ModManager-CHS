using DivinityModManager.Models.App;

namespace BG3ModManager.Tests;

public class LocalizationReleaseVersionTests
{
	[Theory]
	[InlineData("v1.0.12.9-zh-cn", 1)]
	[InlineData("v1.0.12.9-zh-cn-r2", 2)]
	[InlineData("1.0.12.9-ZH-CN-R15", 15)]
	public void TryParseTagReadsLocalizationRevision(string tag, int expectedRevision)
	{
		Assert.True(LocalizationReleaseVersion.TryParseTag(tag, out var release));
		Assert.Equal(new Version(1, 0, 12, 9), release.BaseVersion);
		Assert.Equal(expectedRevision, release.Revision);
	}

	[Fact]
	public void CompareToDetectsLocalizationOnlyUpdates()
	{
		var installed = new LocalizationReleaseVersion(new Version(1, 0, 12, 9), 2);
		var latest = new LocalizationReleaseVersion(new Version(1, 0, 12, 9), 3);

		Assert.True(latest.CompareTo(installed) > 0);
	}

	[Theory]
	[InlineData("")]
	[InlineData("v1.0.12.9")]
	[InlineData("v1.0.12.9-zh-cn-r0")]
	public void TryParseTagRejectsInvalidReleaseTags(string tag)
	{
		Assert.False(LocalizationReleaseVersion.TryParseTag(tag, out _));
	}
}
