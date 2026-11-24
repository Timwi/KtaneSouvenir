using System;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum S3DerMaze
{
    [Question("What were the markings in {0}?", ThreeColumns6Answers, "ABC", "ABH", "ABK", "ACH", "ACK", "AHK", "BCH", "BCK", "BHK", "CHK")]
    QMarkings,

    [Question("What was the direction in {0}?", TwoColumns4Answers, "North", "South", "West", "East", "Up", "Down", TranslateAnswers = true)]
    QBearing,

    [Discriminator("the 3Der Maze whose markings were {0}", Arguments = ["ABC", "ABH", "ABK", "ACH", "ACK", "AHK", "BCH", "BCK", "BHK", "CHK"], ArgumentGroupSize = 1)]
    DMarkings,

    [Discriminator("the 3Der Maze whose direction was {0}", Arguments = ["North", "South", "West", "East", "Up", "Down"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DBearing,
}

public partial class SouvenirModule
{
    [Handler("3DerMaze", "3Der Maze", typeof(S3DerMaze), "MykiiVT")]
    [ManualQuestion("What were the markings?")]
    [ManualQuestion("What was the direction?")]
    private IEnumerator<SouvenirInstruction> Process3DerMaze(ModuleData module)
    {
        var comp = GetComponent(module, "MykiMazeScript");

        yield return WaitForActivate;

        var map = GetField<string[]>(comp, "souvenirList").Get();
        var correctMarkings = $"{map[0]}{map[1]}{map[2]}";

        yield return WaitForSolve;
        var cardinal = new[] { "North", "East", "South", "West", "Up", "Down" }[Array.IndexOf(["N", "E", "S", "W", "U", "D"], map[3].ToString())];
        yield return new Discriminator(S3DerMaze.DMarkings, "markings", correctMarkings, args: [correctMarkings]);
        yield return new Discriminator(S3DerMaze.DBearing, "cardinal", cardinal, args: [cardinal]);
        yield return question(S3DerMaze.QMarkings).AvoidDiscriminators(S3DerMaze.DMarkings).Answers(correctMarkings);
        yield return question(S3DerMaze.QBearing).AvoidDiscriminators(S3DerMaze.DBearing).Answers(cardinal);
    }
}
