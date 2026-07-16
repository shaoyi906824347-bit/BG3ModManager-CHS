using DivinityModManager.Extensions;

using System.ComponentModel;
using System.Runtime.Serialization;

namespace DivinityModManager.Models.Extender;

[DataContract]
public class ScriptExtenderUpdateConfig : ReactiveObject
{
	[Reactive] public bool UpdaterIsAvailable { get; set; }
	[Reactive] public int UpdaterVersion { get; set; }

	[DefaultValue(ExtenderUpdateChannel.Release)]
	[SettingsEntry("更新通道", "选择脚本扩展器接收哪一类更新。\n普通用户建议选择“正式版”。", HideFromUI = true)]
	[DataMember, Reactive]
	public ExtenderUpdateChannel UpdateChannel { get; set; }

	[DefaultValue("")]
	[SettingsEntry("固定脚本扩展器版本", "只安装指定版本的脚本扩展器，例如 5.0.0.0。\n留空时自动使用所选更新通道的最新版本。")]
	[DataMember, Reactive]
	public string TargetVersion { get; set; }

	[DefaultValue("")]
	[SettingsEntry("目标文件校验值（Digest）", "指定目标更新文件的 Digest 校验值，用于确认下载内容。\n选择上方版本时会自动填写；普通用户无需手动修改。", true)]
	[DataMember, Reactive]
	public string TargetResourceDigest { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("关闭脚本扩展器自动更新", "阻止脚本扩展器自动更新到最新版本。\n已安装的版本不会被删除；除非需要固定旧版本，否则建议保持关闭。")]
	[DataMember, Reactive]
	public bool DisableUpdates { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("更新时仅使用 IPv4", "下载脚本扩展器更新时只使用 IPv4。\n仅在网络环境不支持 IPv6，或更新连接异常时尝试开启。")]
	[DataMember, Reactive]
	public bool IPv4Only { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("更新程序调试模式", "让脚本扩展器更新程序在控制台输出更详细的信息，用于排查更新失败。\n普通用户无需开启。")]
	[DataMember, Reactive]
	public bool Debug { get; set; }

	[DefaultValue("")]
	[SettingsEntry("更新清单地址（Manifest URL）", "自定义脚本扩展器更新清单的网址。\n普通用户请留空，错误地址会导致无法检查或下载更新。", true)]
	[DataMember, Reactive]
	public string ManifestURL { get; set; }

	[DefaultValue("")]
	[SettingsEntry("更新清单名称（Manifest）", "指定自定义更新清单的名称。\n普通用户请留空。", true)]
	[DataMember, Reactive]
	public string ManifestName { get; set; }

	[DefaultValue("")]
	[SettingsEntry("更新缓存文件夹", "指定脚本扩展器更新文件的临时缓存位置。\n普通用户请留空以使用默认位置。", true)]
	[DataMember, Reactive]
	public string CachePath { get; set; }

	[DefaultValue(false)]
	[SettingsEntry("验证更新文件签名", "下载后验证脚本扩展器更新文件的数字签名。\n这是自定义更新配置选项，不了解其用途时请勿修改。", true)]
	[DataMember, Reactive]
	public bool ValidateSignature { get; set; }

	public ScriptExtenderUpdateConfig()
	{
		this.SetToDefault();
		UpdaterVersion = -1;
	}
}
