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
        for (var i = 0; i < 36; i++)
            yield return question(SMazeseeker.Cell, questionSprite: Sprites.GenerateGridSprite(new Coord(6, 6, i))).Answers(nums[i / 6, i % 6].ToString());
        yield return question(SMazeseeker.Start).Answers(new Coord(6, 6, startColumn, startRow));
        yield return question(SMazeseeker.Goal).Answers(new Coord(6, 6, goalColumn, goalRow));
    }
}