using System.Globalization;
using System.Runtime.Serialization;

namespace DivinityModManager.Models.Extender;

[DataContract]
public class ScriptExtenderUpdateData
{
	[DataMember] public int ManifestMinorVersion { get; set; }
	[DataMember] public int ManifestVersion { get; set; }
	[DataMember] public string NoMatchingVersionNotice { get; set; }
	[DataMember] public List<ScriptExtenderUpdateResource> Resources { get; set; }
}

[DataContract]
public class ScriptExtenderUpdateResource
{
	[DataMember] public string Name { get; set; }
	[DataMember] public List<ScriptExtenderUpdateVersion> Versions { get; set; }
}

[DataContract]
public class ScriptExtenderUpdateVersion : ReactiveObject
{
	[DataMember][Reactive] public long BuildDate { get; set; }
	[DataMember][Reactive] public string Digest { get; set; }
	[DataMember][Reactive] public string MinGameVersion { get; set; }
	[DataMember][Reactive] public string Notice { get; set; }
	[DataMember][Reactive] public string URL { get; set; }
	[DataMember][Reactive] public string Version { get; set; }
	[DataMember][Reactive] public string Signature { get; set; }

	private readonly ObservableAsPropertyHelper<string> _displayName;
	public string DisplayName => _displayName.Value;

	private readonly ObservableAsPropertyHelper<string> _buildDateDate;
	public string BuildDateDisplayString => _buildDateDate.Value;

	private readonly ObservableAsPropertyHelper<bool> _isEmpty;
	public bool IsEmpty => _isEmpty.Value;

	private string TimestampToReadableString(long timestamp)
	{
		var date = DateTime.FromFileTime(timestamp);
		return date.ToString(DivinityApp.DateTimeExtenderBuildFormat, CultureInfo.InstalledUICulture);
	}

	private string ToDisplayName(ValueTuple<string, string, string> data)
	{
		if (String.IsNullOrEmpty(data.Item1)) return "最新版本";
		var result = data.Item1;
		if (!String.IsNullOrEmpty(data.Item2))
		{
			result += $" ({data.Item2})";
		}
		if (!String.IsNullOrEmpty(data.Item3))
		{
			result += $" - {data.Item3}";
		}
		return result;
	}

	public ScriptExtenderUpdateVersion()
	{
		_isEmpty = this.WhenAnyValue(x => x.Version).Select(x => String.IsNullOrEmpty(x)).ToProperty(this, nameof(IsEmpty), true, RxApp.MainThreadScheduler);
		_buildDateDate = this.WhenAnyValue(x => x.BuildDate).Select(TimestampToReadableString).ToProperty(this, nameof(BuildDateDisplayString), true, RxApp.MainThreadScheduler);
		_displayName = this.WhenAnyValue(x => x.Version, x => x.MinGameVersion, x => x.BuildDateDisplayString).Select(ToDisplayName).ToProperty(this, nameof(DisplayName), true, RxApp.MainThreadScheduler);
	}
}
