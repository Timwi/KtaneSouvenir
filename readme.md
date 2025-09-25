# Souvenir

## Intro

![Number of supported modules](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FTimwi%2FKtaneSouvenir%2Fmaster%2Fdata.json&query=%24.modulesCount&label=Modules%20Supported&color=green)
![Total number of questions](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FTimwi%2FKtaneSouvenir%2Fmaster%2Fdata.json&query=%24.questionsCount&label=Questions&color=green)
![Total number of discriminators](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FTimwi%2FKtaneSouvenir%2Fmaster%2Fdata.json&query=%24.discriminatorsCount&label=Discriminators&color=green)

This is a mod module for [_Keep Talking and Nobody Explodes_](https://keeptalkinggame.com/). This module asks you questions about some of the other modules on your bomb after you’ve solved them. It requires you to remember or write down things that you used to solve those other modules.

A build is available on the [Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=810934485).
The manual is available on the [Repository of Manual Pages](https://ktane.timwi.de/HTML/Souvenir.html).

Based on the [_Keep Talking and Nobody Explodes_ modkit](https://github.com/Qkrisi/ktanemodkit/).

## Contributing

[![](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FTimwi%2FKtaneSouvenir%2Fmaster%2Fdata.json&query=%24.contributorsCount&label=Contributors
)](/CONTRIBUTORS.md)

If you wish to see another module added, you can contribute development by submitting pull requests or finding a volunteer modder on the KTaNE Discord server. Additions would generally be required to satisfy the following guidelines:

* No questions about things that can just be read off after the module is solved (e.g. the symbols in Astrology). However, Souvenir may hide this information on some modules, such as Adventure Game.
* No questions about things that can be re-deduced by re-solving the module (e.g. offset in Caesar Cipher).
* No questions about random things that aren’t required for solving a module (e.g. initial red/green state in Connection Check).
* No questions that could under any circumstances be impossible to answer. This includes things that are removed/reset when striking on a module.
* No questions about modules that are part of an ongoing [manual challenge](https://ktane.timwi.de/More/Glossary.html#manual-challenge).
* Questions about [boss modules](https://ktane.timwi.de/More/Glossary.html#boss-module) are allowed as long as they satisfy the above guidelines. The boss modules should remain on Souvenir’s ignore list.
* Prefer questions that ask what was actually shown on the module rather than what the defuser had to input to solve it (e.g. prefer displayed digits over calculated digits in Forget Me Not).

The following vanilla modules are supported by Souvenir: The Button, Maze, Memory, Simon Says, Wire Sequence. These may be excluded using a setting in the ModSettings file. This file will be created the first time a Souvenir module is loaded on a bomb. After that, the mod settings may be edited using a text editor or the [Mod Selector](https://steamcommunity.com/sharedfiles/filedetails/?id=801400247) tablet with the [Tweaks](https://steamcommunity.com/sharedfiles/filedetails/?id=1366808675) mod. Souvenir itself and other boss modules may also be excluded this way.

## Building

- In Unity, build Souvenir without a separate assembly (keyboard: F7; menu: “Keep Talking ModKit” → “Build AssetBundle (no assembly)”).
- To make this possible, the compiled binary `SouvenirLib.dll` is included in the git repo. Any changes to the source code (in `Lib/`) require a rebuild in Visual Studio, which automatically copies that DLL to the right place for Unity to find it.

## Translations

[![](https://img.shields.io/badge/EN-100.00%25-red)](/Lib/Question.cs)
[![](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FTimwi%2FKtaneSouvenir%2Fmaster%2Fdata.json&query=%24.translationProgress.de&label=DE&color=red)](/Lib/TranslationDE.cs)
[![](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FTimwi%2FKtaneSouvenir%2Fmaster%2Fdata.json&query=%24.translationProgress.ja&label=JA&color=red)](/Lib/TranslationJA.cs)
[![](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FTimwi%2FKtaneSouvenir%2Fmaster%2Fdata.json&query=%24.translationProgress.ru&label=RU&color=red)](/Lib/TranslationRU.cs)

For contributing to translations, see [translations.md](translations.md).

## Adding new module support

First, ensure that there isn’t already a contributor working on adding the module you are hoping to implement. The KTANE Discord server has a thread for this (https://discord.com/channels/160061833166716928/1133454524435148860) where our efforts are coordinated.

The actual source code is in `Lib`. Open `Lib/SouvenirLib.sln` in Visual Studio to get started. The module handlers are in `Lib/Handlers`.

If your installation of KTANE is *not* in the default folder (`C:\Program Files (x86)\Steam\steamapps\common\Keep Talking and Nobody Explodes\`), create a `SouvenirLib.csproj.user` file containing the following and change the folder accordingly. You MUST end your path with a backslash (`\`).

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <ProjectView>ProjectFiles</ProjectView>
    <GameFolder>F:\SteamLibrary\steamapps\common\Keep Talking and Nobody Explodes\</GameFolder>
  </PropertyGroup>
</Project>
```

To add a new module, the following steps are required:

- Unsubscribe from the Workshop version of Souvenir.
- Build the asset bundle in Unity with no assembly (F7) and copy the result to your game’s `mods` folder. You only need to do this once as long as you keep that local copy of Souvenir there.
- Follow the steps in `documentation.md`. You can either start fresh, or find an existing handler for a module that is similar to the one you wish to implement and take a copy of it.
- Compile the project.
- Run Souvenir in Unity and issue a TP command (such as `!1 bulb`) to see your question to make sure that it looks okay.
- Test your Souvenir modifications in-game (e.g. by using Dynamic Mission Generator). Test all corner cases, including getting a strike on the module.
- If your question does not show up and/or Souvenir displays a warning triangle, look at your logfile (the actual file, not the LFA) for error messages from Souvenir. You can Ctrl+F in the logfile for `<Souvenir` to find them.
- Please make a separate git commit for each module you implement. If you made multiple commits for the same module, please squash them into one.
- After submitting the pull request, DM me (Timwi on Discord) the questions to be added to the manual, in the format `Module Name: Question? Question?`, for example:
    ```
    Quiz Buzz: What was the number initially on the display?
    Memory Wires: What were the wire colours? What were the displayed digits?
    The Matrix: Which word was part of the latest access code? What was the glitched word?
    ```
    I will do the rest to update the manual and the info on the repo.
