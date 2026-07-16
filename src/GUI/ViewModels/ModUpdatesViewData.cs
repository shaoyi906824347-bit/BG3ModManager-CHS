

using DivinityModManager.Models.Updates;
using DivinityModManager.Util;
using DivinityModManager.Views;

using DynamicData;
using DynamicData.Binding;

using Ookii.Dialogs.Wpf;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DivinityModManager.ViewModels;

public struct CopyModUpdatesTask
{
	public List<string> NewFilesToMove;
	public List<string> UpdatesToMove;
	public string DocumentsFolder;
	public string ModPakFolder;
	public int TotalMoved;
}

public class ModUpdatesViewData : ReactiveObject
{
	[Reactive] public bool Unlocked { get; set; }
	[Reactive] public bool JustUpdated { get; set; }

	public SourceList<DivinityModUpdateData> Mods { get; private set; } = new SourceList<DivinityModUpdateData>();

	private readonly ReadOnlyObservableCollection<DivinityModUpdateData> _newMods;
	public ReadOnlyObservableCollection<DivinityModUpdateData> NewMods => _newMods;

	private readonly ReadOnlyObservableCollection<DivinityModUpdateData> _updatedMods;
	public ReadOnlyObservableCollection<DivinityModUpdateData> UpdatedMods => _updatedMods;

	readonly ObservableAsPropertyHelper<bool> _anySelected;
	public bool AnySelected => _anySelected.Value;

	readonly ObservableAsPropertyHelper<bool> _allNewModsSelected;
	public bool AllNewModsSelected => _allNewModsSelected.Value;

	readonly ObservableAsPropertyHelper<bool> _allModUpdatesSelected;
	public bool AllModUpdatesSelected => _allModUpdatesSelected.Value;

	readonly ObservableAsPropertyHelper<bool> _newAvailable;
	public bool NewAvailable => _newAvailable.Value;

	readonly ObservableAsPropertyHelper<bool> _updatesAvailable;
	public bool UpdatesAvailable => _updatesAvailable.Value;

	readonly ObservableAsPropertyHelper<int> _totalUpdates;
	public int TotalUpdates => _totalUpdates.Value;

	public ICommand CopySelectedModsCommand { get; private set; }
	public ICommand SelectAllNewModsCommand { get; private set; }
	public ICommand SelectAllUpdatesCommand { get; private set; }

	public Action OnLoaded { get; set; }

	public Action<bool> CloseView { get; set; }

	private readonly MainWindowViewModel _mainWindowViewModel;

	public void Clear()
	{
		Mods.Clear();
		Unlocked = true;
	}

	public void SelectAll(bool select = true)
	{
		foreach (var x in Mods.Items)
		{
			x.IsSelected = select;
		}
	}

	private IEnumerable<string> GetUpdateFiles(string directoryPath)
	{
		var files = DivinityFileUtils.EnumerateFiles(directoryPath, DivinityFileUtils.RecursiveOptions, f => Path.GetExtension(f).Equals(".pak", StringComparison.OrdinalIgnoreCase));
		return files;
	}

	private void CopySelectedMods_Run()
	{
		string documentsFolder = _mainWindowViewModel.PathwayData.AppDataGameFolder;
		string modPakFolder = _mainWindowViewModel.PathwayData.AppDataModsPath;

		if (Directory.Exists(modPakFolder))
		{
			Unlocked = false;
			using ProgressDialog dialog = new ProgressDialog()
			{
				WindowTitle = "正在更新模组",
				Text = "正在复制模组...",
				CancellationText = "模组更新已取消",
				MinimizeBox = false,
				ProgressBarStyle = ProgressBarStyle.ProgressBar
			};
			dialog.DoWork += CopyFilesProgress_DoWork;
			dialog.RunWorkerCompleted += CopyFilesProgress_RunWorkerCompleted;

			var args = new CopyModUpdatesTask()
			{
				DocumentsFolder = documentsFolder,
				ModPakFolder = modPakFolder,
				NewFilesToMove = NewMods.Where(x => x.IsSelected).Select(x => GetUpdateFiles(Path.GetDirectoryName(x.UpdateFilePath))).SelectMany(x => x).ToList(),
				UpdatesToMove = UpdatedMods.Where(x => x.IsSelected).Select(x => GetUpdateFiles(Path.GetDirectoryName(x.UpdateFilePath))).SelectMany(x => x).ToList(),
				TotalMoved = 0
			};

			dialog.ShowDialog(MainWindow.Self, args);
		}
		else
		{
			CloseView?.Invoke(false);
		}
	}

	public void CopySelectedMods()
	{
		using var dialog = new TaskDialog()
		{
			Buttons =
				{
					new TaskDialogButton(ButtonType.Yes),
					new TaskDialogButton(ButtonType.No)
				},
			WindowTitle = "更新模组？",
			Content = "是否使用选中的更新覆盖本地模组？",
			MainIcon = TaskDialogIcon.Warning
		};
		var result = dialog.ShowDialog(MainWindow.Self);
		if (result.ButtonType == ButtonType.Yes)
		{
			CopySelectedMods_Run();
		}
	}

	private void CopyFilesProgress_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
	{
		Unlocked = true;
		DivinityApp.Log("Mod updating complete.");
		try
		{
			if (e.Result is CopyModUpdatesTask args)
			{
				JustUpdated = args.TotalMoved > 0;
			}
		}
		catch (Exception ex)
		{
			string message = $"复制模组时发生错误：{ex}";
			DivinityApp.Log(message);
			MainWindow.Self.AlertBar.SetDangerAlert(message);
		}
		CloseView?.Invoke(true);
	}

	private void CopyFilesProgress_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		ProgressDialog dialog = (ProgressDialog)sender;
		if (e.Argument is CopyModUpdatesTask args)
		{
			var totalWork = args.NewFilesToMove.Count + args.UpdatesToMove.Count;
			if (args.NewFilesToMove.Count > 0)
			{
				DivinityApp.Log($"Copying '{args.NewFilesToMove.Count}' new mod(s) to the local mods folder.");

				foreach (string file in args.NewFilesToMove)
				{
					if (e.Cancel) return;
					var fileName = Path.GetFileName(file);
						dialog.ReportProgress(args.TotalMoved / totalWork, $"正在复制 '{fileName}'...", null);
					try
					{
						File.Copy(file, Path.Combine(args.ModPakFolder, fileName), true);
					}
					catch (Exception ex)
					{
						string message = $"复制 '{fileName}' 时发生错误：\n{ex}";
						DivinityApp.Log(message);
						MainWindow.Self.AlertBar.SetDangerAlert(message);
						dialog.ReportProgress(args.TotalMoved / totalWork, message, null);
					}
					args.TotalMoved++;
				}
			}

			if (args.UpdatesToMove.Count > 0)
			{
				string backupFolder = Path.Combine(_mainWindowViewModel.PathwayData.AppDataGameFolder, "Mods_Old_ModManager");
				Directory.CreateDirectory(backupFolder);
				DivinityApp.Log($"Copying '{args.UpdatesToMove.Count}' mod update(s) to the local mods folder.");
				foreach (string file in args.UpdatesToMove)
				{
					if (e.Cancel) return;
					string baseName = Path.GetFileName(file);
					try
					{
						DivinityApp.Log($"Moving mod into mods folder: '{file}'.");
						File.Copy(file, Path.Combine(args.ModPakFolder, Path.GetFileName(file)), true);
					}
					catch (Exception ex)
					{
						DivinityApp.Log($"Error copying mod:\n{ex}");
					}
					dialog.ReportProgress(args.TotalMoved / totalWork, $"正在复制 '{baseName}'...", null);
					args.TotalMoved++;
				}
			}
		}

	}

	public ModUpdatesViewData(MainWindowViewModel mainWindowViewModel)
	{
		Unlocked = true;

		_mainWindowViewModel = mainWindowViewModel;

		var modsConnection = Mods.Connect();

		_totalUpdates = modsConnection.Count().ToProperty(this, nameof(TotalUpdates));

		var splitList = modsConnection.AutoRefresh(x => x.IsNewMod);
		var newModsConnection = splitList.Filter(x => x.IsNewMod);
		var updatedModsConnection = splitList.Filter(x => !x.IsNewMod);

		newModsConnection.Bind(out _newMods).Subscribe();
		updatedModsConnection.Bind(out _updatedMods).Subscribe();

		var hasNewMods = newModsConnection.Count().Select(x => x > 0);
		var hasUpdatedMods = updatedModsConnection.Count().Select(x => x > 0);
		_newAvailable = hasNewMods.ToProperty(this, nameof(NewAvailable));
		_updatesAvailable = hasUpdatedMods.ToProperty(this, nameof(UpdatesAvailable));

		var selectedMods = modsConnection.AutoRefresh(x => x.IsSelected).ToCollection();
		_anySelected = selectedMods.Select(x => x.Any(y => y.IsSelected)).ToProperty(this, nameof(AnySelected), true, RxApp.MainThreadScheduler);

		var newModsChangeSet = NewMods.ToObservableChangeSet().AutoRefresh(x => x.IsSelected).ToCollection();
		var modUpdatesChangeSet = UpdatedMods.ToObservableChangeSet().AutoRefresh(x => x.IsSelected).ToCollection();

		_allNewModsSelected = splitList.Filter(x => x.IsNewMod).ToCollection().Select(x => x.All(y => y.IsSelected)).ToProperty(this, nameof(AllNewModsSelected), true, RxApp.MainThreadScheduler);
		_allModUpdatesSelected = splitList.Filter(x => !x.IsNewMod).ToCollection().Select(x => x.All(y => y.IsSelected)).ToProperty(this, nameof(AllModUpdatesSelected), true, RxApp.MainThreadScheduler);

		var anySelectedObservable = this.WhenAnyValue(x => x.AnySelected);

		CopySelectedModsCommand = ReactiveCommand.Create(CopySelectedMods, anySelectedObservable);

		SelectAllNewModsCommand = ReactiveCommand.Create<bool>((b) =>
		{
			foreach (var x in NewMods)
			{
				x.IsSelected = b;
			}
		}, hasNewMods);
		SelectAllUpdatesCommand = ReactiveCommand.Create<bool>((b) =>
		{
			foreach (var x in UpdatedMods)
			{
				x.IsSelected = b;
			}
		}, hasUpdatedMods);
	}
}
