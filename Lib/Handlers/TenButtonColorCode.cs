using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum STenButtonColorCode
{
    [SouvenirQuestion("What was the initial color of the {1} button in the {2} stage of {0}?", ThreeColumns3Answers, "red", "green", "blue", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    InitialColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("TenButtonColorCode", "Ten-Button Color Code", typeof(STenButtonColorCode), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessTenButtonColorCode(ModuleData module)
    {
        var comp = GetComponent(module, "scr_colorCode");
        var fldSolvedFirstStage = GetField<bool>(comp, "solvedFirstStage");
        var fldColors = GetArrayField<int>(comp, "prevColors");

        // Take a copy because the module modifies the same array in the second stage
        var firstStageColors = fldColors.Get(expectedLength: 10).ToArray();

        while (!fldSolvedFirstStage.Get())
            yield return new WaitForSeconds(.1f);

        var secondStageColors = fldColors.Get(expectedLength: 10);

        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "blue" };
        var stages = new[] { firstStageColors, secondStageColors };
        for (var stage = 0; stage < stages.Length; stage++)
            for (var slot = 0; slot < 10; slot++)
                yield return question(STenButtonColorCode.InitialColors, args: [Ordinal(slot + 1), Ordinal(stage + 1)]).Answers(colorNames[stages[stage][slot]]);
    }
}
