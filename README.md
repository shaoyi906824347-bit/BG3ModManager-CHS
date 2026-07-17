# BG3 Mod Manager 简体中文汉化版 / Simplified Chinese Localization

## 中文说明

这是 [Baldur's Gate 3 Mod Manager](https://github.com/LaughingLeader/BG3ModManager) 的简体中文汉化版，面向希望使用中文界面的《博德之门3》玩家。

本项目基于官方 BG3 Mod Manager 1.0.12.9，并包含以下内容：

* 主界面、菜单、设置和提示的简体中文翻译，尽量使用易懂的中文名称。
* 修复导出操作可能导致按钮永久变灰的问题。
* 删除主界面的赞助与反馈入口，保持界面简洁。
* 保留原项目的主要功能和许可证。

### 使用说明

1. 先运行一次游戏，让游戏创建用户配置文件和模组文件夹。
2. 安装 [.NET 8 桌面运行时](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.15-windows-x64-installer) 和 [最新版 C++ 运行库](https://aka.ms/vs/17/release/vc_redist.x64.exe)。
3. 从本仓库的 [Releases](https://github.com/shaoyi906824347-bit/BG3ModManager-CHS/releases) 下载汉化版压缩包；如果暂时没有发布包，可以在 GitHub 点击 **Code → Download ZIP** 下载源码。
4. 这是便携版程序，请解压到普通文件夹，不要放在 `Program Files` 中。
5. 从英文版迁移时，只需将原目录中的 `Data` 和 `Orders` 文件夹复制到汉化版目录，即可保留设置、快捷键和模组排序。
6. 启动 `BG3ModManager.exe`，选择模组并点击“应用模组顺序到游戏”。

### 更新注意事项

本项目的汉化内容编译在程序文件中。请不要使用程序内的“下载官方版更新”功能，否则官方英文版可能会覆盖汉化。请等待本仓库发布对应的汉化版更新。

## English Description

This is a community-maintained Simplified Chinese localization of [Baldur's Gate 3 Mod Manager](https://github.com/LaughingLeader/BG3ModManager), intended for Baldur's Gate 3 players who prefer a Chinese interface.

This project is based on the official BG3 Mod Manager 1.0.12.9 and includes:

* Simplified Chinese translations for the main interface, menus, settings, and tooltips.
* A fix for export operations that could leave the toolbar buttons disabled.
* A simplified main toolbar without donation and feedback links.
* The main features and license of the upstream project.

### Usage

1. Run the game once so it can create its profile and mod folders.
2. Install the [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.15-windows-x64-installer) and the [latest C++ Redistributable](https://aka.ms/vs/17/release/vc_redist.x64.exe).
3. Download the Chinese build from this repository's [Releases](https://github.com/shaoyi906824347-bit/BG3ModManager-CHS/releases). If no release package is available yet, use **Code → Download ZIP** to download the source code.
4. The manager is portable. Extract it to a normal folder, not `Program Files`.
5. To migrate from the English version, copy the `Data` and `Orders` folders from the old manager directory to the Chinese version directory.
6. Run `BG3ModManager.exe`, arrange your mods, and click “Apply Load Order to Game”.

### Update Notice

The localization is compiled into the program files. Do not use the in-app “Download Official Update” action, as it may replace the Chinese build with the official English version. Please wait for a matching Chinese release from this repository.

## 原始项目 / Upstream Project

This project is based on LaughingLeader's original BG3 Mod Manager. For the official English version, documentation, and upstream development, visit the [original repository](https://github.com/LaughingLeader/BG3ModManager).

A mod manager for [Baldur's Gate 3](https://store.steampowered.com/app/1086940/Baldurs_Gate_3/).

# Setup / 安装

1. Run the game once if you haven't already, so a profile and the mods folders get created.
2. Make sure you have [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.15-windows-x64-installer) and [the latest C++ redistributable](https://aka.ms/vs/17/release/vc_redist.x64.exe) installed.
3. [Grab the latest Chinese release.](https://github.com/shaoyi906824347-bit/BG3ModManager-CHS/releases/latest)
4. The BG3 Mod Manager is portable, so extract it to a non-protected folder (don't extract it to your Program Files).
5. Upon running `BG3ModManager.exe`, pathways to the game data and exe should be automatically detected.  
*If this fails, manually set the pathways in Settings -> Preferences, click 'Save', then click the 'Refresh' button so the campaign mod data is loaded.*  
![Preferences Window](/Screenshots/PreferencesWindow_GameDataPath.png?raw=true "Making sure the Game Data Path is set.")
6. Organize your active mods for the profile `Public`, then click the first export button (Export Load Order to Game), or click File -> Export Order to Game, to export your active load order to the game. This updates the `modsettings.lsx` file that the game reads.
 [![Exporting Load Orders](https://i.imgur.com/m9IBQrj.png)](https://i.imgur.com/m9IBQrj.png)

# Important Tips  
* Make sure you don't have any subfolders in your mods folder (`%LOCALAPPDATA%\Larian Studios\Baldur's Gate 3\Mods`). This causes the game to reset your `modsettings.lsx`!
* Ensure the `Game Data Path` is set in Settings -> Preferences to the game's data folder, where all the various .pak files are (Gustav.pak etc).
* Make sure you have a campaign selected (i.e. "Main"). The game must have a campaign exported to the `modsettings.lsx`, or it will fail to load the main menu scene / have other issues.
* If your `modsettings.lsx` still resets when loading into the game, this means that one or more of your mods are encountering an error, and the game is clearing the load order.

# Current Features:

* Reorganize mod load orders with a quick drag-and-drop interface. Allows reordering multiple mods at once.
  * View details about each mod, including the description and dependencies.
* Save your mod load orders to external json files for sharing or backing things up.
* Export your active mod order to various text formats (i.e. a spreadsheet). These formats will include extra data, such as the mod's steam workshop url, if any.
* Filter mods by name and properties (author, mode, etc.).
* Export load order mods to zip files (including editor mods), for easier sharing of a playthrough's mods between friends.
* Import load orders from save files.
* Shortcut buttons to all the various game-related folders (mods folder, workshop folder, game directory, etc).
* Dark and light theme support.

## Features for Mod Authors

* Extract selected mods with a few clicks. Useful for mod authors, or those wanting to study mod files for learning.
* Copy a mod's UUID or FolderName in the right click menu. Useful for if you're setting up Ext.IsModLoaded checks with the script extender, for mod support.
* You can specify custom tags in your project's meta.lsx (the "Tags" property"). Seperate tags with a semi-colon, and the mod manager will display them.
* A "Version Generator" tool is available under the Tools menu, for generating the correct number for major/minor/revision/build numbers.

[![Custom Tags](https://i.imgur.com/bxkVqssl.jpg)](https://i.imgur.com/bxkVqss.png)

# Notes

* Mod projects in the Data folder are highlighted in green. They can be used in the load order like regular mods, and even exported to zip files.
* New profiles must be made in-game. You should also run the game at least once, so all of the game's user folders are created.
* Highlight over mods to see their description and list of dependencies. Red dependencies are missing dependencies.

# Links / 链接

* [Chinese Release](https://github.com/shaoyi906824347-bit/BG3ModManager-CHS/releases/latest)
* [Chinese Repository](https://github.com/shaoyi906824347-bit/BG3ModManager-CHS)
* [Upstream Official Repository](https://github.com/LaughingLeader/BG3ModManager)
* [Changelog](https://github.com/LaughingLeader/BG3ModManager/wiki/Changelog)
* [Leader's Lair Discord](https://discord.gg/j5gp6MD)

# Building From Source  
## External Libraries  
* [lslib](https://github.com/Norbyte/lslib)

# Credits

* Thanks to [Norbyte](https://github.com/Norbyte) for creating [LSLib](https://github.com/Norbyte/lslib), which allows various features of the manager (getting data from paks, reading lsb files, just to name a few).
* [Baldur's Gate 3](https://store.steampowered.com/app/1086940/Baldurs_Gate_3/), a wonderful game from [Larian Studios](http://larian.com/)
