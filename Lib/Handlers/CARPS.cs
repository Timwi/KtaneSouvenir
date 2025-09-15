using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCARPS
{
    [SouvenirQuestion("What color was this cell initially in {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Black", UsesQuestionSprite = true, TranslateAnswers = true)]
    Cell
}

public partial class SouvenirModule
{
    [SouvenirHandler("caRPS", "CA-RPS", typeof(SCARPS), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessCARPS(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "carpsScript");
        var grid = GetArrayField<int[,]>(comp, "grid").Get(expectedLength: 3)[0];
        if ((grid.GetLength(0), grid.GetLength(1)) is not (8, 6))
            throw new AbandonModuleException($"Expected 8×6 array, got {grid.GetLength(0)}×{grid.GetLength(1)}");

        var niceGrid = Enumerable.Range(0, 8).SelectMany(y => Enumerable.Range(0, 6).Select(x => grid[y, x])).ToArray();
        if (niceGrid.Any(v => v is < 0 or > 3))
            throw new AbandonModuleException($"Expected all values in range 0–3. Got: {niceGrid.JoinString(" ")}");

        var colors = new[] { "Black", "Red", "Green", "Blue" };
        addQuestions(module, niceGrid.Select((c, i) => makeQuestion(Question.CARPSCell, module, questionSprite: Sprites.GenerateGridSprite(6, 8, i), correctAnswers: new[] { colors[c] })));
    }
}