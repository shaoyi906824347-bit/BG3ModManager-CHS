using Newtonsoft.Json.Converters;

using System.ComponentModel;

namespace DivinityModManager;

[JsonConverter(typeof(StringEnumConverter))]
public enum DivinityGameLaunchWindowAction
{
	[Description("无")]
	None,
	[Description("启动游戏后自动最小化")]
	Minimize,
	[Description("启动游戏后自动退出")]
	Close
}
