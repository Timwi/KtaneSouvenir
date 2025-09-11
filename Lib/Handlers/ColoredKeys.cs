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
    
    [SouvenirQuestion("What letter was on the {1} key in {0}?", ThreeColumns6Answers, Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    [AnswerGenerator.Strings('A', 'Z')]
    KeyLetter,
    
    [SouvenirQuestion("What was the color of the {1} key in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white", Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = [true])]
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

        addQuestions(module,
            makeQuestion(Question.ColoredKeysDisplayWord, module, correctAnswers: new[] { colors[displayWord] }, preferredWrongAnswers: colors),
            makeQuestion(Question.ColoredKeysDisplayWordColor, module, correctAnswers: new[] { colors[displayColor] }, preferredWrongAnswers: colors),
            makeQuestion(Question.ColoredKeysKeyLetter, module, formatArgs: new[] { "top-left" }, correctAnswers: new[] { letters[btnLetter[0]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, module, formatArgs: new[] { "top-right" }, correctAnswers: new[] { letters[btnLetter[1]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, module, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { letters[btnLetter[2]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, module, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { letters[btnLetter[3]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyColor, module, formatArgs: new[] { "top-left" }, correctAnswers: new[] { matsNames[btnColor[0]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, module, formatArgs: new[] { "top-right" }, correctAnswers: new[] { matsNames[btnColor[1]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, module, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { matsNames[btnColor[2]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, module, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { matsNames[btnColor[3]] }, preferredWrongAnswers: matsNames));
    }
}