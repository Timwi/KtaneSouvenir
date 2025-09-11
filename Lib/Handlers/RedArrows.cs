using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRedArrows
{
    [SouvenirQuestion("What was the starting number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    StartNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("redArrowsModule", "Red Arrows", typeof(SRedArrows), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessRedArrows(ModuleData module)
    {
        var comp = GetComponent(module, "RedArrowsScript");
        yield return WaitForSolve;

        addQuestion(module, Question.RedArrowsStartNumber, correctAnswers: new[] { GetIntField(comp, "start").Get(min: 0, max: 9).ToString() });
    }
}