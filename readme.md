# Souvenir

This is a mod module for [_Keep Talking and Nobody Explodes_](https://keeptalkinggame.com/). This module asks you questions about some of the other modules on your bomb after you’ve solved them. It requires you to remember or write down things that you used to solve those other modules.

A build is available on the [Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2018009170).
The manual is available on the [Repository of Manual Pages](https://ktane.timwi.de/HTML/Souvenir.html).

Based on the [_Keep Talking and Nobody Explodes_ modkit](https://github.com/keeptalkinggame/ktanemodkit/).

Some of the things this module might ask about are listed below. For a complete list, please see [the manual](https://ktane.timwi.de/HTML/Souvenir.html).

* **3D Maze**: What were the floor markings and cardinal direction?
* **Bitmaps**: How many white/black squares were in each quadrant?
* **Chess**: What were the coordinates shown on the module?
* **Hexamaze**: What color was the pawn?
* **Monsplode, Fight!**: Which Monsplode and moves were displayed?
* **Mouse in the Maze**: What color were the torus and the solution sphere?
* **Simon States**: Which colors flashed?
* **The Bulb**: What were the correct buttons that you pressed?
* **Two Bits**: What were the three correct query responses?

If you wish to see another module added, you can contribute development by submitting pull requests or finding a volunteer modder on the KTaNE Discord guild. Additions would generally be required to satisfy the following guidelines:

* No questions about things that can just be read off after the module is solved (e.g. the symbols in Astrology). However, Souvenir may hide this information on some modules, such as Adventure Game.
* No questions about things that can be re-deduced by re-solving the module (e.g. offset in Caesar Cipher).
* No questions about random things that aren’t required for solving a module (e.g. initial red/green state in Connection Check).
* No questions that could under any circumstances be impossible to answer. This includes things that are removed/reset when striking on a module.
* No questions about modules that are part of an ongoing [manual challenge](https://ktane.timwi.de/More/FAQs.html#challenge).
* Questions about [boss modules](https://ktane.timwi.de/More/FAQs.html#boss-module) are allowed as long as they satisfy the above guidelines, and are not about information that the boss module itself already requires recording (e.g. calculated digits in Forget Me Not). The boss modules should remain on Souvenir's ignore list.

The following vanilla modules are supported by Souvenir: The Button, Maze, Memory, Simon Says, Wire Sequence. These may be excluded using a setting in the ModSettings file. This file will be created the first time a Souvenir module is loaded on a bomb. After that, the mod settings may be edited using a text editor or the [Mod Selector](https://steamcommunity.com/sharedfiles/filedetails/?id=801400247) tablet with the [Tweaks](https://steamcommunity.com/sharedfiles/filedetails/?id=1366808675) mod. Souvenir itself and other boss modules may also be excluded this way.
