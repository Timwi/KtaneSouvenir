using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMazeScrambler
{
    [Question("What was the starting position on {0}?", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    QStart,

    [Question("What was the goal on {0}?", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    QGoal,

    [Question("Which of these positions was a maze marking on {0}?", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    QIndicators,

    [Discriminator("the Maze Scramber where the starting position was at the {0}", Arguments = ["top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DStart,

    [Discriminator("the Maze Scramber where the goal was at the {0}", Arguments = ["top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DGoal,

    [Discriminator("the Maze Scramber with a maze marking at the {0}", Arguments = ["top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DIndicators,
}

public partial class SouvenirModule
{
    [Handler("MazeScrambler", "Maze Scrambler", typeof(SMazeScrambler), "luisdiogo98")]
    [ManualQuestion("What was the starting position?")]
    [ManualQuestion("What was the goal position?")]
    [ManualQuestion("Which positions were the maze markings?")]
    private IEnumerator<SouvenirInstruction> ProcessMazeScrambler(ModuleData module)
    {
        var comp = GetComponent(module, "MazeScrambler");

        var ind1X = GetIntField(comp, "IDX1").Get(min: 0, max: 2);
        var ind1Y = GetIntField(comp, "IDY1").Get(min: 0, max: 2);
        var ind2X = GetIntField(comp, "IDX2").Get(min: 0, max: 2);
        var ind2Y = GetIntField(comp, "IDY2").Get(min: 0, max: 2);
        var startX = GetIntField(comp, "StartX").Get(min: 0, max: 2);
        var startY = GetIntField(comp, "StartY").Get(min: 0, max: 2);
        var goalX = GetIntField(comp, "GoalX").Get(min: 0, max: 2);
        var goalY = GetIntField(comp, "GoalY").Get(min: 0, max: 2);

        yield return WaitForSolve;

        var positionNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        string start = positionNames[startY * 3 + startX];
        string goal = positionNames[goalY * 3 + goalX];
        string[] indicators = [positionNames[ind1Y * 3 + ind1X], positionNames[ind2Y * 3 + ind2X]];

        yield return new Discriminator(SMazeScrambler.DStart, $"mzsc-start", start, args: [start]);
        yield return question(SMazeScrambler.QStart)
            .AvoidDiscriminators($"mzsc-start")
            .Answers(start, preferredWrong: [goal]);

        yield return new Discriminator(SMazeScrambler.DGoal, $"mzsc-goal", goal, args: [goal]);
        yield return question(SMazeScrambler.QGoal)
            .AvoidDiscriminators($"mzsc-goal")
            .Answers(goal, preferredWrong: [start]);

        foreach (var ind in indicators)
            yield return new Discriminator(SMazeScrambler.DIndicators, $"mzsc-ind-{ind}", ind, args: [ind], avoidAnswers: [ind]);
        yield return question(SMazeScrambler.QIndicators)
            .Answers([indicators[0], indicators[1]], preferredWrong: positionNames);
    }
}