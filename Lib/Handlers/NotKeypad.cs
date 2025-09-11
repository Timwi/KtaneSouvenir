using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNotKeypad
{
    [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", ThreeColumns6Answers, "red", "orange", "yellow", "green", "cyan", "blue", "purple", "magenta", "pink", "brown", "grey", "white", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color,
    
    [SouvenirQuestion("Which symbol was on the button that flashed {1} in the final sequence in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "KeypadSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Symbol
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotKeypad", "Not Keypad", typeof(SNotKeypad), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessNotKeypad(ModuleData module)
    {
        var comp = GetComponent(module, "NotKeypad");
        var connectorComponent = GetComponent(module, "NotVanillaModulesLib.NotKeypadConnector");
        yield return WaitForSolve;

        var strings = Question.NotKeypadColor.GetAnswers();
        var colours = GetField<Array>(comp, "sequenceColours").Get(ar => ar.Cast<int>().Any(v => v <= 0 || v > strings.Length) ? "out of range" : null);
        var buttons = GetArrayField<int>(comp, "sequenceButtons").Get(expectedLength: colours.Length);
        var symbols = GetField<Array>(connectorComponent, "symbols").Get(ar => ar.Cast<int>().Any(v => v < 0 || v > KeypadSprites.Length) ? "out of range" : null);
        var sprites = symbols.Cast<int>().Select(i => KeypadSprites[i]).ToArray();

        var qs = new List<QandA>();
        for (var stage = 0; stage < colours.Length; stage++)
        {
            qs.Add(makeQuestion(Question.NotKeypadColor, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { strings[(int) colours.GetValue(stage) - 1] }));
            qs.Add(makeQuestion(Question.NotKeypadSymbol, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { KeypadSprites[(int) symbols.GetValue(buttons[stage])] }, preferredWrongAnswers: sprites));
        }
        addQuestions(module, qs);
    }
}