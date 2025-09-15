using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMazeseeker
{
    [SouvenirQuestion("How many walls surrounded this cell in {0}?", TwoColumns4Answers, "0", "1", "2", "3", UsesQuestionSprite = true)]
    Cell,

    [SouvenirQuestion("Where was the start in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 6)]
    Start,

    [SouvenirQuestion("Where was the goal in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 6)]
    Goal
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSMazeseeker", "Mazeseeker", typeof(SMazeseeker), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> ProcessMazeseeker(ModuleData module)
    {
        var comp = GetComponent(module, "MazeseekerScript");
        yield return WaitForSolve;

        var nums = GetField<int[,]>(comp, "Grid").Get();
        var startRow = GetField<int>(comp, "StartingRow").Get();
        var startColumn = GetField<int>(comp, "StartingColumn").Get();
        var goalRow = GetField<int>(comp, "GoalRow").Get();
        var goalColumn = GetField<int>(comp, "GoalColumn").Get();

        var qs = new List<QandA>();
        for (var i = 0; i < 36; i++)
            qs.Add(makeQuestion(Question.MazeseekerCell, module, questionSprite: Sprites.GenerateGridSprite(new Coord(6, 6, i)), correctAnswers: new[] { nums[i / 6, i % 6].ToString() }));
        qs.Add(makeQuestion(Question.MazeseekerStart, module, correctAnswers: new[] { new Coord(6, 6, startColumn, startRow) }));
        qs.Add(makeQuestion(Question.MazeseekerGoal, module, correctAnswers: new[] { new Coord(6, 6, goalColumn, goalRow) }));

        addQuestions(module, qs);
    }
}