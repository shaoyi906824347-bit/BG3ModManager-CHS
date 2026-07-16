using DivinityModManager.Extensions;

using Newtonsoft.Json;

using System.ComponentModel;
using System.Runtime.Serialization;

namespace DivinityModManager.Models.Extender;

[DataContract]
public class ScriptExtenderSettings : ReactiveObject
{
	[Reactive] public bool ExtenderIsAvailable { get; set; }
	[Reactive] public string ExtenderVersion { get; set; }
	[Reactive] public int ExtenderMajorVersion { get; set; }

	[DefaultValue(false)]
	[JsonIgnore] // This isn't an actual extender setting, so omit it from the exported json
	[SettingsEntry("保存所有设置（包括默认值）", "保存脚本扩展器设置时，同时写入仍为默认值的项目。\n通常无需开启；开启后配置文件会更完整，但内容也更多。")]
	[DataMember, Reactive]
	public bool ExportDefaultExtenderSettings { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用脚本扩展器开发者模式", "开启脚本扩展器的开发和调试功能，部分模组会据此输出更多日志。\n普通玩家无需开启。")]
	[DataMember, Reactive]
	public bool DeveloperMode { get; set; }

	[DefaultValue("")]
	[SettingsEntry("使用其他游戏配置文件（开发者）", "让脚本扩展器使用 Public 以外的游戏配置文件。\n请填写配置文件的文件夹名称；普通玩家请留空。")]
	[DataMember, Reactive]
	public string CustomProfile { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("显示脚本扩展器控制台", "启动游戏时同时打开一个命令行窗口，实时显示脚本扩展器日志。\n排查闪退或模组问题时很有用，平时可以关闭。")]
	[DataMember, Reactive]
	public bool CreateConsole { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记录剧情脚本编译错误", "当 Osiris 剧情脚本编译失败时，把错误写入日志文件（LogFailedCompile）。\n主要用于排查脚本模组问题。")]
	[DataMember, Reactive]
	public bool LogFailedCompile { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记录 Osiris 运行日志", "把 Osiris 剧情系统的活动（规则判断、查询等）写入日志。\n日志量可能较大，通常只在排查问题时开启。")]
	[DataMember, Reactive]
	public bool EnableLogging { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记录剧情脚本编译过程", "把 Osiris 剧情脚本的编译过程写入日志，主要用于模组开发和故障排查。")]
	[DataMember, Reactive]
	public bool LogCompile { get; set; }

	[DefaultValue("")]
	[SettingsEntry("Osiris 日志文件夹", "Osiris 编译日志和运行日志的保存位置。\n留空时使用默认文件夹 Documents\\OsirisLogs。")]
	[DataMember, Reactive]
	public string LogDirectory { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记录脚本扩展器运行日志", "把脚本扩展器控制台和模组脚本的输出保存到日志文件，便于排查问题。")]
	[DataMember, Reactive]
	public bool LogRuntime { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("阻止打开拉瑞安启动器（高级）", "尝试阻止游戏主程序强制打开拉瑞安启动器。\n如果启用了脚本扩展器自动更新，或启动参数中已有 --skip-launcher，此设置可能不起作用。", true)]
	[DataMember, Reactive]
	public bool DisableLauncher { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("禁用剧情脚本合并（开发者）", "阻止游戏在有模组时自动合并 story.div.osi。\n可能只在读取存档时生效；普通玩家不要修改。", true)]
	[DataMember, Reactive]
	public bool DisableStoryMerge { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("禁用剧情脚本修补（开发者）", "读取存档时，不再使用 story.div.osi 更新 story.bin，因此存档中的 Osiris 脚本不会自动更新。\n普通玩家不要修改。", true)]
	[DataMember, Reactive]
	public bool DisableStoryPatching { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("跳过模组文件校验", "加载模组时跳过模块哈希校验，可以明显缩短加载时间。\n如果正在排查损坏或异常模组，可暂时关闭此项以恢复校验。")]
	[DataMember, Reactive]
	public bool DisableModValidation { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("允许使用模组时解锁成就", "在启用模组的游戏中重新允许解锁 Steam 或 GOG 成就。")]
	[DataMember, Reactive]
	public bool EnableAchievements { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("启用脚本扩展器 API", "允许模组调用脚本扩展器提供的功能。\n关闭后，依赖脚本扩展器的模组可能无法工作；建议保持开启。", true)]
	[DataMember, Reactive]
	public bool EnableExtensions { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("自动发送崩溃报告", "游戏崩溃后，将 Minidump 崩溃转储文件上传到脚本扩展器的崩溃报告服务器，帮助开发者定位问题。\n不希望上传崩溃信息时可以关闭。")]
	[DataMember, Reactive]
	public bool SendCrashReports { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用 Osiris 调试器（开发者）", "开启 Osiris 调试接口，供 VS Code 调试插件连接。\n普通玩家无需开启。", true)]
	[DataMember, Reactive]
	public bool EnableDebugger { get; set; }

	[DefaultValue(9999)]
	[SettingsEntry("Osiris 调试端口（开发者）", "Osiris 调试器等待 VS Code 连接时使用的端口。\n默认值：9999；普通玩家无需修改。", true)]
	[DataMember, Reactive]
	public int DebuggerPort { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("导出联机字符串表（开发者）", "把 NetworkFixedString 字符串表导出到日志文件夹，主要用于排查联机不同步问题。\n普通玩家无需开启。", true)]
	[DataMember, Reactive]
	public bool DumpNetworkStrings { get; set; }

	[DefaultValue(0)]
	[SettingsEntry("Osiris 调试标记（开发者）", "设置 Osiris 调试器使用的 Flags 数值。\n默认值：0；不了解其用途时请勿修改。")]
	[DataMember, Reactive]
	public int DebuggerFlags { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用 Lua 调试器（开发者）", "开启 Lua 调试接口，供 VS Code 调试插件连接。\n普通玩家无需开启。", true)]
	[DataMember, Reactive]
	public bool EnableLuaDebugger { get; set; }

	[DefaultValue("")]
	[SettingsEntry("Lua 内置脚本测试文件夹（开发者）", "指定一个额外文件夹，让脚本扩展器从中读取内置 Lua 脚本。\n仅用于测试脚本修改，普通玩家请留空。", true)]
	[DataMember, Reactive]
	public string LuaBuiltinResourceDirectory { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("重置脚本时清空控制台（开发者）", "执行脚本扩展器的重置命令时，同时清空控制台中已有的文字。", true)]
	[DataMember, Reactive]
	public bool ClearOnReset { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("控制台默认使用客户端环境（开发者）", "让脚本扩展器控制台默认在客户端（Client-side）环境中执行命令。\n普通玩家无需修改。", true)]
	[DataMember, Reactive]
	public bool DefaultToClientConsole { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("在控制台显示性能警告（开发者）", "当游戏服务器端处理过慢（单次 tick 耗时过长）时，在脚本扩展器控制台显示警告。", true)]
	[DataMember, Reactive]
	public bool ShowPerfWarnings { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("阻止崩溃后自动停用全部模组", "禁用游戏的 ModCrashSanityCheck 机制，避免游戏崩溃后在下次启动时自动停用全部模组。\n这不会修复引发崩溃的模组。")]
	[DataMember, Reactive]
	public bool InsanityCheck { get; set; }

	public ScriptExtenderSettings()
	{
		this.SetToDefault();
		ExtenderVersion = String.Empty;
		ExtenderMajorVersion = -1;
	}
}
