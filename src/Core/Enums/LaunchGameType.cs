using System.ComponentModel.DataAnnotations;

namespace DivinityModManager;
public enum LaunchGameType
{
	[Display(Name = "直接启动（Exe）", Description = "直接运行游戏主程序；如果 bin 文件夹中不存在 steam_appid.txt，则自动创建，以便跳过拉瑞安启动器")]
	Exe,
	[Display(Name = "通过 Steam 启动", Description = "使用 Steam 启动协议（steam://run/1086940）运行游戏")]
	Steam,
	[Display(Name = "自定义", Description = "运行自定义文件或协议，例如批处理文件或 playnite://playnite/start/id 等协议处理程序")]
	Custom
}
