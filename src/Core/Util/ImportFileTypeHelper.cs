namespace DivinityModManager.Util;

public static class ImportFileTypeHelper
{
	public static readonly string[] ArchiveExtensions = [".7z", ".7zip", ".gzip", ".rar", ".tar", ".tar.gz", ".zip"];
	public static readonly string[] CompressedExtensions = [".bz2", ".xz", ".zst"];

	private static readonly string[] ImportExtensionsByLength = ArchiveExtensions
		.Concat(CompressedExtensions)
		.Append(".pak")
		.OrderByDescending(x => x.Length)
		.ToArray();

	public static string GetExtension(string pathOrExtension)
	{
		if (String.IsNullOrWhiteSpace(pathOrExtension)) return String.Empty;

		var extension = ImportExtensionsByLength.FirstOrDefault(x => pathOrExtension.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		return extension ?? Path.GetExtension(pathOrExtension).ToLowerInvariant();
	}

	public static bool IsImportable(string pathOrExtension)
	{
		var extension = GetExtension(pathOrExtension);
		return extension == ".pak" || ArchiveExtensions.Contains(extension) || CompressedExtensions.Contains(extension);
	}
}
