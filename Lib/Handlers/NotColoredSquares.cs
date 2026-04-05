using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotColoredSquares
{
    [Question("What was the position of the square you initially pressed in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    InitialPosition
}

public partial class SouvenirModule
{
    [Handler("NotColoredSquaresModule", "Not Colored Squares", typeof(SNotColoredSquares), "Kuro")]
    [ManualQuestion("What position did you initially press?")]
    private IEnumerator<SouvenirInstruction> ProcessNotColoredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "NotColoredSquaresScript");

        yield return WaitForSolve;

        var firstPressedPosition = GetIntField(comp, "_stageOnePress").Get(min: 0, max: 15);
        yield return question(SNotColoredSquares.InitialPosition).Answers(new Coord(4, 4, firstPressedPosition));
    }
}