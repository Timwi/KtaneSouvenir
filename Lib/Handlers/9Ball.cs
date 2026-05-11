using System;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum S9Ball
{
    [Question("What was the number of ball {1} in {0}?", ThreeColumns6Answers, ExampleAnswers = ["2", "3", "4", "5", "6", "7"], Arguments = ["A", "B", "C", "D", "E", "F", "G"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(2, 8)]
    Letters,

    [Question("What was the letter of ball {1} in {0}?", ThreeColumns6Answers, ExampleAnswers = ["A", "B", "C", "D", "E", "F"], Arguments = ["2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G")]
    Numbers,

    [Discriminator("the 9-Ball where ball {0} was {1}", Arguments = ["A", "2", "B", "3", "C", "4", "D", "5", "E", "6", "F", "7", "G", "8"], ArgumentGroupSize = 2)]
    Discriminator,
}

public partial class SouvenirModule
{
    [Handler("GSNineBall", "9-Ball", typeof(S9Ball), "Timwi")]
    [ManualQuestion("What were the numbers on each ball?")]
    private IEnumerator<SouvenirInstruction> Process9Ball(ModuleData module)
    {
        var comp = GetComponent(module, "NineBallScript");
        yield return WaitForSolve;

        var balls = GetArrayField<int>(comp, "RndBallNums").Get(expectedLength: 7);
        for (var ballIx = 0; ballIx < 7; ballIx++)
        {
            yield return new Discriminator(S9Ball.Discriminator, $"ball-{ballIx}", balls[ballIx], args: ["ABCDEFG".Substring(ballIx, 1), (balls[ballIx] + 1).ToString()]);
            yield return question(S9Ball.Letters, args: ["ABCDEFG".Substring(ballIx, 1)]).AvoidDiscriminators($"ball-{ballIx}").Answers((balls[ballIx] + 1).ToString());
            yield return question(S9Ball.Numbers, args: [(balls[ballIx] + 1).ToString()]).AvoidDiscriminators($"ball-{ballIx}").Answers("ABCDEFGH".Substring(ballIx, 1));
        }
    }
}
