using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRoleReversal
{
    [Question("What was the seed in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(0, 279935)]
    Seed
}

public partial class SouvenirModule
{
    [Handler("roleReversal", "Role Reversal", typeof(SRoleReversal), "Espik")]
    [ManualQuestion("What was the seed?")]
    private IEnumerator<SouvenirInstruction> ProcessRoleReversal(ModuleData module)
    {
        var comp = GetComponent(module, "roleReversal");
        yield return WaitForSolve;

        var seed = GetIntField(comp, "_seed").Get();
        yield return question(SRoleReversal.Seed).Answers(seed.ToString());
    }
}
