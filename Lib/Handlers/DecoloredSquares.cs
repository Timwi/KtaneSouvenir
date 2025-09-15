using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDecoloredSquares
{
    [SouvenirQuestion("What was the starting {1} defining color in {0}?", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["column", "row"], ArgumentGroupSize = 1)]
    StartingPos
}

public partial class SouvenirModule
{
    [SouvenirHandler("DecoloredSquaresModule", "Decolored Squares", typeof(SDecoloredSquares), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessDecoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "DecoloredSquaresModule");
        yield return WaitForSolve;

        var colColor = GetField<string>(comp, "_color1").Get();
        var rowColor = GetField<string>(comp, "_color2").Get();

        addQuestions(module,
            makeQuestion(Question.DecoloredSquaresStartingPos, module, formatArgs: new[] { "column" }, correctAnswers: new[] { colColor }),
            makeQuestion(Question.DecoloredSquaresStartingPos, module, formatArgs: new[] { "row" }, correctAnswers: new[] { rowColor }));
    }
}