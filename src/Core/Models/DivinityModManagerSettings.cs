using DivinityModManager.Extensions;
using DivinityModManager.Models.App;
using DivinityModManager.Models.Extender;
using DivinityModManager.Util;

using DynamicData;
using DynamicData.Binding;

using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;

namespace DivinityModManager.Models;

[DataContract]
public class DivinityModManagerSettings : ReactiveObject
{
	[SettingsEntry("游戏 Data 目录路径", "Data 文件夹路径，用于加载非 .pak 格式的编辑器模组。\n例如：Baldur's Gate 3/Data")]
	[DataMember, Reactive] public string GameDataPath { get; set; }

	[SettingsEntry("游戏主程序 (bg3.exe) 路径", "bg3.exe 文件的完整绝对路径")]
	[DataMember, Reactive] public string GameExecutablePath { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("使用 DirectX 11 启动", "启用后，启动游戏时将使用 bg3_dx11.exe 而非默认的 bg3.exe (Vulkan 模式)")]
	[DataMember, Reactive] public bool LaunchDX11 { get; set; }

	[DefaultValue("")]
	[SettingsEntry("NexusMods API Key", "您在 NexusMods 的个人 API 密钥，可允许模组管理器自动获取模组的更新和信息", HideFromUI = true)]
	[DataMember, Reactive] public string NexusModsAPIKey { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用剧情日志", "在游戏启动时，自动开启 Osiris 剧情编译器日志 (osiris.log)")]
	[DataMember, Reactive] public bool GameStoryLogEnabled { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启动器 - 禁用拉瑞安数据收集 (Telemetry)", "禁用拉瑞安启动器中的用户数据分析选项\n注意：如果启用了任何模组，数据收集都会被自动禁用")]
	[DataMember, Reactive] public bool DisableLauncherTelemetry { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启动器 - 禁用版本不匹配警告", "禁用拉瑞安启动器中关于模组或游戏文件版本不匹配的警告")]
	[DataMember, Reactive] public bool DisableLauncherModWarnings { get; set; }

	[DefaultValue(LaunchGameType.Exe)]
	[SettingsEntry("启动游戏方式", "设定启动游戏的方式 (如直接执行、通过 Steam 启动等)")]
	[DataMember, Reactive] public LaunchGameType LaunchType { get; set; }

	[DefaultValue("")]
	[SettingsEntry("自定义启动命令", "自定义要运行的文件路径、协议或进程 Shell 启动指令")]
	[DataMember, Reactive] public string CustomLaunchAction { get; set; }

	[DefaultValue("")]
	[SettingsEntry("自定义启动参数", "传递给自定义启动命令的附加可选命令行参数")]
	[DataMember, Reactive] public string CustomLaunchArgs { get; set; }

	[ObservableAsProperty] public Visibility CustomLaunchVisibility { get; }

	[DefaultValue("Orders")]
	[SettingsEntry("模组排序配置保存路径", "保存模组加载顺序 .json 文件的文件夹路径")]
	[DataMember, Reactive] public string LoadOrderPath { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用管理器内部日志", "开启模组管理器自身的日志记录功能", HideFromUI = true)]
	[DataMember, Reactive] public bool LogEnabled { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("导出时自动补全缺失的依赖模组", "如果在当前启用排序中遗漏了依赖模组，在导出到游戏时，将自动把该依赖模组置于其子模组之上")]
	[DataMember, Reactive] public bool AutoAddDependenciesWhenExporting { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("软件启动时自动检查更新", "在模组管理器启动时自动检索最新版本")]
	[DataMember, Reactive] public bool CheckForUpdates { get; set; }

	[DefaultValue("")]
	[SettingsEntry("覆盖 AppData 存档路径", "[实验性功能]\n覆盖默认的路径 %LOCALAPPDATA%\\Larian Studios\\Baldur's Gate 3\n该文件夹是模组管理器用来导出排序、读取配置和加载模组的地方。")]
	[DataMember, Reactive] public string DocumentsFolderPathOverride { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("无障碍色彩辅助 (色盲支持)", "启用色盲色彩辅助，比如会为原本显示为绿色背景的 Toolkit 项目显示标识图标")]
	[DataMember, Reactive] public bool EnableColorblindSupport { get; set; }

	[DefaultValue(true)]
	[DataMember, Reactive] public bool DarkThemeEnabled { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("回车启用模组时跳转聚焦", "当在未启用模组列表上按回车将其移至已启用列表时，光标聚焦也同步移动到已启用列表")]
	[DataMember, Reactive] public bool ShiftListFocusOnSwap { get; set; }

	[DataMember] public ScriptExtenderSettings ExtenderSettings { get; set; }
	[DataMember] public ScriptExtenderUpdateConfig ExtenderUpdaterSettings { get; set; }

	[DefaultValue(DivinityGameLaunchWindowAction.None)]
	[SettingsEntry("游戏启动时的管理器操作", "当通过管理器成功启动游戏后，对模组管理器窗口执行的操作")]
	[DataMember, Reactive]
	public DivinityGameLaunchWindowAction ActionOnGameLaunch { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("跳过缺失模组的警告", "如果加载的排序中存在未下载的缺失模组，将不会弹出警告提示")]
	[DataMember, Reactive] public bool DisableMissingModWarnings { get; set; }

	[DefaultValue(false)]
	[Reactive] public bool DisplayFileNames { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("模组开发人员模式", "为模组制作者启用额外功能，例如在右键菜单中复制模组的 UUID，以及开放更多脚本扩展器调试选项", HideFromUI = true)]
	[Reactive, DataMember] public bool DebugModeEnabled { get; set; }

	[DefaultValue("")]
	[DataMember, Reactive] public string GameLaunchParams { get; set; }

	[DataMember] public WindowSettings Window { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记住窗口位置", "在软件启动时，自动恢复上一次关闭时的窗口大小和位置。")]
	[DataMember, Reactive] public bool SaveWindowLocation { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("自动清除崩溃检测标记 (ModCrashSanityCheck)", "自动删除 %LOCALAPPDATA%/Larian Studios/Baldur's Gate 3/ModCrashSanityCheck 文件夹。该文件夹存在时会导致游戏下次运行时自动停用所有模组")]
	[DataMember, Reactive] public bool DeleteModCrashSanityCheck { get; set; }

	[DataMember] public ConfirmationSettings Confirmations { get; set; }

	[DataMember, Reactive] public long LastUpdateCheck { get; set; }

	[DataMember, Reactive] public string LastOrder { get; set; }

	[DataMember, Reactive] public string LastImportDirectoryPath { get; set; }
	[DataMember, Reactive] public string LastLoadedOrderFilePath { get; set; }
	[DataMember, Reactive] public string LastExtractOutputPath { get; set; }

	public bool Loaded { get; set; }

	private bool canSaveSettings = false;

	public bool CanSaveSettings
	{
		get => canSaveSettings;
		set { this.RaiseAndSetIfChanged(ref canSaveSettings, value); }
	}

	public bool SettingsWindowIsOpen { get; set; }


	[Reactive] public string DefaultExtenderLogDirectory { get; set; }
	[Reactive] public string ExtenderLogDirectory { get; set; }

	private static string GetExtenderLogsDirectory(string defaultDirectory, string logDirectory)
	{
		if (String.IsNullOrWhiteSpace(logDirectory))
		{
			return defaultDirectory;
		}
		return logDirectory;
	}

	private static bool TryGetExtraProperty<T>(IDictionary<string, object> additionalProperties, string key, out T value)
	{
		value = default;
		if(additionalProperties.TryGetValue(key, out var entryObj) && entryObj is T entry)
		{
			value = entry;
			return true;
		}
		return false;
	}

	[Newtonsoft.Json.JsonExtensionData]
	private IDictionary<string, object> AdditionalFields { get; set; } = new Dictionary<string, object>();

	[OnDeserialized]
	private void OnDeserialized(StreamingContext context)
	{
		if (TryGetExtraProperty(AdditionalFields, "LaunchThroughSteam", out bool launchThroughSteam) && launchThroughSteam == true)
		{
			LaunchType = LaunchGameType.Steam;
		}
	}

	public void InitSubscriptions()
	{
		var properties = typeof(DivinityModManagerSettings)
		.GetRuntimeProperties()
		.Where(prop => Attribute.IsDefined(prop, typeof(DataMemberAttribute)))
		.Select(prop => prop.Name)
		.ToArray();

		this.WhenAnyPropertyChanged(properties).Subscribe((c) =>
		{
			if (SettingsWindowIsOpen) CanSaveSettings = true;
		});

		var extenderProperties = typeof(ScriptExtenderSettings)
		.GetRuntimeProperties()
		.Where(prop => Attribute.IsDefined(prop, typeof(DataMemberAttribute)))
		.Select(prop => prop.Name)
		.ToArray();

		ExtenderSettings.WhenAnyPropertyChanged(extenderProperties).Subscribe((c) =>
		{
			if (SettingsWindowIsOpen) CanSaveSettings = true;
		});

		var extenderUpdaterProperties = typeof(ScriptExtenderUpdateConfig)
		.GetRuntimeProperties()
		.Where(prop => Attribute.IsDefined(prop, typeof(DataMemberAttribute)))
		.Select(prop => prop.Name)
		.ToArray();

		ExtenderUpdaterSettings.WhenAnyPropertyChanged(extenderUpdaterProperties).Subscribe((c) =>
		{
			if (SettingsWindowIsOpen) CanSaveSettings = true;
		});

		this.WhenAnyValue(x => x.DebugModeEnabled).Subscribe(b => DivinityApp.DeveloperModeEnabled = b);

		this.WhenAnyValue(x => x.DefaultExtenderLogDirectory, x => x.ExtenderSettings.LogDirectory)
		.Select(x => GetExtenderLogsDirectory(x.Item1, x.Item2))
		.BindTo(this, x => x.ExtenderLogDirectory);

		this.WhenAnyValue(x => x.LaunchType, x => x == LaunchGameType.Custom)
			.Select(PropertyConverters.BoolToVisibility)
			.ToUIProperty(this, x => x.CustomLaunchVisibility, Visibility.Collapsed);
	}

	public DivinityModManagerSettings()
	{
		Loaded = false;
		//Defaults
		ExtenderSettings = new ScriptExtenderSettings();
		ExtenderUpdaterSettings = new ScriptExtenderUpdateConfig();
		Window = new WindowSettings();
		Confirmations = new();

		DefaultExtenderLogDirectory = "";

		this.SetToDefault();
	}
}
