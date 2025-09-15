using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S9Ball
{
    [SouvenirQuestion("What was the number of ball {1} in {0}?", ThreeColumns6Answers, ExampleAnswers = ["2", "3", "4", "5", "6", "7"], Arguments = ["A", "B", "C", "D", "E", "F", "G"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(2, 8)]
    Letters,

    [SouvenirQuestion("What was the letter of ball {1} in {0}?", ThreeColumns6Answers, ExampleAnswers = ["A", "B", "C", "D", "E", "F"], Arguments = ["2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G")]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSNineBall", "9-Ball", typeof(S9Ball), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> Process9Ball(ModuleData module)
    {
        var comp = GetComponent(module, "NineBallScript");
        yield return WaitForSolve;

        var balls = GetArrayField<int>(comp, "RndBallNums").Get(expectedLength: 7);

        yield return question(S9Ball.Letters, args: ["A"]).Answers((balls[0] + 1).ToString());
        yield return question(S9Ball.Letters, args: ["B"]).Answers((balls[1] + 1).ToString());
        yield return question(S9Ball.Letters, args: ["C"]).Answers((balls[2] + 1).ToString());
        yield return question(S9Ball.Letters, args: ["D"]).Answers((balls[3] + 1).ToString());
        yield return question(S9Ball.Letters, args: ["E"]).Answers((balls[4] + 1).ToString());
        yield return question(S9Ball.Letters, args: ["F"]).Answers((balls[5] + 1).ToString());
        yield return question(S9Ball.Letters, args: ["G"]).Answers((balls[6] + 1).ToString());
        yield return question(S9Ball.Numbers, args: ["2"]).Answers(new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 1)]);
        yield return question(S9Ball.Numbers, args: ["3"]).Answers(new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 2)]);
        yield return question(S9Ball.Numbers, args: ["4"]).Answers(new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 3)]);
        yield return question(S9Ball.Numbers, args: ["5"]).Answers(new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 4)]);
        yield return question(S9Ball.Numbers, args: ["6"]).Answers(new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 5)]);
        yield return question(S9Ball.Numbers, args: ["7"]).Answers(new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 6)]);
        yield return question(S9Ball.Numbers, args: ["8"]).Answers(new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 7)]);
    }
}