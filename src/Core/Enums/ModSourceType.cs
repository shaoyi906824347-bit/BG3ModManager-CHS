using System.ComponentModel;

namespace DivinityModManager;

public enum ModSourceType
{
	[Description("无")]
	NONE,
	[Description("Steam 创意工坊")]
	STEAM,
	[Description("Nexus Mods")]
	NEXUSMODS,
	[Description("GitHub")]
	GITHUB
}
