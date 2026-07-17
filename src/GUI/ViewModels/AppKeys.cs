

using DivinityModManager.Models.App;
using DivinityModManager.Localization;
using DivinityModManager.Util;

using DynamicData;

using Newtonsoft.Json;

using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

namespace DivinityModManager.ViewModels;

public class AppKeys : ReactiveObject
{
	[MenuSettings(MenuText.File, MenuText.ImportMod, true)]
	public Hotkey ImportMod { get; private set; } = new Hotkey(Key.O, ModifierKeys.Control);

	[MenuSettings(MenuText.File, MenuText.NewOrder, true)]
	public Hotkey NewOrder { get; private set; } = new Hotkey(Key.N, ModifierKeys.Control);

	[MenuSettings(MenuText.File, MenuText.SaveOrder)]
	public Hotkey Save { get; private set; } = new Hotkey(Key.S, ModifierKeys.Control);

	[MenuSettings(MenuText.File, MenuText.SaveOrderAs, true)]
	public Hotkey SaveAs { get; private set; } = new Hotkey(Key.S, ModifierKeys.Control | ModifierKeys.Alt);

	[MenuSettings(MenuText.File, MenuText.ImportOrderFromSave)]
	public Hotkey ImportOrderFromSave { get; private set; } = new Hotkey(Key.I, ModifierKeys.Control);

	[MenuSettings(MenuText.File, MenuText.ImportOrderFromSaveAsNew)]
	public Hotkey ImportOrderFromSaveAsNew { get; private set; } = new Hotkey(Key.I, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings(MenuText.File, MenuText.ImportOrderFromFile)]
	public Hotkey ImportOrderFromFile { get; private set; } = new Hotkey(Key.O, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings(MenuText.File, MenuText.ImportOrderFromZip, true)]
	public Hotkey ImportOrderFromZipFile { get; private set; } = new Hotkey(Key.None);

	[MenuSettings(MenuText.File, MenuText.ExportOrderToGame)]
	public Hotkey ExportOrderToGame { get; private set; } = new Hotkey(Key.E, ModifierKeys.Control);

	[MenuSettings(MenuText.File, MenuText.ExportOrderToList)]
	public Hotkey ExportOrderToList { get; private set; } = new Hotkey(Key.E, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings(MenuText.File, MenuText.ExportOrderToZip)]
	public Hotkey ExportOrderToZip { get; private set; } = new Hotkey(Key.R, ModifierKeys.Control);

	[MenuSettings(MenuText.File, MenuText.ExportOrderToArchiveAs, true)]
	public Hotkey ExportOrderToArchiveAs { get; private set; } = new Hotkey(Key.R, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings(MenuText.File, MenuText.RefreshAllMods)]
	public Hotkey Refresh { get; private set; } = new Hotkey(Key.F5);

	//[MenuSettings("File", "Refresh Mod Updates")]
	public Hotkey RefreshModUpdates { get; private set; } = new Hotkey(Key.None);

	[MenuSettings(MenuText.Edit, MenuText.ToggleSelectedMods, true)]
	public Hotkey Confirm { get; private set; } = new Hotkey(Key.Enter);

	[MenuSettings(MenuText.Edit, MenuText.FocusActiveList)]
	public Hotkey MoveFocusLeft { get; private set; } = new Hotkey(Key.Left);

	[MenuSettings(MenuText.Edit, MenuText.FocusInactiveList)]
	public Hotkey MoveFocusRight { get; private set; } = new Hotkey(Key.Right);

	[MenuSettings(MenuText.Edit, MenuText.SwapListFocus)]
	public Hotkey SwapListFocus { get; private set; } = new Hotkey(Key.Tab);

	[MenuSettings(MenuText.Edit, MenuText.MoveSelectedToTop)]
	public Hotkey MoveToTop { get; private set; } = new Hotkey(Key.PageUp, ModifierKeys.Control);

	[MenuSettings(MenuText.Edit, MenuText.MoveSelectedToBottom, true)]
	public Hotkey MoveToBottom { get; private set; } = new Hotkey(Key.PageDown, ModifierKeys.Control);

	[MenuSettings(MenuText.Edit, MenuText.FocusCurrentFilter, AddSeparator = true)]
	public Hotkey ToggleFilterFocus { get; private set; } = new Hotkey(Key.F, ModifierKeys.Control);

	[MenuSettings(MenuText.Edit, MenuText.ShowRealFileName)]
	public Hotkey ToggleFileNameDisplay { get; private set; } = new Hotkey(Key.None);

	[MenuSettings(MenuText.Edit, MenuText.DeleteSelectedMods, AddSeparator = true)]
	public Hotkey DeleteSelectedMods { get; private set; } = new Hotkey(Key.Delete);

	[MenuSettings(MenuText.Settings, MenuText.GeneralSettings)]
	public Hotkey OpenPreferences { get; private set; } = new Hotkey(Key.P, ModifierKeys.Control);

	[MenuSettings(MenuText.Settings, MenuText.HotkeySettings)]
	public Hotkey OpenKeybindings { get; private set; } = new Hotkey(Key.K, ModifierKeys.Control);

	[MenuSettings(MenuText.Settings, MenuText.ToggleTheme)]
	public Hotkey ToggleViewTheme { get; private set; } = new Hotkey(Key.L, ModifierKeys.Control);

	//[MenuSettings("View", "Toggle Updates View")]
	public Hotkey ToggleUpdatesView { get; private set; } = new Hotkey();

	[MenuSettings(MenuText.GoTo, MenuText.OpenModsFolder)]
	public Hotkey OpenModsFolder { get; private set; } = new Hotkey(Key.D1, ModifierKeys.Control);

	[MenuSettings(MenuText.GoTo, MenuText.OpenGameFolder)]
	public Hotkey OpenGameFolder { get; private set; } = new Hotkey(Key.D2, ModifierKeys.Control);

	[MenuSettings(MenuText.GoTo, MenuText.OpenExtenderLogsFolder)]
	public Hotkey OpenLogsFolder { get; private set; } = new Hotkey(Key.D4, ModifierKeys.Control);

	[MenuSettings(MenuText.GoTo, MenuText.LaunchGame)]
	public Hotkey LaunchGame { get; private set; } = new Hotkey(Key.G, ModifierKeys.Control | ModifierKeys.Shift);

	[MenuSettings(MenuText.Tools, MenuText.ExtractSelectedMods)]
	public Hotkey ExtractSelectedMods { get; private set; } = new Hotkey(Key.OemPeriod, ModifierKeys.Control);

	[MenuSettings(MenuText.Tools, MenuText.ExtractSelectedAdventure)]
	public Hotkey ExtractSelectedAdventure { get; private set; } = new Hotkey(Key.None);

	[MenuSettings(MenuText.Tools, MenuText.OpenVersionGenerator, Tooltip = MenuText.VersionGeneratorTooltip)]
	public Hotkey ToggleVersionGeneratorWindow { get; private set; } = new Hotkey(Key.G, ModifierKeys.Control);

	[MenuSettings(MenuText.Tools, MenuText.DownloadScriptExtender)]
	public Hotkey DownloadScriptExtender { get; private set; } = new Hotkey(Key.T, ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt);

	[MenuSettings(MenuText.Tools, MenuText.SpeakActiveOrder)]
	public Hotkey SpeakActiveModOrder { get; private set; } = new Hotkey(Key.Home, ModifierKeys.Control);

	[MenuSettings(MenuText.Tools, MenuText.StopSpeaking)]
	public Hotkey StopSpeaking { get; private set; } = new Hotkey(Key.Home, ModifierKeys.Control | ModifierKeys.Alt);

	[MenuSettings(MenuText.Help, MenuText.CheckForUpdates)]
	public Hotkey CheckForUpdates { get; private set; } = new Hotkey(Key.F7);

	[MenuSettings(MenuText.Help, MenuText.About)]
	public Hotkey OpenAboutWindow { get; private set; } = new Hotkey(Key.F1);

	[MenuSettings(MenuText.Help, MenuText.OpenOfficialRepository)]
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
