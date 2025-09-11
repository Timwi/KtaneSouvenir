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
        addQuestions(module, new[] { firstStageColors, secondStageColors }.SelectMany((colors, stage) => Enumerable.Range(0, 10)
            .Select(slot => makeQuestion(Question.TenButtonColorCodeInitialColors, module, formatArgs: new[] { Ordinal(slot + 1), Ordinal(stage + 1) }, correctAnswers: new[] { colorNames[colors[slot]] }))));
    }
}