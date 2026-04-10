using System;
using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDoubleExpert
{
    [Question("What was the starting key number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(30, 69)]
    StartingKeyNumber
}

public partial class SouvenirModule
{
    [Handler("doubleExpert", "Double Expert", typeof(SDoubleExpert), "Kuro")]
    [ManualQuestion("What was the starting key number?")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleExpert(ModuleData module)
    {
        var comp = GetComponent(module, "doubleExpertScript");

        yield return WaitForSolve;

        if (DateTime.Now is { Month: 4, Day: 9 }) // If it's April 9th, the module can be solved without any information
            yield return legitimatelyNoQuestion(module, "Quirk 7 applied.");

        var startingKeyNumber = GetIntField(comp, "startKeyNumber").Get(min: 30, max: 69);

        yield return question(SDoubleExpert.StartingKeyNumber).Answers(startingKeyNumber.ToString());
    }
}
