using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUncoloredSwitches
{
    [SouvenirQuestion("What was the initial state of the switches in {0}?", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings(5, 'Q', 'R')]
    InitialState,

    [SouvenirQuestion("What color was the {1} LED in reading order in {0}?", TwoColumns4Answers, "red", "green", "blue", "turquoise", "orange", "purple", "white", "black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    LedColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("R4YUncoloredSwitches", "Uncolored Switches", typeof(SUncoloredSwitches), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessUncoloredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "UncoloredSwitches");
        var fldLedColors = GetField<StringBuilder>(comp, "LEDsColorsString");
        var switchState = GetField<StringBuilder>(comp, "Switches_Current_State").Get(str => str.Length != 5 ? "expected length 5" : null);
        var switchStates = Enumerable.Range(0, 5).Select(swIx => switchState[swIx] == '1').ToArray();

        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "blue", "turquoise", "orange", "purple", "white", "black" };
        var curLedColors = fldLedColors.Get(str => str.Length != 10 ? "expected length 10" : null);
        var ledColors = Enumerable.Range(0, 10).Select(ledIx => "RGBTOPWK".IndexOf(curLedColors[ledIx])).ToArray();
        yield return question(SUncoloredSwitches.InitialState).Answers(switchStates.Select(b => b ? 'Q' : 'R').JoinString());
        for (var ledIx = 0; ledIx < 10; ledIx++)
            yield return question(SUncoloredSwitches.LedColors, args: [Ordinal(ledIx + 1)]).Answers(colorNames[ledColors[ledIx]]);
    }
}