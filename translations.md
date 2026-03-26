# Translations

## Adding a new language to Souvenir

You may wish to look at an existing translation file to follow along with as an example.

1. Create a file named `TranslationXX.cs` in the `Lib` folder, where `XX` is the relevant ISO language code. Paste the following skeleton code, which creates a new class called `Translation_xx` (change the `xx` to the language code):

	```cs
	using System.Collections.Generic;

	namespace Souvenir
	{
		public class Translation_xx : TranslationBase<TranslationInfo>
		{
			public override string Ordinal(int number)
			{
				// ...
			}

			public override string FormatModuleName(SouvenirHandlerAttribute handler, bool addSolveCount, int numSolved)
			{
				// ...
			}

			protected override Dictionary<Type, TranslationInfo> _translations => new()
			{
				#region Translatable strings
				#endregion
			};

			public override string[] IntroTexts => Ut.NewArray(
				// ...
			);
		}
	}
	```

2. Implement the `Ordinal` and `FormatModuleName` methods:

	- `Ordinal` should return the ordinal form of any number, e.g. “first”, “second”, “400th”, “-40th”.
	- `FormatModuleName` determines what the `{0}` in a question string is replaced with:
		* If `addSolveCount` is `false`, this should just be the module name, including “The” (where relevant).
		* If `addSolveCount` is `true`, this should return a phrase meaning something like “the Mad Memory you solved 4th”.
		* See below if you need special grammar considerations.
		* Do not worry about discriminators here.

3. Implement the `IntroTexts` property:

	- Return an array which contains the possible texts to be shown on the module at the start of the bomb before the lights turn on.

4. Go to `TranslationInfo.cs` and add an entry to the `AllTranslations` dictionary corresponding to the new language.

5. Special consideration to accommodate grammar and other language peculiarities:

	For many languages, `FormatModuleName` needs information pertaining to the grammar (cases, gender, pluralization, etc.etc.) of each module name.
	In such cases, declare a nested class derived from `TranslationInfo` to include the additional data. Here is a rough example to demonstrate the idea:

	```cs
	using System.Collections.Generic;

	namespace Souvenir
	{
		public class Translation_xx : TranslationBase<Translation_xx.TranslationInfo_xx>
		{
			public class TranslationInfo_xx : TranslationInfo
			{
				public string ModuleNameSpecial;
				public Conjugation Conjugation;
			}

			public enum Conjugation
			{
				// ...
			}

			// ...

			protected override Dictionary<Type, TranslationInfo_xx> _translations => new()
			{
				#region Translatable strings
				#endregion
			};
		}
	}
	```

	Note that the type of `_translations` has also changed.

	Note that `ModuleNameSpecial` and `Conjugation` are just examples. These additional fields can be used in your implementation of `FormatModuleName` when writing the standard discriminator, “the Module you solved first”. You should think about what information is necessary to accommodate the grammar of your language. You can only use strings and custom enum types. Refer to the existing translations (e.g. Russian, German) to see examples.

6. **Compile `SouvenirLib.dll`.** This will automatically run a tool which updates all of the translation files. It will populate your new file with untranslated strings for every Souvenir module, question and discriminator.

## Translating

Each **module** has an entry containing the following properties:

- `NeedsTranslation = true` — to help you find which entries are still untranslated or have changed since you last translated them. Once a module is fully translated, remove this line.
- `ModuleName = "..."` — the name of the module. This is usually what `{0}` is replaced with in the question if there is only one module (no discriminator), but your `FormatModuleName` implementation can use it in any way it desires.
- All the custom fields you declared in your `TranslationInfo` derived class.
- Note that these fields do not initially show up on their own, but Visual Studio’s auto-completion recognizes them.

Each **question** has an entry containing the following properties:

- `Question` — The question itself. This will contain things like `{0}`, `{1}`, etc., where information is inserted during the game. The comment above this entry gives one example of such a replacement so you can tell which number will get replaced with what kind of information.
- `Arguments` — contains the strings that `{1}`, `{2}`, etc. are replaced with. Please replace the strings *after* the `=` signs with the corresponding translation.
- `Answers` — contains the answers the user can select. Please replace the strings *after* the `=` signs with the corresponding translation.
- `Additional` — contains some additional strings sometimes used when constructing a question. These are explained further down in this document. Please replace the strings *after* the `=` signs with the corresponding translation.
- Ordinals (“first”, “second”, etc.) do not need to be translated as the `Ordinal` method you implemented earlier will handle this.

Each **discriminator** has an entry containing the following properties:

- `Discriminator` — The discriminator itself. This will contain things like `{0}`, `{1}`, etc., where information is inserted during the game. The comment above this entry gives one example of such a replacement so you can tell which number will get replaced with what kind of information, but remember that this example includes *only* the discriminator (which itself is inserted in place of `{0}` in the question).
- `Arguments` — contains the strings that `{0}`, `{1}`, etc. are replaced with. Please replace the strings *after* the `=` signs with the corresponding translation.
- `Additional` — contains some additional strings sometimes used when constructing a discriminator. These are explained further down in this document. Please replace the strings *after* the `=` signs with the corresponding translation.
- Ordinals (“first”, “second”, etc.) do not need to be translated as the `Ordinal` method you implemented earlier will handle this.

## What if an argument or an answer is missing and can’t be translated

The translation tool (the one that automatically populates the translation file when you compile `SouvenirLib.dll`) will only include strings that are explicitly marked as translatable. This marking is done in the `[SouvenirQuestion(...)]`/`[SouvenirDiscriminator(...)]` attribute. Let’s take *Rule Of Three* as an example:

```cs
[SouvenirQuestion("What was the {1} coordinate of the {2} vertex in {0}?", ThreeColumns6Answers, Arguments = ["X", "red", "Y", "yellow", "Z", "blue"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
Coordinates,
```

The important bit is the `TranslateArguments = [false, true]`. It needs to follow these rules:
* The number of booleans in the array must match the `ArgumentGroupSize` (which in turn also matches the number of `{1}`, `{2}`, etc., not counting the `{0}`).
* Specify `true` for each argument that you want translatable. In this example, the first boolean being `false` means that `X`/`Y`/`Z` are not translatable, while the second boolean being `true` means that `red`/`yellow`/`blue` are translatable.
* Use `TranslateAnswers = true` if you need the answers to be translatable. This only works if there is a full list of answers (not an `ExampleAnswers` array).

## Testing translations

You can change the module language in the TestHarness with the `!1 lang xx` command. Navigate to a module with `!1 module name`. You can then use the six answer buttons as follows:
* Button 1 (usually top-left): cycle through the modules alphabetically.
* Button 2 (usually bottom-left): cycle through the intro texts.
* Button 3 (usually top-middle): cycle through the questions for the current module.
* Button 4 (usually bottom-middle): cycle through the sets of example arguments for the current question.
* Button 5 (usually top-right): cycle through the discriminators for the current question.
* Button 6 (usually bottom-right): cycle through the sets of example arguments for the current discriminator.

## Using translations in the game

You can change the language in Mod Selector.

Alternatively, you can find `Souvenir-settings.txt` in the `mod-settings` folder. In the `"Language":` field at the bottom, enter the language code (e.g. `"Language": "ja"`).

# Special strings

## Forget Any Color

- `"{0}, {1}, {2}"` is used to combine the colors together to describe the module’s cylinders. This will end up like `Red, Green, Blue`.
- `"L"`, `"M"`, and `"R"` are used to describe the module’s figures. Five of these will be concatenated (e.g. `LLMMR`).

## Hidden Value, The

- `"{0} {1}"` is used to construct answers, e.g. `Red 7`.
- The colors are used in that construction.

## Kugelblitz

- `"{0}{1}{2}{3}{4}{5}{6}"` is used to combine the colors together to describe which particles were present. Absent particles will become nothing, while present ones will become the translation of `"R"`, `"O"`, `"Y"`, `"G"`, `"B"`, `"I"`, or `"V"`, which stand for the colors.
- `"None"` is used as the answer when no particles were present.
- `"R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"` is used to generate answers for red, blue, and green Kugelblitzes. Each placeholder will be filled in with a single-digit number.

## Module Maneuvers

`"{0}, {1}"` is used to construct answers, e.g. `1, -2`

## Mssngv Wls

In line with the theming of the module, this question is formatted like the “missing vowels” round of the game show *Only Connect*: all vowels and spaces are removed from the question text, then some spaces are randomly inserted back in. Spaces are added such that “words” are between 2 and 6 letters long.

For this purpose, a translatable string is provided in which you can specify the vowels of your language.

If this is not applicable in your language, feel free to translate `["AEIOU"]` as `""` (i.e. empty string). However, if you have a cool idea on doing something kinda similar, but don’t know how to implement it, please talk to Timwi and we’ll work something out!

## Variety

The discriminator “the Variety that has one” might need to agree grammatically with the component in question (e.g. gender), so there are separate strings provided for each component that “one” might refer to. The part after the “\uE003” shows the component for your information and is not actually part of the string. Do not include “\uE003” or the component name in your translation.

## White Arrows

- `"{0} {1}"` is used to construct answers, e.g. `Red Up`.
- The colors and directions get inserted into the above.
