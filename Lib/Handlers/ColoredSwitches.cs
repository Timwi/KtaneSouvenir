using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SColoredSwitches
{
    [SouvenirQuestion("What was the initial position of the switches in {0}?", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings(5, 'Q', 'R')]
    InitialPosition,

    [SouvenirQuestion("What was the position of the switches when the LEDs came on in {0}?", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings(5, 'Q', 'R')]
    WhenLEDsCameOn
}

public partial class SouvenirModule
{
    [SouvenirHandler("ColoredSwitchesModule", "Colored Switches", typeof(SColoredSwitches), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessColoredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "ColoredSwitchesModule");
        var fldSwitches = GetIntField(comp, "_switchState");
        var fldSolution = GetIntField(comp, "_solutionState");

        var initial = fldSwitches.Get(0, (1 << 5) - 1);

        while (fldSolution.Get() == -1)
            yield return null;  // not waiting for .1 seconds this time to make absolutely sure we catch it before the player toggles another switch

        var afterReveal = fldSwitches.Get(0, (1 << 5) - 1);

        yield return WaitForSolve;
        yield return question(SColoredSwitches.InitialPosition).Answers(Enumerable.Range(0, 5).Select(b => (initial & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString());
        yield return question(SColoredSwitches.WhenLEDsCameOn).Answers(Enumerable.Range(0, 5).Select(b => (afterReveal & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString());
    }
}