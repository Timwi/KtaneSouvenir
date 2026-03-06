using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSimonShapes
{
    [SouvenirQuestion("What was the shape submitted at the end of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Polyominoes(minCellCount: 2, maxCellCount: 5, maxWidth: 3, maxHeight: 3, requireSingleStrokePath: true)]
    SubmittedShape
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonShapesModule", "Simon Shapes", typeof(SSimonShapes), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessSimonShapes(ModuleData module)
    {
        var comp = GetComponent(module, "SimonShapesScript");
        var fldAllFinalShapes = GetListField<List<int>>(comp, "_possibleFinalShapes");

        yield return WaitForSolve;

        var solutionShape = fldAllFinalShapes.Get(minLength: 1).First();

        yield return question(SSimonShapes.SubmittedShape).Answers([Sprites.GeneratePolyominoSprite(3, solutionShape.ToArray())]);
    }
}