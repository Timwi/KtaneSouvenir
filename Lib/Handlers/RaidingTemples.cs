using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRaidingTemples
{
    [SouvenirQuestion("How many jewels were in the starting common pool in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 10)]
    StartingCommonPool
}

public partial class SouvenirModule
{
    [SouvenirHandler("raidingTemples", "Raiding Temples", typeof(SRaidingTemples), "GoodHood")]
    private IEnumerator<SouvenirInstruction> ProcessRaidingTemples(ModuleData module)
    {
        var comp = GetComponent(module, "raidingTemplesScript");
        yield return WaitForSolve;

        var startingCommonPool = GetField<int>(comp, "startingCommonPool");
        var commonPoolText = GetField<TextMesh>(comp, "commonPoolText", isPublic: true).Get();

        commonPoolText.text = "";
        addQuestion(module, Question.RaidingTemplesStartingCommonPool, correctAnswers: new[] { startingCommonPool.Get().ToString() });
    }
}