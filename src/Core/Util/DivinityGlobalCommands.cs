

using DivinityModManager.Models;
using DivinityModManager.ViewModels;

using System.Diagnostics;
using System.Windows;

namespace DivinityModManager.Util;

public class DivinityGlobalCommands : ReactiveObject
{
	private IDivinityAppViewModel _viewModel;

	public IDivinityAppViewModel ViewModel => _viewModel;

	public void SetViewModel(IDivinityAppViewModel vm)
	{
		_viewModel = vm;
		this.RaisePropertyChanged(nameof(ViewModel));
	}

	public ReactiveCommand<string, Unit> OpenFileCommand { get; private set; }
	public ReactiveCommand<string, Unit> OpenInFileExplorerCommand { get; private set; }
	public RxCommandUnit ClearMissingModsCommand { get; private set; }
	public ReactiveCommand<DivinityModData, Unit> ToggleNameDisplayCommand { get; private set; }
	public ReactiveCommand<string, Unit> CopyToClipboardCommand { get; private set; }
	public ReactiveCommand<DivinityModData, Unit> DeleteModCommand { get; private set; }
	public ReactiveCommand<DivinityModData, Unit> OpenNexusModsPageCommand { get; private set; }
	public ReactiveCommand<string, Unit> OpenURLCommand { get; private set; }
	public ReactiveCommand<DivinityModData, Unit> ToggleForceAllowInLoadOrderCommand { get; private set; }

	public void OpenFile(string path)
	{
		if (File.Exists(path))
		{
			try
			{
				Process.Start(Path.GetFullPath(path));
			}
			catch (System.ComponentModel.Win32Exception) // No File Association
			{
				Process.Start("explorer.exe", $"\"{Path.GetFullPath(path)}\"");
			}
		}
		else if (Directory.Exists(path))
		{
			Process.Start("explorer.exe", $"\"{Path.GetFullPath(path)}\"");
		}
		else
		{
			_viewModel.ShowAlert($"无法打开 '{path}'：文件或文件夹不存在", AlertType.Danger, 10);
		}
	}

	public void OpenInFileExplorer(string path)
	{
		if (File.Exists(path))
		{
			Process.Start("explorer.exe", $"/select, \"{Path.GetFullPath(path)}\"");
		}
		else if (Directory.Exists(path))
		{
			Process.Start("explorer.exe", $"\"{Path.GetFullPath(path)}\"");
		}
		else
		{
			_viewModel.ShowAlert($"无法打开 '{path}'：文件或文件夹不存在", AlertType.Danger, 10);
		}
	}

	public void CopyToClipboard(string text)
	{
		try
		{
			Clipboard.SetText(text);
			_viewModel.ShowAlert("文本已复制到剪贴板。", 0, 10);
		}
		catch (Exception ex)
		{
			_viewModel.ShowAlert($"复制文本到剪贴板时发生错误：{ex}", AlertType.Danger, 10);
		}
	}

	public void OpenURL(string url)
	{
		if (!String.IsNullOrEmpty(url))
		{
			ProcessHelper.TryOpenUrl(url);
		}
	}

	public void OpenNexusModsPage(DivinityModData mod)
	{
		var url = mod.GetURL(ModSourceType.NEXUSMODS);
		if (!String.IsNullOrEmpty(url))
		{
			ProcessHelper.TryOpenUrl(url);
		}
	}

	public void OpenRepositoryPage(DivinityModData mod)
	{
		var url = mod.GetURL(ModSourceType.GITHUB);
		if (!String.IsNullOrEmpty(url))
		{
			ProcessHelper.TryOpenUrl(url);
		}
	}

	public void ToggleForceAllowInLoadOrder(DivinityModData mod)
	{
		RxApp.MainThreadScheduler.Schedule(() =>
		{
			mod.ForceAllowInLoadOrder = !mod.ForceAllowInLoadOrder;
			if (mod.ForceAllowInLoadOrder)
			{
				ViewModel.AddActiveMod(mod);
			}
			else
			{
				ViewModel.RemoveActiveMod(mod);
			}
		});
	}

	public void ClearMissingMods()
	{
		_viewModel.ClearMissingMods();
	}

	public DivinityGlobalCommands()
	{
		var canExecuteViewModelCommands = this.WhenAnyValue(x => x.ViewModel, x => x.ViewModel.IsLocked, (vm, b) => vm != null && !b);

		OpenFileCommand = ReactiveCommand.Create<string>(OpenFile, canExecuteViewModelCommands);
		OpenInFileExplorerCommand = ReactiveCommand.Create<string>(OpenInFileExplorer, canExecuteViewModelCommands);
		ClearMissingModsCommand = ReactiveCommand.Create(ClearMissingMods, canExecuteViewModelCommands);

		ToggleNameDisplayCommand = ReactiveCommand.Create<DivinityModData>((mod) =>
		{
			mod.DisplayFileForName = !mod.DisplayFileForName;
			var b = mod.DisplayFileForName;
			foreach (var m in _viewModel.Mods)
			{
				if (m.IsSelected)
				{
					m.DisplayFileForName = b;
				}
			}
		}, canExecuteViewModelCommands);

		CopyToClipboardCommand = ReactiveCommand.Create<string>(CopyToClipboard, canExecuteViewModelCommands);

		DeleteModCommand = ReactiveCommand.Create<DivinityModData>((mod) =>
		{
			if (mod.CanDelete && _viewModel != null)
			{
				_viewModel.DeleteMod(mod);
			}
		}, canExecuteViewModelCommands);

		OpenURLCommand = ReactiveCommand.Create<string>(OpenURL, canExecuteViewModelCommands);
		OpenNexusModsPageCommand = ReactiveCommand.Create<DivinityModData>(OpenNexusModsPage, canExecuteViewModelCommands);
		ToggleForceAllowInLoadOrderCommand = ReactiveCommand.Create<DivinityModData>(ToggleForceAllowInLoadOrder, canExecuteViewModelCommands);
	}
}
