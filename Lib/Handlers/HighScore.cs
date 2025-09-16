using System;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SHighScore
{
    [SouvenirQuestion("What was the position of the player in {0}?", TwoColumns4Answers, "1st", "2nd", "3rd", "4th", "5th")]
    Position,

    [SouvenirQuestion("What was the score of the player in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(1750, 999990, 10)]
    Score
}

public partial class SouvenirModule
{
    [SouvenirHandler("ksmHighScore", "High Score", typeof(SHighScore), "Hawker", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessHighScore(ModuleData module)
    {
        var comp = GetComponent(module, "HighScore");
        yield return WaitForSolve;

        var highScores = GetField<Array>(comp, "highScores").Get();
        var fldScore = GetIntField(highScores.GetValue(0), "score", isPublic: true);

        var playerPosition = GetIntField(comp, "entryNum").Get();
        var playerScore = fldScore.GetFrom(highScores.GetValue(playerPosition));

        var stringPos = playerPosition switch
        {
            0 => "1st",
            1 => "2nd",
            2 => "3rd",
            3 => "4th",
            _ => "5th"
        };

        yield return question(SHighScore.Position).Answers(stringPos);
        yield return question(SHighScore.Score).Answers("" + playerScore);
    }
}