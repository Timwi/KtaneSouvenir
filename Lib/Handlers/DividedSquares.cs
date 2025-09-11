using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDividedSquares
{
    [SouvenirQuestion("What color was {1} while pressing it in {0}?", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Black", "White", Arguments = ["the square", "the correct square"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = [true])]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("DividedSquaresModule", "Divided Squares", typeof(SDividedSquares), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessDividedSquares(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "DividedSquaresModule");
        if (GetField<int?>(comp, "_correctNumSolved").Get(nullAllowed: true) is null)
            yield return legitimatelyNoQuestion(module, "The module became solvable at any solve count (either solves > 199 or something went wrong)");

        var len = GetIntField(comp, "_sideLength").Get(1, 13);
        var b = GetIntField(comp, "_colorB").Get(0, 5);

        addQuestion(module, Question.DividedSquaresColor,
            formatArguments: new[] { len == 1 ? "the square" : "the correct square" },
            correctAnswers: new[] { Question.DividedSquaresColor.GetAnswers()[b] });
    }
}