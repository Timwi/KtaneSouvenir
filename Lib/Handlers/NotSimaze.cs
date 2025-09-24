using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotSimaze
{
    [SouvenirQuestion("Which maze was used in {0}?", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", TranslateAnswers = true)]
    Maze,

    [SouvenirQuestion("What was the starting position in {0}?", TwoColumns4Answers, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)", TranslateAnswers = true)]
    Start,

    [SouvenirQuestion("What was the goal position in {0}?", TwoColumns4Answers, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)", TranslateAnswers = true)]
    Goal
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotSimaze", "Not Simaze", typeof(SNotSimaze), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessNotSimaze(ModuleData module)
    {
        var comp = GetComponent(module, "NotSimaze");
        var fldMazeIndex = GetIntField(comp, "mazeIndex");

        var colours = SNotSimaze.Maze.GetAnswers();
        var startPositionArray = new[] { $"({colours[GetIntField(comp, "x").Get()]}, {colours[GetIntField(comp, "y").Get()]})" };

        yield return WaitForSolve;

        var goalPositionArray = new[] { $"({colours[GetIntField(comp, "goalX").Get()]}, {colours[GetIntField(comp, "goalY").Get()]})" };

        yield return question(SNotSimaze.Maze).Answers(colours[fldMazeIndex.Get()]);
        yield return question(SNotSimaze.Start).Answers(startPositionArray, preferredWrong: goalPositionArray);
        yield return question(SNotSimaze.Goal).Answers(goalPositionArray, preferredWrong: startPositionArray);
    }
}