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
	[SettingsEntry("游戏 Data 文件夹", "游戏安装目录中的 Data 文件夹，用于读取未打包为 .pak 的编辑器项目。\n通常会自动检测，普通用户无需修改。\n示例：Baldur's Gate 3\\Data")]
	[DataMember, Reactive] public string GameDataPath { get; set; }

	[SettingsEntry("游戏程序路径", "游戏主程序 bg3.exe 的完整路径。\n通常会自动检测；如果移动了游戏安装位置，可在此重新选择。")]
	[DataMember, Reactive] public string GameExecutablePath { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("使用 DirectX 11（DX11）启动", "启用后，管理器会运行 bg3_dx11.exe；关闭时运行默认的 bg3.exe（Vulkan）。")]
	[DataMember, Reactive] public bool LaunchDX11 { get; set; }

	[DefaultValue("")]
	[SettingsEntry("NexusMods API Key", "您在 NexusMods 的个人 API 密钥，可允许模组管理器自动获取模组的更新和信息", HideFromUI = true)]
	[DataMember, Reactive] public string NexusModsAPIKey { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记录游戏剧情脚本日志", "启动游戏时生成 Osiris 剧情脚本日志（osiris.log），用于排查脚本或模组问题。\n普通玩家通常无需开启。")]
	[DataMember, Reactive] public bool GameStoryLogEnabled { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启动器：禁用拉瑞安数据收集（遥测）", "关闭拉瑞安启动器中的使用数据收集。\n注意：启用任何模组后，游戏通常也会自动关闭此项数据收集。")]
	[DataMember, Reactive] public bool DisableLauncherTelemetry { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启动器：不显示版本不匹配警告", "隐藏拉瑞安启动器中关于模组或游戏文件版本不匹配的警告。\n这只会隐藏提示，不会修复版本不匹配。")]
	[DataMember, Reactive] public bool DisableLauncherModWarnings { get; set; }

	[DefaultValue(LaunchGameType.Exe)]
	[SettingsEntry("启动游戏方式", "选择管理器如何启动游戏，例如直接运行游戏程序或通过 Steam 启动。")]
	[DataMember, Reactive] public LaunchGameType LaunchType { get; set; }

	[DefaultValue("")]
	[SettingsEntry("自定义启动命令", "仅在“启动游戏方式”选择“自定义”时生效。\n可填写文件路径、网址协议或其他系统启动命令。")]
	[DataMember, Reactive] public string CustomLaunchAction { get; set; }

	[DefaultValue("")]
	[SettingsEntry("自定义启动参数", "传递给自定义启动命令的附加参数。\n不确定时请留空。")]
	[DataMember, Reactive] public string CustomLaunchArgs { get; set; }

	[ObservableAsProperty] public Visibility CustomLaunchVisibility { get; }

	[DefaultValue("Orders")]
	[SettingsEntry("模组排序文件夹", "保存模组加载顺序（.json）文件的文件夹。\n普通用户建议保留默认值 Orders。")]
	[DataMember, Reactive] public string LoadOrderPath { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("启用管理器内部日志", "开启模组管理器自身的日志记录功能", HideFromUI = true)]
	[DataMember, Reactive] public bool LogEnabled { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("导出时自动加入缺少的前置模组", "应用排序到游戏时，如果已启用模组所需的前置（依赖）模组不在排序中，管理器会自动加入，并放在依赖它的模组之前。\n建议保持开启。")]
	[DataMember, Reactive] public bool AutoAddDependenciesWhenExporting { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("启动时自动检查管理器更新", "每次启动模组管理器时检查官方是否发布了新版本。")]
	[DataMember, Reactive] public bool CheckForUpdates { get; set; }

	[DefaultValue("")]
	[SettingsEntry("自定义游戏 AppData 路径（实验性）", "把游戏用户数据路径改为其他文件夹。默认路径为：\n%LOCALAPPDATA%\\Larian Studios\\Baldur's Gate 3\n管理器会在这里读取模组、配置文件并写入模组排序。普通用户请留空；填错会导致管理器找不到模组或配置文件。")]
	[DataMember, Reactive] public string DocumentsFolderPathOverride { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("色觉辅助（色盲模式）", "减少只依靠颜色区分状态的情况，例如为编辑器项目额外显示图标。")]
	[DataMember, Reactive] public bool EnableColorblindSupport { get; set; }

	[DefaultValue(true)]
	[DataMember, Reactive] public bool DarkThemeEnabled { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("按回车移动模组后切换到另一列表", "在模组列表中按回车把模组移到另一侧后，键盘焦点也跟随切换到目标列表，便于继续用键盘操作。")]
	[DataMember, Reactive] public bool ShiftListFocusOnSwap { get; set; }

	[DataMember, IgnoreSetFrom] public ScriptExtenderSettings ExtenderSettings { get; set; }
	[DataMember, IgnoreSetFrom] public ScriptExtenderUpdateConfig ExtenderUpdaterSettings { get; set; }

	[DefaultValue(DivinityGameLaunchWindowAction.None)]
	[SettingsEntry("启动游戏后管理器的操作", "通过模组管理器成功启动游戏后，选择让管理器保持原样、最小化或关闭。")]
	[DataMember, Reactive]
	public DivinityGameLaunchWindowAction ActionOnGameLaunch { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("不再提示排序中缺少模组", "加载模组排序时，即使其中包含本机未安装的模组也不弹出警告。\n这只会隐藏提示，缺少的模组仍然无法加载。")]
	[DataMember, Reactive] public bool DisableMissingModWarnings { get; set; }

	[DefaultValue(false)]
	[Reactive] public bool DisplayFileNames { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("模组开发者模式", "为模组制作者显示额外功能，例如复制模组 UUID，以及更多脚本扩展器调试选项。\n普通玩家无需开启。", HideFromUI = true)]
	[Reactive, DataMember] public bool DebugModeEnabled { get; set; }

	[DefaultValue("")]
	[DataMember, Reactive] public string GameLaunchParams { get; set; }

	[DataMember] public WindowSettings Window { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("记住窗口大小和位置", "下次启动时恢复上一次关闭时的窗口大小和屏幕位置。")]
	[DataMember, Reactive] public bool SaveWindowLocation { get; set; }

	[DefaultValue(true)]
	[SettingsEntry("防止崩溃后自动停用全部模组", "自动清除游戏崩溃后生成的 ModCrashSanityCheck 标记，避免游戏下次启动时自动停用全部模组。\n这不会修复引发崩溃的模组；如果反复崩溃，仍需排查模组冲突。")]
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
