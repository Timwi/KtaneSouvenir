using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SColorMorse
{
    [SouvenirQuestion("What was the color of the {1} LED in {0}?", ThreeColumns6Answers, "Blue", "Green", "Orange", "Purple", "Red", "Yellow", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color,

    [SouvenirQuestion("What character was flashed by the {1} LED in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-9A-Z")]
    Character
}

public partial class SouvenirModule
{
    [SouvenirHandler("ColorMorseModule", "Color Morse", typeof(SColorMorse), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessColorMorse(ModuleData module)
    {
        var comp = GetComponent(module, "ColorMorseModule");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        yield return WaitForSolve;

        var numbers = GetArrayField<int>(comp, "Numbers").Get(expectedLength: 3);
        var colorNames = GetArrayField<string>(comp, "ColorNames", isPublic: true).Get();
        var colors = GetArrayField<int>(comp, "Colors").Get(expectedLength: 3, validator: c => c < 0 || c >= colorNames.Length ? "out of range" : null);

        var flashedColorNames = colors.Select(c => colorNames[c].Substring(0, 1) + colorNames[c].Substring(1).ToLowerInvariant()).ToArray();
        var flashedCharacters = numbers.Select(num => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(num, 1)).ToArray();

        addQuestions(module, Enumerable.Range(0, 3).SelectMany(ix => Ut.NewArray(
             makeQuestion(Question.ColorMorseColor, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { flashedColorNames[ix] }, preferredWrongAnswers: flashedColorNames),
             makeQuestion(Question.ColorMorseCharacter, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { flashedCharacters[ix] }, preferredWrongAnswers: flashedCharacters))));
    }
}