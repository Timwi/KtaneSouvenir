using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSwan
{
    [Question("How many times was the system reset in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 24)]
    Resets
}

public partial class SouvenirModule
{
    [Handler("theSwan", "Swan", typeof(SSwan), "Espik", AddThe = true)]
    [ManualQuestion("How many times was the system reset?")]
    private IEnumerator<SouvenirInstruction> ProcessSwan(ModuleData module)
    {
        var comp = GetComponent(module, "theSwanScript");
        yield return WaitForSolve;

        var resetCount = GetIntField(comp, "systemResetCounter").Get();

        if (resetCount >= 25)
            yield return legitimatelyNoQuestion(module, "There were at least 25 resets.");

        yield return question(SSwan.Resets).Answers(resetCount.ToString());
    }
}
