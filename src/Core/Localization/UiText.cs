namespace DivinityModManager.Localization;

/// <summary>
/// Simplified Chinese text shared by the UI.
/// Keep frequently reused or release-sensitive wording here so upstream merges
/// do not require hunting through view models and XAML files.
/// </summary>
public static class BrandText
{
	public const string EditionName = "简体中文汉化版";
	public const string ProductNameChinese = "《博德之门 3》模组管理器";
	public const string Translator = "可爱女孩王语嫣 (小黑盒 ID: 29602586)";
	public const string FeedbackEmail = "bg3modfeedback@163.com";

	public static string GetWindowTitle(string productName, Version version)
		=> $"{productName} {version}｜{EditionName}";
}

public static class WindowText
{
	public const string About = "关于简体中文汉化版";
	public const string CheckForUpdates = "检查更新（汉化安全模式）";
	public const string Preferences = "偏好设置";
	public const string HotkeyHelp = "快捷键帮助";
	public const string Help = "帮助";
	public const string VersionGenerator = "版本生成器";
}

public static class CommonText
{
	public const string Close = "关闭";
	public const string View = "查看";
	public const string Save = "保存";
	public const string Cancel = "取消";
	public const string None = "无";
	public const string CloseWindowTooltip = "关闭此窗口";
}

public static class UpdateText
{
	public const string ViewChineseRelease = "查看汉化版发布页面";
	public const string ViewChineseReleaseTooltip = "打开汉化版 Releases 页面。此操作只打开网页，不会覆盖当前程序。";
	public const string OfficialChangelog = "官方更新日志（英文）";
	public const string UpdateFoundAlert = "发现官方新版本";
	public const string NoUpdateAlert = "当前官方基础版本已是最新";
	public const string CheckFailedTitle = "更新检查失败";
	public const string NetworkError = "连接官方更新服务器时遇到问题。请检查网络连接后重试。";
	public const string CheckOnStartupLabel = "启动时检查官方版本更新（仅提醒）";
	public const string CheckOnStartupDescription = "每次启动模组管理器时检查原版是否发布了新版本。\n此功能只显示提醒并引导你查看汉化版发布页面，不会下载、退出程序或覆盖汉化文件。";

	public static string OfficialUpdateAvailable(string officialVersion, Version installedVersion)
		=> $"官方英文版已发布 {officialVersion}，当前汉化版基于 {installedVersion}。\n"
		 + "请前往汉化版发布页面查看是否已有对应版本。程序不会自动下载或覆盖文件。";

	public static string OfficialBaseIsCurrent(Version installedVersion)
		=> $"当前汉化版基于官方版本 {installedVersion}，该基础版本目前已是最新。\n"
		 + "检查更新功能只负责提醒，不会修改或覆盖汉化文件。";

	public static string CheckFailed(string message) => $"检查更新时发生错误：{message}";
}

public static class MenuText
{
	public const string File = "文件";
	public const string Edit = "编辑";
	public const string Settings = "设置";
	public const string GoTo = "转到";
	public const string Tools = "工具";
	public const string Help = "帮助";

	public const string ImportMod = "导入模组 (.pak 文件)...";
	public const string NewOrder = "新建模组排序配置";
	public const string SaveOrder = "保存当前排序";
	public const string SaveOrderAs = "另存排序配置为...";
	public const string ImportOrderFromSave = "从游戏存档导入模组顺序...";
	public const string ImportOrderFromSaveAsNew = "从存档导入并新建排序配置...";
	public const string ImportOrderFromFile = "从文件导入排序...";
	public const string ImportOrderFromZip = "从压缩包 (.zip) 导入模组和排序...";
	public const string ExportOrderToGame = "应用模组顺序到游戏";
	public const string ExportOrderToList = "导出模组列表到文本文件...";
	public const string ExportOrderToZip = "打包当前启用的模组为压缩包 (.zip)";
	public const string ExportOrderToArchiveAs = "打包当前启用的模组并另存为...";
	public const string RefreshAllMods = "重新加载/刷新所有模组";

	public const string ToggleSelectedMods = "启用/禁用所选模组 (移动至对侧列表)";
	public const string FocusActiveList = "定位到已启用列表";
	public const string FocusInactiveList = "定位到未启用列表";
	public const string SwapListFocus = "切换列表聚焦";
	public const string MoveSelectedToTop = "置顶所选模组";
	public const string MoveSelectedToBottom = "置底所选模组";
	public const string FocusCurrentFilter = "聚焦并搜索当前列表";
	public const string ShowRealFileName = "显示模组的真实文件名";
	public const string DeleteSelectedMods = "彻底删除所选模组文件...";

	public const string GeneralSettings = "常规设置";
	public const string HotkeySettings = "快捷键设置";
	public const string ToggleTheme = "切换亮色/暗色主题";

	public const string OpenModsFolder = "打开模组存放文件夹（Mods）";
	public const string OpenGameFolder = "打开游戏安装目录";
	public const string OpenExtenderLogsFolder = "打开脚本扩展器日志文件夹";
	public const string LaunchGame = "启动游戏";

	public const string ExtractSelectedMods = "解压所选模组到指定文件夹...";
	public const string ExtractSelectedAdventure = "解压所选游戏主线模组到指定文件夹...";
	public const string OpenVersionGenerator = "打开模组版本号生成器...";
	public const string VersionGeneratorTooltip = "模组创作者工具，用于为模组的 meta.lsx 生成版本号";
	public const string DownloadScriptExtender = "在线下载并安装脚本扩展器（Script Extender）...";
	public const string SpeakActiveOrder = "语音朗读当前启用模组顺序";
	public const string StopSpeaking = "停止语音朗读";

	public const string CheckForUpdates = "检查模组管理器更新（不会覆盖汉化）";
	public const string About = "关于简体中文汉化版";
	public const string OpenOfficialRepository = "打开原版官方主页（GitHub）...";
}
