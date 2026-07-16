using AutoUpdaterDotNET;

using DivinityModManager.Util;
using DivinityModManager.Views;

using System.Text.RegularExpressions;
using System.Windows.Input;

namespace DivinityModManager.ViewModels;
public partial class AppUpdateWindowViewModel : ReactiveObject
{
	[Reactive] public bool IsVisible { get; set; }
	[Reactive] public bool CanConfirm { get; set; }
	[Reactive] public bool CanSkip { get; set; }
	[Reactive] public string AppTitle { get; set; }
	[Reactive] public Version AppVersion { get; set; }
	[Reactive] public string SkipButtonText { get; set; }
	[Reactive] public string UpdateDescription { get; set; }
	[Reactive] public string UpdateChangelogView { get; set; }
	[Reactive] public Version UpdateVersion { get; set; }

	public ICommand ConfirmCommand { get; private set; }
	public ICommand SkipCommand { get; private set; }
	public ReactiveCommand<UpdateInfoEventArgs, Unit> OnUpdateCheckCommand { get; private set; }

	[GeneratedRegex(@"^\s+$[\r\n]*", RegexOptions.Multiline)]
	private static partial Regex RemoveEmptyLinesRe();

	private static readonly Regex RemoveEmptyLinesPattern = RemoveEmptyLinesRe();

	private UpdateInfoEventArgs? _updateArgs;

	private void TryRunUpdate()
	{
		try
		{
			MainWindow.Self.ViewModel.Settings.LastUpdateCheck = DateTimeOffset.Now.ToUnixTimeSeconds();
			MainWindow.Self.ViewModel.SaveSettings();
			if (AutoUpdater.DownloadUpdate(_updateArgs))
			{
				System.Windows.Application.Current.Shutdown();
			}
			Environment.Exit(0);
		}
		catch (Exception ex)
		{
			DivinityApp.Log($"Error updating program:\n{ex}");
			IsVisible = false;
		}
	}

	private bool _showAlert;

	private async Task OnUpdateCheckAsync(UpdateInfoEventArgs args)
	{
		try
		{
			var markdownText = await WebHelper.DownloadUrlAsStringAsync(DivinityApp.URL_CHANGELOG_RAW, CancellationToken.None);
			if (!String.IsNullOrEmpty(markdownText))
			{
				markdownText = RemoveEmptyLinesPattern.Replace(markdownText, string.Empty);
				await Observable.Start(() =>
				{
					UpdateChangelogView = markdownText;
				}, RxApp.MainThreadScheduler);
			}

			_updateArgs = args;

			if (args.IsUpdateAvailable)
			{
				UpdateDescription = $"已检测到新版本 {args.CurrentVersion}。\n当前已安装的版本为 {AppVersion}。\n【注意】当前运行的是汉化版本。如果更新，汉化将被官方英文版本覆盖！";

				CanConfirm = true;
				SkipButtonText = "跳过";
				CanSkip = true;
				UpdateVersion = Version.Parse(args.CurrentVersion);
				if (_showAlert) MainWindow.Self.ViewModel.ShowAlert("发现新版本！", AlertType.Success, 20);
			}
			else
			{
				UpdateDescription = $"{AppTitle} 已是最新版本。\n当前已安装的版本为 {AppVersion}。";
				CanConfirm = false;
				CanSkip = true;
				SkipButtonText = "关闭";
				if (_showAlert) MainWindow.Self.ViewModel.ShowAlert("已经是最新版本", AlertType.Info, 20);
			}

			if (args.IsUpdateAvailable || _showAlert)
			{
				RxApp.MainThreadScheduler.Schedule(() =>
				{
					IsVisible = true;
				});
			}
		}
		catch(Exception ex)
		{
			DivinityApp.Log($"Error checking for update:\n{ex}");
			if (_showAlert) MainWindow.Self.ViewModel.ShowAlert($"检查更新时发生错误：{ex.Message}", AlertType.Danger, 60);

			if (ex is System.Net.WebException)
			{
				MainWindow.Self.DisplayError("更新检查失败", "连接更新服务器时遇到问题。请检查您的网络连接并重试。", false);
			}
		}
	}

	public void ScheduleUpdateCheck(bool showAlerts = false)
	{
		_showAlert = showAlerts;
		AutoUpdater.ReportErrors = _showAlert;
		AutoUpdater.Start(DivinityApp.URL_UPDATE);
	}

	public AppUpdateWindowViewModel()
	{
		OnUpdateCheckCommand = ReactiveCommand.CreateFromTask<UpdateInfoEventArgs>(OnUpdateCheckAsync, null, RxApp.TaskpoolScheduler);

		//Observable.FromEventPattern<AutoUpdater.CheckForUpdateEventHandler, UpdateInfoEventArgs>(
		//  handler => AutoUpdater.CheckForUpdateEvent += handler,
		//  handler => AutoUpdater.CheckForUpdateEvent -= handler).ObserveOn(RxApp.TaskpoolScheduler).InvokeCommand(OnUpdateCheckCommand);

		var canConfirm = this.WhenAnyValue(x => x.CanConfirm);
		ConfirmCommand = ReactiveCommand.Create(() =>
		{
			TryRunUpdate();
		}, canConfirm, RxApp.MainThreadScheduler);

		var canSkip = this.WhenAnyValue(x => x.CanSkip);
		SkipCommand = ReactiveCommand.Create(() => IsVisible = false, canSkip);
		CanSkip = true;
	}
}
