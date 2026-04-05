using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SColoredSwitches
{
    [Question("What was the initial position of the switches in {0}?", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings(5, 'Q', 'R')]
    InitialPosition
}

public partial class SouvenirModule
{
    [Handler("ColoredSwitchesModule", "Colored Switches", typeof(SColoredSwitches), "Timwi")]
    [ManualQuestion("What was the initial position of the switches?")]
    private IEnumerator<SouvenirInstruction> ProcessColoredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "ColoredSwitchesModule");
        var fldSwitches = GetIntField(comp, "_switchState");
        var fldSolution = GetIntField(comp, "_solutionState");

        var initial = fldSwitches.Get(0, (1 << 5) - 1);

        while (fldSolution.Get() == -1)
            yield return null;

        yield return WaitForSolve;
        yield return question(SColoredSwitches.InitialPosition).Answers(Enumerable.Range(0, 5).Select(b => (initial & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString());
    }
}