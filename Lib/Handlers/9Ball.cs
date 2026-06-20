using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum S9Ball
{
    [Question("What was the number of this ball in {0}?", ThreeColumns6Answers, ExampleAnswers = ["2", "3", "4", "5", "6", "7"], QuestionExtraType = InfoType.Sprites)]
    [AnswerGenerator.Integers(2, 8)]
    QPositions,

    [Question("Which ball was ball {1} in {0}?", ThreeColumns6Answers, Arguments = ["2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1, AnswerType = InfoType.Sprites, SpriteFieldName = "NineBallSprites")]
    QNumbers,

    [Discriminator("the 9-Ball where ball {0} was here", Arguments = ["2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1, QuestionExtraType = InfoType.Sprites)]
    Discriminator
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
            yield return new Discriminator(S9Ball.Discriminator, $"ball-{ballIx}", balls[ballIx], args: [(balls[ballIx] + 1).ToString()], questionExtra: NineBallSprites[ballIx]);
            yield return question(S9Ball.QPositions, questionExtra: NineBallSprites[ballIx]).AvoidDiscriminators($"ball-{ballIx}").Answers((balls[ballIx] + 1).ToString());
            yield return question(S9Ball.QNumbers, args: [(balls[ballIx] + 1).ToString()]).AvoidDiscriminators($"ball-{ballIx}").Answers(NineBallSprites[ballIx]);
        }
    }
}
