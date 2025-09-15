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

        addQuestions(module,
            makeQuestion(Question._9BallLetters, module, formatArgs: new[] { "A" }, correctAnswers: new[] { (balls[0] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, module, formatArgs: new[] { "B" }, correctAnswers: new[] { (balls[1] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, module, formatArgs: new[] { "C" }, correctAnswers: new[] { (balls[2] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, module, formatArgs: new[] { "D" }, correctAnswers: new[] { (balls[3] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, module, formatArgs: new[] { "E" }, correctAnswers: new[] { (balls[4] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, module, formatArgs: new[] { "F" }, correctAnswers: new[] { (balls[5] + 1).ToString() }),
            makeQuestion(Question._9BallLetters, module, formatArgs: new[] { "G" }, correctAnswers: new[] { (balls[6] + 1).ToString() }),
            makeQuestion(Question._9BallNumbers, module, formatArgs: new[] { "2" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 1)] }),
            makeQuestion(Question._9BallNumbers, module, formatArgs: new[] { "3" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 2)] }),
            makeQuestion(Question._9BallNumbers, module, formatArgs: new[] { "4" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 3)] }),
            makeQuestion(Question._9BallNumbers, module, formatArgs: new[] { "5" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 4)] }),
            makeQuestion(Question._9BallNumbers, module, formatArgs: new[] { "6" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 5)] }),
            makeQuestion(Question._9BallNumbers, module, formatArgs: new[] { "7" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 6)] }),
            makeQuestion(Question._9BallNumbers, module, formatArgs: new[] { "8" }, correctAnswers: new[] { new[] { "A", "B", "C", "D", "E", "F", "G" }[Array.IndexOf(balls, 7)] }));
    }
}