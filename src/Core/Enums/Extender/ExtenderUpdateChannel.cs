using Newtonsoft.Json.Converters;

using System.ComponentModel;

namespace DivinityModManager;

[JsonConverter(typeof(StringEnumConverter))]
public enum ExtenderUpdateChannel
{
	[Description("正式版")]
	Release,
	[Description("开发版")]
	Devel,
	[Description("每夜构建")]
	Nightly
}
