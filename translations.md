# Translations

## Adding a new language to Souvenir

You may wish to look at an existing translation file to follow along with as an example.

1. Create a file named `TranslationXX.cs` in the `Lib` folder, where `XX` is the relevant language code, and import the `System.Collections.Generic` namespace (it will be needed later). Create a new class called `Translation_xx`, inheriting from `Translation`, in the `Souvenir` namespace:

    ```cs
    using System.Collections.Generic;

    namespace Souvenir
    {
        public class Translation_xx : Translation
        {

        }
    }
    ```

2. Implement the `FormatModuleName` and `Ordinal` methods:

    ```cs
    public abstract string Ordinal(int number);

    public abstract string FormatModuleName(string moduleNameWithoutThe, string moduleNameWithThe, bool addSolveCount, int numSolved);
    ```

    - `Ordinal` should return the ordinal form of any number, e.g. "first", "second", "400th", "-40th".
    - `FormatModuleName` should return the module name (formatted *with* "The") if `addSolveCount` is false. Otherwise, it should include `numSolved`, e.g. "the Mad Memory that you solved 4th". Use the aforementioned `Ordinal` method.

3. Implement the `IntroTexts` property:

    ```cs
    public abstract string[] IntroTexts { get; }
    ```

    - Return an array which contains the possible texts to be shown on the module at the start of the bomb before the lights turn on.

4. In the `Translation` class, add an entry to the `AllTranslations` dictionary corresponding to the new language.

## Adding questions to an existing language

1. If the `Translations` property has not been overriden, do so now:

    ```cs
    protected override Dictionary<Question, TranslationInfo> _translations => new()
    ```

2. For each question, add an entry to `Translations` with the question itself as the key, and a `TranslationInfo` object as the value. The properties to set in this object are as follows:

    - `string QuestionText` — The question itself. Make sure to include the relevant format arguments in the correct order.
    - `string ModuleName` — the name of the module without "The".
    - `string ModuleNameWithThe` — the name of the module with "The", if applicable. If your language does not have an equivalent for "The", leave this unspecified.
    - `Dictionary<string, string> Answers` — keys are all possible answers in English, values are the corresponding translations. You must set `TranslateAnswers` to `true` in the question definition if you specify this.
    - `Dictionary<string, string> FormatArgs` — keys are all possible extra format arguments in English, values are the corresponding translations. If specified, you must also specify `TranslateFormatArgs` in the question definition to indicate which format arguments should be translated.

Note:

- Answers and format arguments should be translated *only* if they appear translated on the relevant module or if they refer to something that is not written, for example a colour or position. Otherwise, do not specify the relevant dictionaries.
- Ordinals in format arguments, if using the `ordinal` method or the `QandA.Ordinal` placeholder, do not need to be translated as the `Translation.Ordinal` method you implemented earlier will handle this.

## Testing translations

You can change the module language in the TestHarness with the `!1 lang xx` command. Navigate to a module with `!1 module name`.