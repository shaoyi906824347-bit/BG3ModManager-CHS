using AutoUpdaterDotNET;

using DivinityModManager.Util;
using DivinityModManager.Views;
using DivinityModManager.Localization;
using DivinityModManager.Models.App;

using Newtonsoft.Json;

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

	private void OpenChineseReleasePage()
	{
		MainWindow.Self.ViewModel.Settings.LastUpdateCheck = DateTimeOffset.Now.ToUnixTimeSeconds();
		MainWindow.Self.ViewModel.SaveSettings();
		ProcessHelper.TryOpenUrl(DivinityApp.URL_CHS_RELEASES);
		IsVisible = false;
	}

	private bool _showAlert;

	private sealed class GithubReleaseData
	{
		[JsonProperty("tag_name")]
		public string TagName { get; set; }
	}

	private async Task<string> GetAvailableChineseReleaseTagAsync()
	{
		var releaseJson = await WebHelper.DownloadUrlAsStringAsync(DivinityApp.URL_CHS_LATEST_RELEASE_API, CancellationToken.None);
		if (String.IsNullOrWhiteSpace(releaseJson)) return String.Empty;

		var releaseData = DivinityJsonUtils.SafeDeserialize<GithubReleaseData>(releaseJson);
		if (releaseData == null || !LocalizationReleaseVersion.TryParseTag(releaseData.TagName, out var latestRelease)) return String.Empty;

		var installedRelease = new LocalizationReleaseVersion(AppVersion, DivinityApp.CHS_RELEASE_REVISION);
		return latestRelease.CompareTo(installedRelease) > 0 ? releaseData.TagName : String.Empty;
	}

	private async Task OnUpdateCheckAsync(UpdateInfoEventArgs args)
	{
		try
		{
			var availableChineseReleaseTag = await GetAvailableChineseReleaseTagAsync();
			var markdownText = await WebHelper.DownloadUrlAsStringAsync(DivinityApp.URL_CHANGELOG_RAW, CancellationToken.None);
			if (!String.IsNullOrEmpty(markdownText))
			{
				markdownText = RemoveEmptyLinesPattern.Replace(markdownText, string.Empty);
				await Observable.Start(() =>
				{
					UpdateChangelogView = markdownText;
				}, RxApp.MainThreadScheduler);
			}

			if (!String.IsNullOrWhiteSpace(availableChineseReleaseTag))
			{
				UpdateDescription = UpdateText.ChineseUpdateAvailable(availableChineseReleaseTag, AppVersion, DivinityApp.CHS_RELEASE_REVISION);
				CanConfirm = true;
				SkipButtonText = CommonText.Close;
				CanSkip = true;
				if (_showAlert) MainWindow.Self.ViewModel.ShowAlert(UpdateText.UpdateFoundAlert, AlertType.Info, 20);
			}
			else if (args.IsUpdateAvailable)
			{
				UpdateDescription = UpdateText.OfficialUpdateAvailable(args.CurrentVersion, AppVersion);

				CanConfirm = true;
				SkipButtonText = CommonText.Close;
				CanSkip = true;
				UpdateVersion = Version.Parse(args.CurrentVersion);
				if (_showAlert) MainWindow.Self.ViewModel.ShowAlert(UpdateText.UpdateFoundAlert, AlertType.Info, 20);
			}
			else
			{
				UpdateDescription = UpdateText.OfficialBaseIsCurrent(AppVersion);
				CanConfirm = false;
				CanSkip = true;
				SkipButtonText = CommonText.Close;
				if (_showAlert) MainWindow.Self.ViewModel.ShowAlert(UpdateText.NoUpdateAlert, AlertType.Info, 20);
			}

			if (!String.IsNullOrWhiteSpace(availableChineseReleaseTag) || args.IsUpdateAvailable || _showAlert)
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
			if (_showAlert) MainWindow.Self.ViewModel.ShowAlert(UpdateText.CheckFailed(ex.Message), AlertType.Danger, 60);

			if (ex is System.Net.WebException)
			{
				MainWindow.Self.DisplayError(UpdateText.CheckFailedTitle, UpdateText.NetworkError, false);
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
			OpenChineseReleasePage();
		}, canConfirm, RxApp.MainThreadScheduler);

		var canSkip = this.WhenAnyValue(x => x.CanSkip);
		SkipCommand = ReactiveCommand.Create(() => IsVisible = false, canSkip);
		CanSkip = true;
	}
}
