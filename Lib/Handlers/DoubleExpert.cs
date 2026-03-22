using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDoubleExpert
{
    [SouvenirQuestion("What was the starting key number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(30, 69)]
    StartingKeyNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("doubleExpert", "Double Expert", typeof(SDoubleExpert), "Kuro")]
    [SouvenirManualQuestion("What was the starting key number?")]
    [SouvenirManualQuestion("What was the submitted word?")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleExpert(ModuleData module)
    {
        var comp = GetComponent(module, "doubleExpertScript");

        yield return WaitForSolve;

        var startingKeyNumber = GetIntField(comp, "startKeyNumber").Get(min: 30, max: 69);

        yield return question(SDoubleExpert.StartingKeyNumber).Answers(startingKeyNumber.ToString());
    }
}
