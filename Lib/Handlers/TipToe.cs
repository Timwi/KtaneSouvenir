using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STipToe
{
    [SouvenirQuestion("Which of these squares was safe in row {1} in {0}?", ThreeColumns6Answers, Arguments = ["9", "10"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    SafeSquares
}

public partial class SouvenirModule
{
    [SouvenirHandler("TipToe", "Tip Toe", typeof(STipToe), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessTipToe(ModuleData module)
    {
        var comp = GetComponent(module, "Main");
        yield return WaitForSolve;

        var grid = GetField<Array>(comp, "Grid").Get();
        var rowNineSafeSquares = new List<string>();
        var rowTenSafeSquares = new List<string>();

        for (var col = 0; col < 10; col++)
        {
            if (!GetField<bool>(grid.GetValue(0, col), "Flicker").Get())
                rowTenSafeSquares.Add(((col + 1) % 10).ToString());
            if (!GetField<bool>(grid.GetValue(1, col), "Flicker").Get())
                rowNineSafeSquares.Add(((col + 1) % 10).ToString());
        }

        addQuestions(module,
            makeQuestion(Question.TipToeSafeSquares, module, formatArgs: new[] { "9" }, correctAnswers: rowNineSafeSquares.ToArray()),
            makeQuestion(Question.TipToeSafeSquares, module, formatArgs: new[] { "10" }, correctAnswers: rowTenSafeSquares.ToArray()));
    }
}