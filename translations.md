# Translations

## Adding a new language to Souvenir

You may wish to look at an existing translation file to follow along with as an example.

1. Create a file named `TranslationXX.cs` in the `Lib` folder, where `XX` is the relevant language code, and import the `System.Collections.Generic` namespace (it will be needed later). Create a new class called `Translation_xx`, inheriting from `Translation`, in the `Souvenir` namespace:

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

			public override string FormatModuleName(Question question, bool addSolveCount, int numSolved)
			{
				// ...
			}

			protected override Dictionary<Question, TranslationInfo> _translations => new()
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

	- `Ordinal` should return the ordinal form of any number, e.g. "first", "second", "400th", "-40th".
	- `FormatModuleName` determines what the {0} in a question string is replaced with:
	    * If `addSolveCount` is false, this should just be the module name, including “The” (where relevant).
		* If `addSolveCount` is true, this should return a phrase meaning something like “the Mad Memory that you solved 4th”.
		* See below if you need special grammar considerations.

3. Implement the `IntroTexts` property:

	- Return an array which contains the possible texts to be shown on the module at the start of the bomb before the lights turn on.

4. In the `TranslationInfo` class, add an entry to the `AllTranslations` dictionary corresponding to the new language.

5. Special consideration to accommodate grammar and other language peculiarities:

	For many languages, `FormatModuleName` needs information pertaining to the grammar (cases, gender, pluralization, etc.etc.) of each module name.
	In such cases, declare a nested class derived from `TranslationInfo` to include the additional data:

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

			protected override Dictionary<Question, TranslationInfo_xx> _translations => new()
			{
				#region Translatable strings
				#endregion
			};
		}
	}
	```

	Note that the type of `_translations` has also changed.

	Note that `ModuleNameSpecial` and `Conjugation` are just examples. You can name them whatever is appropriate for your language.
	You can only use strings and custom enum types. Refer to the existing translations (e.g. Russian, German) to see examples.

6. **Compile `SouvenirLib.dll`.** This will automatically run a tool which updates all of the translation files. It will populate your new file with untranslated strings for every Souvenir question.

## Translating questions

Each question has an entry containing the following properties:

- `string QuestionText` — The question itself. This will contain things like {0}, {1}, etc., where information is inserted during the game. The comment above the question entry gives one example of such a replacement so you can tell which number will get replaced with which information.
- `string ModuleName` — the name of the module without “The”. This is usually what {0} is replaced with in the question. If your language uses definite articles, you might need to include an extra field in the `TranslationInfo_xx` class to accommodate this; see the German translation for an example, where it is called `ModuleNameWithThe`.
- `Dictionary<string, string> FormatArgs` — contains the strings that {1}, {2}, etc. are replaced with. Keys are in English, values are the corresponding translations.
- `Dictionary<string, string> Answers` — contains the answers the user can select. Keys are in English, values are the corresponding translations.
- `Dictionary<string, string> TranslatableStrings` — contains some additional strings sometimes used when constructing a question. Keys are in English, values are the corresponding translations.

Note:

- Answers and format arguments should be translated *only* if they appear translated on the relevant module or if they refer to something that is not written, for example a colour or position.
- Ordinals in format arguments, if using the `ordinal` method or the `QandA.Ordinal` placeholder, do not need to be translated as the `Translation.Ordinal` method you implemented earlier will handle this.

## What if a format argument or an answer is missing and can’t be translated

The translation tool (the one that automatically populates the translation file when you compile `SouvenirLib.dll`) will only include strings that are explicitly marked as translatable.
This marking is done in `Question.cs`. Let’s take `RuleOfThreeCoordinates` as an example:

```cs
[SouvenirQuestion("What was the {1} coordinate of the {2} vertex in {0}?", "Rule of Three", ThreeColumns6Answers,
	ExampleFormatArguments = new[] { "X", "red", "Y", "yellow", "Z", "blue" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { false, true })]
RuleOfThreeCoordinates,
```

The important bit is the `TranslateFormatArgs = new[] { false, true }`. It needs to follow these rules:
* The number of booleans in the array must match the `ExampleFormatArgumentGroupSize` (which in turn also matches the number of {1}, {2}, etc., not counting the {0}).
* Specify `true` for each format argument that you want translatable. In this example, the first boolean being `false` means that `X`/`Y`/`Z` are not translatable, while the second boolean being `true` means that `red`/`yellow`/`blue` are translatable.
* Use `TranslateAnswers = true` if you need the answers to be translatable. In this case, there needs to be a full list of answers (not an `ExampleAnswers` array).

## Testing translations

You can change the module language in the TestHarness with the `!1 lang xx` command. Navigate to a module with `!1 module name`.

## Using translations in the game

You can change the language in Mod Selector.

Alternatively, you will find `Souvenir-settings.txt` in the `mod-settings` folder.
In the `"Language":` field at the bottom, enter the language code (e.g. `"Language": "ja"`).

# Specific Tricky Modules

## Bosses: Concentration, Forget Any Color, Forget Anything, Forget Me Not, Forget The Colors, Forget This, Forget Us Not, Hyperforget, Kugelblitz, RPS Judging, and Sbemail Songs

Ordinarily, if a module — let’s say Coordinates — occurs twice on a bomb, Souvenir will use a phrasing like “the Coordinates you solved second”. This works for regular modules that can be solved, but for boss modules, Souvenir uses a different phrasing that refers to stages of the boss module instead.

These special phrasings are included as `TranslatableStrings`. So, whenever there are multiple instances of a boss module on a bomb, these translatable strings will be used to replace `{0}` in the question. In most cases, context should be enough to determine what `{0}`, `{1}`, etc. mean in these phrases.

Some examples of fully-formed questions using these:
- `What was the digit displayed in the third stage of the Forget Me Not which displayed a 2 in the first stage?`
- `What was the first displayed digit in the first stage of the Forget Everything whose tenth displayed digit in that stage was 4?`
- `Which figure was used during the third stage of the Forget Any Color whose cylinders in the fourth stage were Purple, Orange, White?`

## Divided Squares

"the square" is used when the square never divided, while "the correct square" is used when it has.

## Forget Any Color

This module has a few additional `TranslatableStrings`.

- `"{0}, {1}, {2}"` is used to combine the colors together to describe the module’s cylinders. This will end up like `Red, Green, Blue`.
- `"L"`, `"M"`, and `"R"` are used to describe the module’s figures. Five of these will be concatenated (e.g. `LLMMR`).

## Hidden Value, The

This module has a few `TranslatableStrings`.

- `"{0} {1}"` is used to construct answers, e.g. `Red 7`.
- The colors are normal.

## Kugelblitz

- `"{0}{1}{2}{3}{4}{5}{6}"` is used to combine the colors together to describe which particles were present. Absent particles will become nothing, while present ones will become one of `"R"`, `"O"`, `"Y"`, `"G"`, `"B"`, `"I"`, or `"v"`.
- `"None"` is used as the answer when no particles were present.
- `"R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"` is used to generate answers for red, blue, and green Kugelblitzes. Each placeholder will be filled in with a single-digit number.

## Module Manuevers

`"{0}, {1}"` is used to construct answers, e.g. `1, -2`

## Mssngv Wls

In line with the theming of the module, this question is formatted like the “missing vowels” round of the game show *Only Connect*: all vowels and spaces are removed from the question text, then some spaces are randomly inserted back in. Spaces are added such that words are between 2 and 6 letters long.

If your language can’t perform an equivalent to removing vowels:
1. Translate the question and module name normally. Do not include `\uE001` nor `\uE002` in the module name.
2. For `TranslatableStrings`, set `["AEIOU"] = ""`.

Otherwise:
1. Translate the question normally.
2. Add `\uE001` to the start of the translated module name, and add `\uE002` to the end.
3. For the `TranslatableStrings`, list out every vowel to be removed from the question text.

## White Arrows

This module has a few `TranslatableStrings`.

- `"{0} {1}"` is used to construct answers, e.g. `Red Up`.
- The colors and directions get inserted into the above format string.
