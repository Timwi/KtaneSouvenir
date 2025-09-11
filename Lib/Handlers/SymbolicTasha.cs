using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSymbolicTasha
{
    [SouvenirQuestion("Which button flashed {1} in the final sequence of {0}?", ThreeColumns6Answers, "Top", "Right", "Bottom", "Left", "Pink", "Green", "Yellow", "Blue", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Flashes,
    
    [SouvenirQuestion("Which symbol was on the {1} button in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "SymbolicTashaSprites", Arguments = ["top", "right", "bottom", "left", "blue", "green", "yellow", "pink"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Symbols
}

public partial class SouvenirModule
{
    [SouvenirHandler("symbolicTasha", "Symbolic Tasha", typeof(SSymbolicTasha), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSymbolicTasha(ModuleData module)
    {
        var comp = GetComponent(module, "symbolicTasha");
        yield return WaitForSolve;

        var positionNames = new[] { "Top", "Right", "Bottom", "Left" };
        var colorNames = new[] { "Pink", "Green", "Yellow", "Blue" };

        var positionNamesLc = new[] { "top", "right", "bottom", "left" };
        var colorNamesLc = new[] { "pink", "green", "yellow", "blue" };

        var cracked = GetArrayField<bool>(comp, "cracked").Get();
        var flashing = GetArrayField<int>(comp, "flashing").Get();
        var presentSymbols = GetField<Array>(comp, "presentSymbols").Get(validator: arr => arr.Length != 4 ? "expected length 4" : null).Cast<object>().Select(obj => (int) obj).ToArray();
        var buttonColors = GetField<Array>(comp, "buttonColors").Get(validator: arr => arr.Length != 4 ? "expected length 4" : null).Cast<object>().Select(obj => (int) obj).ToArray();

        var qs = new List<QandA>();
        for (var pos = 0; pos < 5; pos++)
            qs.Add(makeQuestion(Question.SymbolicTashaFlashes, module, formatArgs: new[] { Ordinal(pos + 1) },
                correctAnswers: new[] { positionNames[flashing[pos]], colorNames[buttonColors[flashing[pos]]] }));

        for (var btn = 0; btn < 4; btn++)
            if (presentSymbols[btn] < 0)
            {
                qs.Add(makeQuestion(Question.SymbolicTashaSymbols, module, formatArgs: new[] { positionNamesLc[btn] }, correctAnswers: new[] { SymbolicTashaSprites[-presentSymbols[btn] - 1] }, preferredWrongAnswers: SymbolicTashaSprites));
                qs.Add(makeQuestion(Question.SymbolicTashaSymbols, module, formatArgs: new[] { colorNamesLc[buttonColors[btn]] }, correctAnswers: new[] { SymbolicTashaSprites[-presentSymbols[btn] - 1] }, preferredWrongAnswers: SymbolicTashaSprites));
            }

        addQuestions(module, qs);
    }
}