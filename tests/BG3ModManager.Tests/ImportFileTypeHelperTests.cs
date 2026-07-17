using DivinityModManager.Util;

namespace BG3ModManager.Tests;

public class ImportFileTypeHelperTests
{
	[Theory]
	[InlineData(@"C:\Downloads\mods.tar.gz", ".tar.gz")]
	[InlineData(@"C:\Downloads\MOD.PAK", ".pak")]
	[InlineData(@"C:\Downloads\mod.zst", ".zst")]
	public void GetExtensionRecognizesSupportedCompoundAndCaseInsensitiveExtensions(string path, string expected)
	{
		Assert.Equal(expected, ImportFileTypeHelper.GetExtension(path));
		Assert.True(ImportFileTypeHelper.IsImportable(path));
	}

	[Fact]
	public void IsImportableRejectsUnknownFiles()
	{
		Assert.False(ImportFileTypeHelper.IsImportable(@"C:\Downloads\notes.txt"));
	}
}
