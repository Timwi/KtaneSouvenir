using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMulticoloredSwitches
{
    [SouvenirQuestion("What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?", TwoColumns4Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", TranslateAnswers = true, TranslateArguments = [false, true, true], Arguments = [QandA.Ordinal, "top", "lit", QandA.Ordinal, "bottom", "lit", QandA.Ordinal, "top", "unlit", QandA.Ordinal, "bottom", "unlit"], ArgumentGroupSize = 3)]
    LedColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("R4YMultiColoredSwitches", "Multicolored Switches", typeof(SMulticoloredSwitches), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMulticoloredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "MultiColoredSwitches");
        var fldLedsUp = GetField<Array>(comp, "LEDsUp");
        var fldLedsDown = GetField<Array>(comp, "LEDsDown");

        yield return WaitForSolve;

        var ledsUp = fldLedsUp.Get(validator: arr => arr.Length != 5 ? "expected length 5" : null);
        var ledsDown = fldLedsDown.Get(validator: arr => arr.Length != 5 ? "expected length 5" : null);

        var fldCharColor1 = GetField<char>(ledsUp.GetValue(0), "CharColor1", isPublic: true);
        var fldCharColor2 = GetField<char>(ledsUp.GetValue(0), "CharColor2", isPublic: true);

        var upColors = Enumerable.Range(0, 5).Select(i => new[] { fldCharColor1.GetFrom(ledsUp.GetValue(i)), fldCharColor2.GetFrom(ledsUp.GetValue(i)) }).ToArray();
        var downColors = Enumerable.Range(0, 5).Select(i => new[] { fldCharColor1.GetFrom(ledsDown.GetValue(i)), fldCharColor2.GetFrom(ledsDown.GetValue(i)) }).ToArray();

        var colorNames = new[] { "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white" };
        var colorChars = "KRGYBMCW";
        for (var upDown = 0; upDown < 2; upDown++)
            for (var cycle = 0; cycle < 2; cycle++)
                for (var led = 0; led < 5; led++)
                    yield return question(SMulticoloredSwitches.LedColor, args: [Ordinal(led + 1), upDown == 0 ? "top" : "bottom", cycle == 0 ? "lit" : "unlit"]).Answers(colorNames[colorChars.IndexOf((upDown == 0 ? upColors : downColors)[led][cycle])]);
    }
}