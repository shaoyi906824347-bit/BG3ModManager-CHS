# BG3 Mod Manager 简体中文汉化版 / Simplified Chinese Localization

## 中文说明

这是 [Baldur's Gate 3 Mod Manager](https://github.com/LaughingLeader/BG3ModManager) 的简体中文汉化版，面向希望使用中文界面的《博德之门3》玩家。

本汉化版基于原始项目 1.0.12.9，保留原项目的主要功能和许可证，并额外修复了导出操作可能导致按钮永久变灰的问题。

从英文版迁移时，只需将原管理器目录中的 `Data` 和 `Orders` 文件夹复制到汉化版目录，即可保留设置、快捷键和模组排序。

请注意：汉化内容编译在程序文件中。不要使用程序内的“下载官方版更新”功能，否则官方英文版可能会覆盖汉化。请等待本仓库发布对应的汉化版更新。

## English Description

This is a community-maintained Simplified Chinese localization of [Baldur's Gate 3 Mod Manager](https://github.com/LaughingLeader/BG3ModManager), based on version 1.0.12.9 of the upstream project.

It keeps the original project's main features and license, and includes an additional fix for export operations that could leave the toolbar buttons disabled.

To migrate from the English version, copy the `Data` and `Orders` folders from the original manager directory to the Chinese version directory. This preserves settings, keybindings, and saved mod orders.

The localization is compiled into the program files. Do not use the in-app “Download Official Update” action, as it may replace the Chinese build with the official English version. Please wait for a matching Chinese release from this repository.

---

# LaughingLeader 的《博德之门 3》模组管理器

一个用于管理[《博德之门 3》](https://store.steampowered.com/app/1086940/Baldurs_Gate_3/)模组的工具。

**原始项目的官方来源只有其官方 GitHub 仓库。** 本仓库是社区维护的中文汉化版，并非官方仓库。

## 安装

1. 如果还没有运行过游戏，请先运行一次，让游戏创建用户配置文件和模组文件夹。
2. 确保已安装 [.NET 8.0 桌面运行时](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.15-windows-x64-installer) 和[最新版 C++ 运行库](https://aka.ms/vs/17/release/vc_redist.x64.exe)。
3. 汉化版发布包请前往本仓库的 [Releases 页面](https://github.com/shaoyi906824347-bit/BG3ModManager-CHS/releases) 下载；原始项目的官方英文版可以在[这里下载](https://github.com/LaughingLeader/BG3ModManager/releases/latest/download/BG3ModManager_Latest.zip)。
4. BG3 模组管理器是便携式程序，请将它解压到普通文件夹中，不要解压到 `Program Files` 等受保护目录。
5. 启动 `BG3ModManager.exe` 后，程序通常会自动识别游戏 Data 文件夹和游戏程序路径。

   *如果自动识别失败，请打开“设置 → 偏好设置”，手动设置路径并点击“保存”，然后点击“刷新”，以加载游戏本体模组数据。*
   ![偏好设置窗口](/Screenshots/PreferencesWindow_GameDataPath.png?raw=true "确认游戏 Data 文件夹路径")
6. 在 `Public` 配置文件中整理要启用的模组，然后点击第一个导出按钮“应用模组顺序到游戏”，也可以打开“文件 → 应用模组顺序到游戏”。该操作会更新游戏读取的 `modsettings.lsx` 文件。
   [![导出模组顺序](https://i.imgur.com/m9IBQrj.png)](https://i.imgur.com/m9IBQrj.png)

## 重要提示

* 模组文件夹中不要包含子文件夹：`%LOCALAPPDATA%\Larian Studios\Baldur's Gate 3\Mods`。子文件夹可能导致游戏重置 `modsettings.lsx`！
* 请确认“设置 → 偏好设置”中的“游戏 Data 文件夹路径”指向游戏 Data 文件夹，也就是包含各种 `.pak` 文件（例如 `Gustav.pak`）的文件夹。
* 请确认已选择游戏本体模组（例如“主线”）。游戏必须将一个游戏本体模组写入 `modsettings.lsx`，否则可能无法加载主菜单场景或出现其他问题。
* 如果进入游戏时 `modsettings.lsx` 仍然被重置，通常说明一个或多个模组发生错误，游戏因此清除了模组加载顺序。

## 主要功能

* 通过快速的拖放界面调整模组加载顺序，也可以一次移动多个模组。
  * 查看每个模组的详细信息，包括描述和依赖项。
* 将模组加载顺序保存为外部 JSON 文件，方便分享或备份。
* 将当前启用的模组顺序导出为多种文本格式（例如表格文件），其中可以包含模组的 Steam 创意工坊链接等额外信息。
* 按名称和属性筛选模组，例如作者、类型等。
* 将加载顺序中的模组打包为 ZIP 压缩包，包括编辑器模组，方便在玩家之间分享同一套游戏模组。
* 从游戏存档导入模组加载顺序。
* 提供多个快捷按钮，用于打开各种游戏相关文件夹，例如模组文件夹、创意工坊文件夹和游戏目录。
* 支持深色和浅色主题。

## 面向模组作者的功能

* 只需几次点击即可提取选定的模组，适合模组作者或希望研究模组文件的用户。
* 在右键菜单中复制模组的 UUID 或文件夹名称，方便设置脚本扩展器的 `Ext.IsModLoaded` 检查或编写模组兼容说明。
* 可以在项目的 `meta.lsx` 中设置自定义标签（`Tags` 属性）。多个标签之间使用分号分隔，模组管理器会显示这些标签。
* “工具”菜单中提供“版本号生成器”，可生成正确的主版本、次版本、修订版本和构建版本编号。

[![自定义标签](https://i.imgur.com/bxkVqssl.jpg)](https://i.imgur.com/bxkVqss.png)

## 说明

* `Data` 文件夹中的模组项目会以绿色高亮显示。它们可以像普通模组一样加入加载顺序，也可以打包为 ZIP 压缩包。
* 新配置文件必须在游戏内创建。建议至少运行一次游戏，让游戏创建所有用户文件夹。
* 将鼠标悬停在模组上，可以查看模组描述和依赖项列表。红色依赖项表示缺少对应依赖模组。

## 链接

* [汉化版仓库](https://github.com/shaoyi906824347-bit/BG3ModManager-CHS)
* [汉化版发布页面](https://github.com/shaoyi906824347-bit/BG3ModManager-CHS/releases)
* [原始项目官方仓库](https://github.com/LaughingLeader/BG3ModManager)
* [原始项目更新日志](https://github.com/LaughingLeader/BG3ModManager/wiki/Changelog)
* [Leader's Lair Discord 社区](https://discord.gg/j5gp6MD)

## 支持原始项目

如果你愿意支持原始项目作者，可以通过 Ko-fi 请作者喝杯咖啡：

[![请我喝杯咖啡](https://i.imgur.com/NkmwXff.png)](https://ko-fi.com/LaughingLeader)

所有赞助都会用于支持原始项目的持续开发。感谢支持！

## 从源代码构建

### 外部库

* [lslib](https://github.com/Norbyte/lslib)

## 致谢

* 感谢 [Norbyte](https://github.com/Norbyte) 开发 [LSLib](https://github.com/Norbyte)。它为模组管理器提供了许多功能，例如读取 PAK 文件中的数据和读取 LSB 文件等。
* 感谢 [Larian Studios](http://larian.com/) 制作了精彩的游戏[《博德之门 3》](https://store.steampowered.com/app/1086940/Baldurs_Gate_3/)。
