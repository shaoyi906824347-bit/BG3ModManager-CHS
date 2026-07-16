using DivinityModManager.Enums.Extender;
using DivinityModManager.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.ComponentModel;
using System.Runtime.Serialization;

namespace DivinityModManager.Models.Extender;

[DataContract]
public class ScriptExtenderUpdateConfig : ReactiveObject
{
	[Reactive] public bool UpdaterIsAvailable { get; set; }
	[Reactive] public int UpdaterVersion { get; set; }

	[DefaultValue(ExtenderUpdateChannel.Release)]
	[SettingsEntry("更新通道", "选择脚本扩展器的更新通道", HideFromUI = true)]
	[DataMember, Reactive]
	public ExtenderUpdateChannel UpdateChannel { get; set; }

	[DefaultValue("")]
	[SettingsEntry("目标版本", "将脚本扩展器更新到指定版本（例如 5.0.0.0）")]
	[DataMember, Reactive]
	public string TargetVersion { get; set; }

	[DefaultValue("")]
	[SettingsEntry("目标资源摘要", "为目标更新指定 Digest 校验值", true)]
	[DataMember, Reactive]
	public string TargetResourceDigest { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("禁用更新", "禁止自动更新到最新的脚本扩展器版本")]
	[DataMember, Reactive]
	public bool DisableUpdates { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("仅使用 IPv4", "获取最新更新时只使用 IPv4")]
	[DataMember, Reactive]
	public bool IPv4Only { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("调试模式", "启用脚本扩展器更新程序的调试模式，并在控制台中输出更多消息")]
	[DataMember, Reactive]
	public bool Debug { get; set; }

	[DefaultValue("")]
	[SettingsEntry("Manifest 地址", "", true)]
	[DataMember, Reactive]
	public string ManifestURL { get; set; }

	[DefaultValue("")]
	[SettingsEntry("Manifest 名称", "", true)]
	[DataMember, Reactive]
	public string ManifestName { get; set; }

	[DefaultValue("")]
	[SettingsEntry("缓存路径", "", true)]
	[DataMember, Reactive]
	public string CachePath { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("验证签名", "", true)]
	[DataMember, Reactive]
	public bool ValidateSignature { get; set; }

	public ScriptExtenderUpdateConfig()
	{
		this.SetToDefault();
		UpdaterVersion = -1;
	}
}
