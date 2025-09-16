using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCrazyMaze
{
    [SouvenirQuestion("What was the {1} location in {0}?", ThreeColumns6Answers, Arguments = ["starting", "goal"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings("A-Z", "A-Z")]
    StartOrGoal
}

public partial class SouvenirModule
{
    [SouvenirHandler("CrazyMazeModule", "Crazy Maze", typeof(SCrazyMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCrazyMaze(ModuleData module)
    {
        var comp = GetComponent(module, "CrazyMazeScript");
        var fldStart = GetIntField(comp, "_startingCell");
        var fldGoal = GetIntField(comp, "_goalCell");
        var fldCellLetters = GetArrayField<string>(comp, "_cellLetters");

        yield return WaitForSolve;

        var cellLetters = fldCellLetters.Get(expectedLength: 26 * 26);
        var start = cellLetters[fldStart.Get(min: 0, max: 26 * 26 - 1)];
        var goal = cellLetters[fldGoal.Get(min: 0, max: 26 * 26 - 1)];
        yield return question(SCrazyMaze.StartOrGoal, args: ["starting"]).Answers(start, preferredWrong: [goal]);
        yield return question(SCrazyMaze.StartOrGoal, args: ["goal"]).Answers(goal, preferredWrong: [start]);
    }
}