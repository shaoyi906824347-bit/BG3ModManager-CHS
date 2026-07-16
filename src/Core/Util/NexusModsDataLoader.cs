using DivinityModManager.Models;
using DivinityModManager.Models.NexusMods;
using DivinityModManager.Models.Updates;
//using DivinityModManager.ModUpdater.NexusMods;

using NexusModsNET;
using NexusModsNET.DataModels;

namespace DivinityModManager.Util;

public class NexusModsRateLimitsUpdatedEventArgs : EventArgs
{
	public NexusApiLimits Limits { get; set; }

	public NexusModsRateLimitsUpdatedEventArgs(NexusApiLimits limits)
	{
		Limits = limits;
	}
}

public delegate void NexusModsRateLimitsUpdatedEventHandler(object sender, NexusModsRateLimitsUpdatedEventArgs e);

public static class NexusModsDataLoader
{
	private static INexusModsClient _client;
	private static bool _isActive = false;
	private static bool _pendingDispose = false;

	private static string _lastApiKey = "";

	public static INexusModsClient Client => _client;

	public static event NexusModsRateLimitsUpdatedEventHandler RateLimitsUpdated;

	public static void Init(string apiKey, string appName, string appVersion)
	{
		if (!String.IsNullOrEmpty(apiKey) && apiKey != _lastApiKey)
		{
			if (Dispose())
			{
				_lastApiKey = apiKey;
				_client = NexusModsClient.Create(apiKey, appName, appVersion);
				//_client = new NexusModsCustomClient(apiKey, appName, appVersion);
				//RateLimitsUpdated ?.Invoke(_client, new NexusModsRateLimitsUpdatedEventArgs(_client.RateLimitsManagement.APILimits));
			}
		}
	}

	public static void EmitLimitsChanged(NexusApiLimits limits)
	{
		RateLimitsUpdated?.Invoke(_client, new NexusModsRateLimitsUpdatedEventArgs(limits));
	}

	public static bool Dispose()
	{
		if (!_isActive)
		{
			_client?.Dispose();
			_pendingDispose = false;
			return true;
		}
		_pendingDispose = true;
		return false;
	}

	public static bool CanFetchData => _client != null && !_client.RateLimitsManagement.ApiDailyLimitExceeded() && !_client.RateLimitsManagement.ApiHourlyLimitExceeded();
	public static bool LimitExceeded => _client != null && (_client.RateLimitsManagement.ApiDailyLimitExceeded() || !_client.RateLimitsManagement.ApiHourlyLimitExceeded());
	public static bool IsInitialized => _client != null;

	private static bool LimitExceededCheck()
	{
		if (_client != null)
		{
			var daily = _client.RateLimitsManagement.ApiDailyLimitExceeded();
			var hourly = _client.RateLimitsManagement.ApiHourlyLimitExceeded();

			if (daily)
			{
				DivinityApp.Log($"Daily limit exceeded ({_client.RateLimitsManagement.APILimits.DailyLimit})");
				return true;
			}
			else if (hourly)
			{
				DivinityApp.Log($"Hourly limit exceeded ({_client.RateLimitsManagement.APILimits.HourlyLimit})");
				return true;
			}
		}
		return false;
	}

	public static bool CanDoTask(int apiCalls)
	{
		if (_client != null)
		{
			var currentLimit = Math.Min(_client.RateLimitsManagement.APILimits.HourlyRemaining, _client.RateLimitsManagement.APILimits.DailyRemaining);
			if (currentLimit > apiCalls)
			{
				return true;
			}
		}
		return false;
	}

	private static void OnTaskDone()
	{
		_isActive = false;
		if (_pendingDispose) Dispose();
	}

	public static async Task<List<NexusModsModDownloadLink>> GetLatestDownloadsForMods(List<DivinityModData> mods, CancellationToken t)
	{
		var links = new List<NexusModsModDownloadLink>();
		if (!CanFetchData || mods.Count <= 0) return links;
		_isActive = true;

		try
		{
			var apiCallAmount = mods.Count(x => x.NexusModsData.ModId >= DivinityApp.NEXUSMODS_MOD_ID_START) & 2;
			if (!CanDoTask(apiCallAmount))
			{
				var apiAmounts = _client.RateLimitsManagement.APILimits;

				DivinityApp.Log($"Task would exceed hourly or daily API limits. ExpectedCalls({apiCallAmount}) HourlyRemaining({apiAmounts.HourlyRemaining}/{apiAmounts.HourlyLimit}) DailyRemaining({apiAmounts.DailyRemaining}/{apiAmounts.DailyLimit})");
				OnTaskDone();
				return links;
			}
			using var dataLoader = new InfosInquirer(_client);
			foreach (var mod in mods)
			{
				if (mod.NexusModsData.ModId >= DivinityApp.NEXUSMODS_MOD_ID_START)
				{
					var result = await dataLoader.ModFiles.GetModFilesAsync(DivinityApp.NEXUSMODS_GAME_DOMAIN, mod.NexusModsData.ModId, t);
					if (result != null)
					{
						var file = result.ModFiles.FirstOrDefault(x => x.IsPrimary);
						if (file != null)
						{
							var fileId = file.FileId;
							var linkResult = await dataLoader.ModFiles.GetModFileDownloadLinksAsync(DivinityApp.NEXUSMODS_GAME_DOMAIN, mod.NexusModsData.ModId, fileId, t);
							if (linkResult != null && linkResult.Count() > 0)
							{
								var primaryLink = linkResult.FirstOrDefault();
								links.Add(new NexusModsModDownloadLink(mod, primaryLink));
							}
						}
					}
				}

				if (t.IsCancellationRequested) break;
			}
		}
		catch (Exception ex)
		{
			DivinityApp.Log($"Error fetching NexusMods data:\n{ex}");
		}

		OnTaskDone();

		return links;
	}

	public static async Task<UpdateResult> LoadAllModsDataAsync(IEnumerable<DivinityModData> mods, CancellationToken t)
	{
		var taskResult = new UpdateResult();
		if (!CanFetchData)
		{
			taskResult.Success = false;
			if (_client == null)
			{
				taskResult.FailureMessage = "API 客户端尚未初始化。";
			}
			else
			{
				var rateLimits = _client.RateLimitsManagement.APILimits;
				taskResult.FailureMessage = $"已超出 NexusMods API 请求限额。每小时剩余：{rateLimits.HourlyRemaining}/{rateLimits.HourlyLimit}；每日剩余：{rateLimits.DailyRemaining}/{rateLimits.DailyLimit}";
			}
			return taskResult;
		}
		var totalLoaded = 0;

		_isActive = true;

		try
		{
			var targetMods = mods.Where(mod => mod.NexusModsData.ModId >= DivinityApp.NEXUSMODS_MOD_ID_START).ToList();
			var total = targetMods.Count;
			if (total == 0)
			{
				taskResult.Success = false;
				taskResult.FailureMessage = "已跳过：没有可检查的模组（已加载的模组均未设置 NexusMods ID）。";
				return taskResult;
			}

			var apiCallAmount = total; // 1 call for 1 mod
			if (!CanDoTask(total))
			{
				var apiAmounts = _client.RateLimitsManagement.APILimits;

				DivinityApp.Log($"Task would exceed hourly or daily API limits. ExpectedCalls({apiCallAmount}) HourlyRemaining({apiAmounts.HourlyRemaining}/{apiAmounts.HourlyLimit}) DailyRemaining({apiAmounts.DailyRemaining}/{apiAmounts.DailyLimit})");
				OnTaskDone();
				return taskResult;
			}

			DivinityApp.Log($"Using NexusMods API to update {total} mods");

			using var dataLoader = new InfosInquirer(_client);
			foreach (var mod in targetMods)
			{
				var result = await dataLoader.Mods.GetMod(DivinityApp.NEXUSMODS_GAME_DOMAIN, mod.NexusModsData.ModId, t);
				if (result != null)
				{
					mod.NexusModsData.Update(result);
					taskResult.UpdatedMods.Add(mod);
					totalLoaded++;
				}

				if (t.IsCancellationRequested) break;
			}
		}
		catch (Exception ex)
		{
			DivinityApp.Log($"Error fetching NexusMods data:\n{ex}");
		}

		OnTaskDone();

		return taskResult;
	}
}
