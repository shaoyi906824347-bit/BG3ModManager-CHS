using Newtonsoft.Json.Converters;

using System.ComponentModel;

namespace DivinityModManager;

[JsonConverter(typeof(StringEnumConverter))]
public enum DivinityGameLaunchWindowAction
{
	[Description("无")]
	None,
	[Description("最小化管理器")]
	Minimize,
	[Description("关闭管理器")]
	Close
}
