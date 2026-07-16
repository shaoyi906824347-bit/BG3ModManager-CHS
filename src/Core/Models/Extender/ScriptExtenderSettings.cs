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
	[SettingsEntry("导出默认值", "导出所有数值设置，即使其与脚本扩展器的默认数值相同")]
	[DataMember, Reactive]
	public bool ExportDefaultExtenderSettings { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用开发者模式", "启用各种用于开发的调试功能\n模组可以检测此状态以开启附加日志消息等")]
	[DataMember, Reactive]
	public bool DeveloperMode { get; set; }

	[DefaultValue("")]
	[SettingsEntry("自定义配置文件", "使用非 Public (公共) 的配置文件\n此处应为配置文件文件夹的名称")]
	[DataMember, Reactive]
	public string CustomProfile { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("创建调试控制台", "开启一个命令行控制台窗口，用于显示脚本扩展器内部日志\n对排查闪退、调试模组极其有用")]
	[DataMember, Reactive]
	public bool CreateConsole { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记录编译错误剧情日志", "在 Osiris 剧情编译出错时，将错误记录到日志文件 (LogFailedCompile) 中")]
	[DataMember, Reactive]
	public bool LogFailedCompile { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用 Osiris 活动日志", "启用 Osiris 活动日志 (规则评估、查询等) 并写入日志文件")]
	[DataMember, Reactive]
	public bool EnableLogging { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记录脚本编译日志", "将 Osiris 剧情编译过程记录到日志文件中")]
	[DataMember, Reactive]
	public bool LogCompile { get; set; }

	[DefaultValue("")]
	[SettingsEntry("日志存放目录", "生成的 Osiris 编译/活动日志的存储目录\n默认为 Documents\\OsirisLogs")]
	[DataMember, Reactive]
	public string LogDirectory { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记录运行时日志", "将扩展器控制台和脚本输出记录到日志文件中")]
	[DataMember, Reactive]
	public bool LogRuntime { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("禁用游戏启动器", "阻止游戏主程序强制打开拉瑞安启动器\n如果启用了扩展器自动更新或设置了 --skip-launcher 参数，此项可能不起作用", true)]
	[DataMember, Reactive]
	public bool DisableLauncher { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("禁用剧情合并 (Disable Story Merge)", "防止合并 story.div.osi 文件 (有模组时自动发生)\n可能只在加载存档时生效", true)]
	[DataMember, Reactive]
	public bool DisableStoryMerge { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("禁用剧情修补 (Disable Story Patching)", "在加载存档时，阻止使用 story.div.osi 修补 story.bin。这会防止存档中的 Osiris 脚本自动更新", true)]
	[DataMember, Reactive]
	public bool DisableStoryPatching { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("禁用模组校验", "在加载模组时，禁用模块哈希校验\n这可以大幅提高模组加载速度且没有副作用")]
	[DataMember, Reactive]
	public bool DisableModValidation { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("启用成就解锁", "允许在使用模组的游戏中正常解锁 Steam/GOG 成就")]
	[DataMember, Reactive]
	public bool EnableAchievements { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("启用扩展 API 功能", "启用或禁用脚本扩展器 API 接口功能", true)]
	[DataMember, Reactive]
	public bool EnableExtensions { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("发送崩溃报告", "在游戏崩溃后，自动将 Minidump 崩溃转储文件上传到崩溃报告收集服务器")]
	[DataMember, Reactive]
	public bool SendCrashReports { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用 Osiris 调试器", "启用 Osiris 调试器接口 (即 VSCode 插件调试支持)", true)]
	[DataMember, Reactive]
	public bool EnableDebugger { get; set; }

	[DefaultValue(9999)]
	[SettingsEntry("Osiris 调试端口", "Osiris 调试器监听的端口号\n默认值：9999", true)]
	[DataMember, Reactive]
	public int DebuggerPort { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("转储网络 fixed-strings 字符串表", "将 NetworkFixedString 字符串表转储至日志存放目录\n主要用于调试联机同步问题", true)]
	[DataMember, Reactive]
	public bool DumpNetworkStrings { get; set; }

	[DefaultValue(0)]
	[SettingsEntry("Osiris 调试标记", "要设置的调试器 Flags 标记\n默认值：0")]
	[DataMember, Reactive]
	public int DebuggerFlags { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用 Lua 调试器", "启用 Lua 调试器接口 (即 VSCode 插件 Lua 调试支持)", true)]
	[DataMember, Reactive]
	public bool EnableLuaDebugger { get; set; }

	[DefaultValue("")]
	[SettingsEntry("Lua 内置脚本目录", "设定一个附加文件夹，脚本扩展器将在其中检查内置脚本\n此设置专为开发者设计，便于测试内置脚本的修改", true)]
	[DataMember, Reactive]
	public string LuaBuiltinResourceDirectory { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("重置时清空控制台", "在使用重置命令时，自动清空扩展器控制台窗口的文本内容", true)]
	[DataMember, Reactive]
	public bool ClearOnReset { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("控制台默认客户端侧", "将扩展器控制台默认设为客户端侧 (Client-side)\n此设置专为开发者设计", true)]
	[DataMember, Reactive]
	public bool DefaultToClientConsole { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("显示性能警告", "将游戏服务器端落后的警告信息打印到扩展器控制台窗口 (即耗时过长的警告)。", true)]
	[DataMember, Reactive]
	public bool ShowPerfWarnings { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("禁用 ModCrashSanityCheck 崩溃自检", "禁用拉瑞安在游戏崩溃后下一次运行自动停用所有模组的崩溃检查机制")]
	[DataMember, Reactive]
	public bool InsanityCheck { get; set; }

	public ScriptExtenderSettings()
	{
		this.SetToDefault();
		ExtenderVersion = String.Empty;
		ExtenderMajorVersion = -1;
	}
}
