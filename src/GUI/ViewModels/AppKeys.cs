

using DivinityModManager.Models.App;
using DivinityModManager.Util;

using DynamicData;

using Newtonsoft.Json;

using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

namespace DivinityModManager.ViewModels;

public class AppKeys : ReactiveObject
{
	[MenuSettings("文件", "导入模组 (.pak 文件)...", true)]
	public Hotkey ImportMod { get; private set; } = new Hotkey(Key.O, ModifierKeys.Control);

	[MenuSettings("文件", "新建模组排序配置", true)]
	public Hotkey NewOrder { get; private set; } = new Hotkey(Key.N, ModifierKeys.Control);

	[MenuSettings("文件", "保存当前排序")]
	public Hotkey Save { get; private set; } = new Hotkey(Key.S, ModifierKeys.Control);

	[MenuSettings("文件", "另存排序配置为...", true)]
	public Hotkey SaveAs { get; private set; } = new Hotkey(Key.S, ModifierKeys.Control | ModifierKeys.Alt);

	[MenuSettings("文件", "从游戏存档导入模组顺序...")]
	public Hotkey ImportOrderFromSave { get; private set; } = new Hotkey(Key.I, ModifierKeys.Control);

	[MenuSettings("文件", "从存档导入并新建排序配置...")]
	public Hotkey ImportOrderFromSaveAsNew { get; private set; } = new Hotkey(Key.I, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings("文件", "从文件导入排序...")]
	public Hotkey ImportOrderFromFile { get; private set; } = new Hotkey(Key.O, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings("文件", "从压缩包 (.zip) 导入模组和排序...", true)]
	public Hotkey ImportOrderFromZipFile { get; private set; } = new Hotkey(Key.None);

	[MenuSettings("文件", "应用模组顺序到游戏")]
	public Hotkey ExportOrderToGame { get; private set; } = new Hotkey(Key.E, ModifierKeys.Control);

	[MenuSettings("文件", "导出模组列表到文本文件...")]
	public Hotkey ExportOrderToList { get; private set; } = new Hotkey(Key.E, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings("文件", "打包当前启用的模组为压缩包 (.zip)")]
	public Hotkey ExportOrderToZip { get; private set; } = new Hotkey(Key.R, ModifierKeys.Control);

	[MenuSettings("文件", "打包当前启用的模组并另存为...", true)]
	public Hotkey ExportOrderToArchiveAs { get; private set; } = new Hotkey(Key.R, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings("文件", "重新加载/刷新所有模组")]
	public Hotkey Refresh { get; private set; } = new Hotkey(Key.F5);

	//[MenuSettings("File", "Refresh Mod Updates")]
	public Hotkey RefreshModUpdates { get; private set; } = new Hotkey(Key.None);

	[MenuSettings("编辑", "启用/禁用所选模组 (移动至对侧列表)", true)]
	public Hotkey Confirm { get; private set; } = new Hotkey(Key.Enter);

	[MenuSettings("编辑", "定位到已启用列表")]
	public Hotkey MoveFocusLeft { get; private set; } = new Hotkey(Key.Left);

	[MenuSettings("编辑", "定位到未启用列表")]
	public Hotkey MoveFocusRight { get; private set; } = new Hotkey(Key.Right);

	[MenuSettings("编辑", "切换列表聚焦")]
	public Hotkey SwapListFocus { get; private set; } = new Hotkey(Key.Tab);

	[MenuSettings("编辑", "置顶所选模组")]
	public Hotkey MoveToTop { get; private set; } = new Hotkey(Key.PageUp, ModifierKeys.Control);

	[MenuSettings("编辑", "置底所选模组", true)]
	public Hotkey MoveToBottom { get; private set; } = new Hotkey(Key.PageDown, ModifierKeys.Control);

	[MenuSettings("编辑", "聚焦并搜索当前列表", AddSeparator = true)]
	public Hotkey ToggleFilterFocus { get; private set; } = new Hotkey(Key.F, ModifierKeys.Control);

	[MenuSettings("编辑", "显示模组的真实文件名")]
	public Hotkey ToggleFileNameDisplay { get; private set; } = new Hotkey(Key.None);

	[MenuSettings("编辑", "彻底删除所选模组文件...", AddSeparator = true)]
	public Hotkey DeleteSelectedMods { get; private set; } = new Hotkey(Key.Delete);

	[MenuSettings("设置", "常规设置")]
	public Hotkey OpenPreferences { get; private set; } = new Hotkey(Key.P, ModifierKeys.Control);

	[MenuSettings("设置", "快捷键设置")]
	public Hotkey OpenKeybindings { get; private set; } = new Hotkey(Key.K, ModifierKeys.Control);

	[MenuSettings("设置", "切换亮色/暗色主题")]
	public Hotkey ToggleViewTheme { get; private set; } = new Hotkey(Key.L, ModifierKeys.Control);

	//[MenuSettings("View", "Toggle Updates View")]
	public Hotkey ToggleUpdatesView { get; private set; } = new Hotkey();

	[MenuSettings("转到", "打开模组存放文件夹（Mods）")]
	public Hotkey OpenModsFolder { get; private set; } = new Hotkey(Key.D1, ModifierKeys.Control);

	[MenuSettings("转到", "打开游戏安装目录")]
	public Hotkey OpenGameFolder { get; private set; } = new Hotkey(Key.D2, ModifierKeys.Control);

	[MenuSettings("转到", "打开脚本扩展器日志文件夹")]
	public Hotkey OpenLogsFolder { get; private set; } = new Hotkey(Key.D4, ModifierKeys.Control);

	[MenuSettings("转到", "启动游戏")]
	public Hotkey LaunchGame { get; private set; } = new Hotkey(Key.G, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings("工具", "解压所选模组到指定文件夹...")]
	public Hotkey ExtractSelectedMods { get; private set; } = new Hotkey(Key.OemPeriod, ModifierKeys.Control);

	[MenuSettings("工具", "解压所选游戏主线模组到指定文件夹...")]
	public Hotkey ExtractSelectedAdventure { get; private set; } = new Hotkey(Key.None);

	[MenuSettings("工具", "打开模组版本号生成器...", Tooltip = "模组创作者工具，用于为模组的 meta.lsx 生成版本号")]
	public Hotkey ToggleVersionGeneratorWindow { get; private set; } = new Hotkey(Key.G, ModifierKeys.Control);

	[MenuSettings("工具", "在线下载并安装脚本扩展器（Script Extender）...")]
	public Hotkey DownloadScriptExtender { get; private set; } = new Hotkey(Key.T, ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt);

	[MenuSettings("工具", "语音朗读当前启用模组顺序")]
	public Hotkey SpeakActiveModOrder { get; private set; } = new Hotkey(Key.Home, ModifierKeys.Control);

	[MenuSettings("工具", "停止语音朗读")]
	public Hotkey StopSpeaking { get; private set; } = new Hotkey(Key.Home, ModifierKeys.Control | ModifierKeys.Alt);

	[MenuSettings("帮助", "检查模组管理器更新")]
	public Hotkey CheckForUpdates { get; private set; } = new Hotkey(Key.F7);

	[MenuSettings("帮助", "关于软件")]
	public Hotkey OpenAboutWindow { get; private set; } = new Hotkey(Key.F1);

	[MenuSettings("帮助", "打开官方主页（GitHub）...")]
	public Hotkey OpenRepositoryPage { get; private set; } = new Hotkey(Key.F11);

	private readonly SourceCache<Hotkey, string> keyMap = new((hk) => hk.ID);

	protected readonly ReadOnlyObservableCollection<Hotkey> allKeys;
	public ReadOnlyObservableCollection<Hotkey> All => allKeys;

	public void SaveDefaultKeybindings()
	{
		string filePath = DivinityApp.GetAppDirectory("Data", "keybindings-default.json");
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));
			var keyMapDict = new Dictionary<string, Hotkey>();
			foreach (var key in All)
			{
				keyMapDict.Add(key.ID, key);
			}
			string contents = JsonConvert.SerializeObject(keyMapDict, Newtonsoft.Json.Formatting.Indented);
			File.WriteAllText(filePath, contents);
		}
		catch (Exception ex)
		{
			DivinityApp.Log($"Error saving default keybindings at '{filePath}': {ex}");
		}
	}

	public bool SaveKeybindings(out string result)
	{
		result = "";
		var filePath = DivinityApp.GetAppDirectory("Data", "keybindings.json");
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));
			var keyMapDict = new Dictionary<string, Hotkey>();
			foreach (var key in All)
			{
				if (!key.IsDefault)
				{
					keyMapDict.Add(key.ID, key);
				}
			}
			if (keyMapDict.Count > 0)
			{
				string contents = JsonConvert.SerializeObject(keyMapDict, Newtonsoft.Json.Formatting.Indented);
				File.WriteAllText(filePath, contents);
			}
			else
			{
				File.WriteAllText(filePath, "{}");
			}
			result = $"快捷键配置已保存到 '{filePath}'";
			return true;
		}
		catch (Exception ex)
		{
			result = $"保存快捷键配置到 '{filePath}' 时发生错误：{ex}";
		}
		return false;
	}

	public bool LoadKeybindings(MainWindowViewModel vm)
	{
		var filePath = DivinityApp.GetAppDirectory("Data", "keybindings.json");
		try
		{
			if (DivinityJsonUtils.TrySafeDeserializeFromPath<Dictionary<string, Hotkey>>(filePath, out var allKeybindings))
			{
				foreach (var kvp in allKeybindings)
				{
					var existingHotkey = All.FirstOrDefault(x => x.ID.Equals(kvp.Key, StringComparison.OrdinalIgnoreCase));
					if (existingHotkey != null)
					{
						existingHotkey.Key = kvp.Value.Key;
						existingHotkey.Modifiers = kvp.Value.Modifiers;
						existingHotkey.UpdateDisplayBindingText();
					}
				}
				return true;
			}
		}
		catch (Exception ex)
		{
			vm.ShowAlert($"加载快捷键配置 '{filePath}' 时发生错误：{ex}", AlertType.Danger);
		}
		return false;
	}

	public void SetToDefault()
	{
		foreach (var entry in keyMap.Items)
		{
			entry.ResetToDefault();
		}
	}

	public AppKeys(MainWindowViewModel vm)
	{
		keyMap.Connect().Bind(out allKeys).Subscribe();
		var baseCanExecute = vm.WhenAnyValue(x => x.IsLocked, b => !b);
		Type t = typeof(AppKeys);
		// Building a list of keys / key names from properties, because lazy
		var keyProps = t.GetRuntimeProperties().Where(prop => Attribute.IsDefined(prop, typeof(MenuSettingsAttribute)) && prop.GetGetMethod() != null).ToList();
		foreach (var prop in keyProps)
		{
			var hotkey = (Hotkey)t.GetProperty(prop.Name).GetValue(this);
			hotkey.AddCanExecuteCondition(baseCanExecute);
			hotkey.ID = prop.Name;
			keyMap.AddOrUpdate(hotkey);
		}
	}
}
