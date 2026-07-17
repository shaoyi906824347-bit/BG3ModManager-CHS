using DivinityModManager.Util;

namespace BG3ModManager.Tests;

public class FileOperationTests
{
	[Fact]
	public async Task TempFileCopiesFromSourcePathAndUsesUniqueNames()
	{
		var testDirectory = Path.Combine(Path.GetTempPath(), "BG3MMTests", Guid.NewGuid().ToString("N"));
		Directory.CreateDirectory(testDirectory);
		var sourcePath = Path.Combine(testDirectory, "same-name.pak");
		var expected = new byte[] { 9, 8, 7, 6 };
		await File.WriteAllBytesAsync(sourcePath, expected);

		try
		{
			using var first = await TempFile.CreateAsync(sourcePath, CancellationToken.None);
			using var second = await TempFile.CreateAsync(sourcePath, CancellationToken.None);

			Assert.NotEqual(first.FilePath, second.FilePath);
			Assert.Equal(0, first.Stream.Position);
			var actual = new byte[expected.Length];
			Assert.Equal(expected.Length, await first.Stream.ReadAsync(actual));
			Assert.Equal(expected, actual);
		}
		finally
		{
			Directory.Delete(testDirectory, true);
		}
	}

	[Fact]
	public async Task TempFileStartsAtBeginningAfterCopy()
	{
		var expected = new byte[] { 1, 2, 3, 4, 5 };
		await using var source = new MemoryStream(expected);
		using var temp = await TempFile.CreateAsync("position-test.pak", source, CancellationToken.None);

		Assert.Equal(0, temp.Stream.Position);
		var actual = new byte[expected.Length];
		var bytesRead = await temp.Stream.ReadAsync(actual);
		Assert.Equal(expected.Length, bytesRead);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task CancelledCopyDoesNotLeavePartialDestinationFile()
	{
		var testDirectory = Path.Combine(Path.GetTempPath(), "BG3MMTests", Guid.NewGuid().ToString("N"));
		Directory.CreateDirectory(testDirectory);
		var sourcePath = Path.Combine(testDirectory, "source.pak");
		var destinationPath = Path.Combine(testDirectory, "destination.pak");
		await File.WriteAllBytesAsync(sourcePath, new byte[1024]);
		using var cancellation = new CancellationTokenSource();
		cancellation.Cancel();

		try
		{
			Assert.False(await DivinityFileUtils.CopyFileAsync(sourcePath, destinationPath, cancellation.Token));
			Assert.False(File.Exists(destinationPath));
		}
		finally
		{
			Directory.Delete(testDirectory, true);
		}
	}
}
