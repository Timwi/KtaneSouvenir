using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SColoredKeys
{
    [SouvenirQuestion("What was the displayed word in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white", TranslateAnswers = true)]
    DisplayWord,

    [SouvenirQuestion("What was the displayed word’s color in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white", TranslateAnswers = true)]
    DisplayWordColor,

    [SouvenirQuestion("What letter was on the {1} key in {0}?", ThreeColumns6Answers, Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings('A', 'Z')]
    KeyLetter,

    [SouvenirQuestion("What was the color of the {1} key in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white", Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateArguments = [true])]
    KeyColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("lgndColoredKeys", "Colored Keys", typeof(SColoredKeys), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessColoredKeys(ModuleData module)
    {
        var comp = GetComponent(module, "ColoredKeysScript");

        yield return WaitForSolve;

        var colors = GetArrayField<string>(comp, "loggingWords", isPublic: true).Get();
        var letters = GetArrayField<string>(comp, "letters", isPublic: true).Get();
        var displayWord = GetIntField(comp, "displayIndex").Get(0, colors.Length - 1);
        var displayColor = GetIntField(comp, "displayColIndex").Get(0, colors.Length - 1);
        var matsNames = GetArrayField<Material>(comp, "buttonmats", isPublic: true).Get().Select(x => x.name).ToArray();

        var btnLetter = Enumerable.Range(1, 4).Select(i => GetIntField(comp, $"b{i}LetIndex").Get(0, letters.Length - 1)).ToArray();
        var btnColor = Enumerable.Range(1, 4).Select(i => GetIntField(comp, $"b{i}ColIndex").Get(0, matsNames.Length - 1)).ToArray();

        yield return question(SColoredKeys.DisplayWord).Answers(colors[displayWord], preferredWrong: colors);
        yield return question(SColoredKeys.DisplayWordColor).Answers(colors[displayColor], preferredWrong: colors);
        yield return question(SColoredKeys.KeyLetter, args: ["top-left"]).Answers(letters[btnLetter[0]], preferredWrong: letters);
        yield return question(SColoredKeys.KeyLetter, args: ["top-right"]).Answers(letters[btnLetter[1]], preferredWrong: letters);
        yield return question(SColoredKeys.KeyLetter, args: ["bottom-left"]).Answers(letters[btnLetter[2]], preferredWrong: letters);
        yield return question(SColoredKeys.KeyLetter, args: ["bottom-right"]).Answers(letters[btnLetter[3]], preferredWrong: letters);
        yield return question(SColoredKeys.KeyColor, args: ["top-left"]).Answers(matsNames[btnColor[0]], preferredWrong: matsNames);
        yield return question(SColoredKeys.KeyColor, args: ["top-right"]).Answers(matsNames[btnColor[1]], preferredWrong: matsNames);
        yield return question(SColoredKeys.KeyColor, args: ["bottom-left"]).Answers(matsNames[btnColor[2]], preferredWrong: matsNames);
        yield return question(SColoredKeys.KeyColor, args: ["bottom-right"]).Answers(matsNames[btnColor[3]], preferredWrong: matsNames);
    }
}