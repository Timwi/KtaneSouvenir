using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
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

        var strings = SNotKeypad.Color.GetAnswers();
        var colours = GetField<Array>(comp, "sequenceColours").Get(ar => ar.Cast<int>().Any(v => v <= 0 || v > strings.Length) ? "out of range" : null);
        var buttons = GetArrayField<int>(comp, "sequenceButtons").Get(expectedLength: colours.Length);
        var symbols = GetField<Array>(connectorComponent, "symbols").Get(ar => ar.Cast<int>().Any(v => v < 0 || v > KeypadSprites.Length) ? "out of range" : null);
        var sprites = symbols.Cast<int>().Select(i => KeypadSprites[i]).ToArray();
        for (var stage = 0; stage < colours.Length; stage++)
        {
            yield return question(SNotKeypad.Color, args: [Ordinal(stage + 1)]).Answers(strings[(int) colours.GetValue(stage) - 1]);
            yield return question(SNotKeypad.Symbol, args: [Ordinal(stage + 1)]).Answers(KeypadSprites[(int) symbols.GetValue(buttons[stage])], preferredWrong: sprites);
        }
    }
}