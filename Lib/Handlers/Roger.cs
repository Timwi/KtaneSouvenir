using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRoger
{
    [Question("What four-digit number was given in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9999, "0000")]
    Seed
}

public partial class SouvenirModule
{
    [Handler("roger", "Roger", typeof(SRoger), "BigCrunch22")]
    [ManualQuestion("What four-digit number was given?")]
    private IEnumerator<SouvenirInstruction> ProcessRoger(ModuleData module)
    {
        var comp = GetComponent(module, "rogerScript");
        yield return WaitForSolve;

        var seededAnswer = GetField<int>(comp, "seed").Get().ToString().PadLeft(4, '0');
        yield return question(SRoger.Seed).Answers(seededAnswer);
    }
}