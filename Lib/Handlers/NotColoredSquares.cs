using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNotColoredSquares
{
    [SouvenirQuestion("What was the position of the square you initially pressed in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    InitialPosition
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotColoredSquaresModule", "Not Colored Squares", typeof(SNotColoredSquares), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessNotColoredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "NotColoredSquaresScript");

        yield return WaitForSolve;

        var firstPressedPosition = GetIntField(comp, "_stageOnePress").Get(min: 0, max: 15);
        addQuestion(module, Question.NotColoredSquaresInitialPosition, correctAnswers: new[] { new Coord(4, 4, firstPressedPosition) });
    }
}