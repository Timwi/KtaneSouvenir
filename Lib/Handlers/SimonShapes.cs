using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonShapes
{
    [SouvenirQuestion("What was the shape submitted at the end of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "SimonShapesSprites")]
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

        // Converts the shape (aligned with the top-left corner) into a binary number
        var binaryValue = 0;
        var hOffset = solutionShape.Min(x => x % 3);
        var vOffset = solutionShape.Min(x => x / 3);
        foreach (var pos in solutionShape)
            binaryValue |= 1 << (3 * (pos / 3 - vOffset) + (pos % 3 - hOffset));

        // Every shape (in reading order of Table B) converted into binary using the above method.
        var binaryIx = Array.IndexOf(new[] { 210, 201, 61, 147, 55, 244, 409, 15, 30, 403, 7, 90, 73, 214, 11, 39, 60, 27, 313, 51, 19, 57, 59, 203, 25, 307, 211, 218, 75, 3, 94, 47, 91, 26, 9, 153 }, binaryValue);
        if (binaryIx == -1)
            throw new AbandonModuleException($"Obtained binary value {binaryValue} does not match any entry corresponding to a shape.");

        yield return question(SSimonShapes.SubmittedShape).Answers(SimonShapesSprites[binaryIx], preferredWrong: SimonShapesSprites);
    }
}