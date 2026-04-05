using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRaidingTemples
{
    [Question("How many jewels were in the starting common pool in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 10)]
    StartingCommonPool
}

public partial class SouvenirModule
{
    [Handler("raidingTemples", "Raiding Temples", typeof(SRaidingTemples), "GoodHood")]
    [ManualQuestion("How many jewels were initially in the common pool?")]
    private IEnumerator<SouvenirInstruction> ProcessRaidingTemples(ModuleData module)
    {
        var comp = GetComponent(module, "raidingTemplesScript");
        yield return WaitForSolve;

        var startingCommonPool = GetField<int>(comp, "startingCommonPool");
        
        yield return question(SRaidingTemples.StartingCommonPool).Answers(startingCommonPool.Get().ToString());
    }
}
