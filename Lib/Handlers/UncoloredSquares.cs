using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SUncoloredSquares
{
    [SouvenirQuestion("What was the {1} color in reading order used in the first stage of {0}?", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    FirstStage
}

public partial class SouvenirModule
{
    [SouvenirHandler("UncoloredSquaresModule", "Uncolored Squares", typeof(SUncoloredSquares), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessUncoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "UncoloredSquaresModule");
        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.UncoloredSquaresFirstStage, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor1").Get().ToString() }),
            makeQuestion(Question.UncoloredSquaresFirstStage, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor2").Get().ToString() }));
    }
}