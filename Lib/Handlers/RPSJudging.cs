using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SRPSJudging
{
    [Question("What was the {2} team’s gesture in the {1} round of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, "blue", QandA.Ordinal, "red"], ArgumentGroupSize = 2, TranslateArguments = [false, true], Type = AnswerType.Sprites)]
    QGesture,

    [Discriminator("the RPS Judging where this was the {0} team’s gesture in the {1} round", UsesQuestionSprite = true, Arguments = ["blue", QandA.Ordinal, "red", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DGesture,
}

public partial class SouvenirModule
{
    [Handler("RPSJudging", "RPS Judging", typeof(SRPSJudging), "Anonymous", IsBossModule = true)]
    [ManualQuestion("What were the gestures in each round?")]
    private IEnumerator<SouvenirInstruction> ProcessRPSJudging(ModuleData module)
    {
        var comp = GetComponent(module, "RPSJudgingScript");

        while (!_noUnignoredModulesLeft)
            yield return null;

        var leftDisplays = GetListField<int>(comp, "LeftDisplays").Get(minLength: 0, validator: v => v is < 0 or > 100 ? "Expected range [0, 100]" : null);
        var rightDisplays = GetListField<int>(comp, "RightDisplays").Get(expectedLength: leftDisplays.Count, validator: v => v is < 0 or > 100 ? "Expected range [0, 100]" : null);
        var leftSprites = GetArrayField<Sprite>(comp, "SpriteLeft", isPublic: true).Get(expectedLength: 101).TranslateSprites(500f).ToArray();
        var rightSprites = GetArrayField<Sprite>(comp, "SpriteRight", isPublic: true).Get(expectedLength: 101).TranslateSprites(500f).ToArray();
        var usedLeftSprites = leftDisplays.Select(ix => leftSprites[ix]).ToArray();
        var usedRightSprites = rightDisplays.Select(ix => rightSprites[ix]).ToArray();

        if (leftDisplays.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        for (var stage = 0; stage < leftDisplays.Count; stage++)
        {
            yield return new Discriminator(SRPSJudging.DGesture, $"blue-{stage}", leftDisplays[stage],
                args: ["blue", Ordinal(stage + 1)], questionSprite: leftSprites[leftDisplays[stage]], avoidAnswers: [leftSprites[leftDisplays[stage]], rightSprites[leftDisplays[stage]]]);
            yield return new Discriminator(SRPSJudging.DGesture, $"red-{stage}", rightDisplays[stage],
                args: ["red", Ordinal(stage + 1)], questionSprite: rightSprites[rightDisplays[stage]], avoidAnswers: [leftSprites[rightDisplays[stage]], rightSprites[rightDisplays[stage]]]);

            yield return question(SRPSJudging.QGesture, args: [Ordinal(stage + 1), "blue"]).AvoidDiscriminators($"blue-{stage}")
                .Answers(leftSprites[leftDisplays[stage]], preferredWrong: usedLeftSprites, all: leftSprites);
            yield return question(SRPSJudging.QGesture, args: [Ordinal(stage + 1), "red"]).AvoidDiscriminators($"red-{stage}")
                .Answers(rightSprites[rightDisplays[stage]], preferredWrong: usedRightSprites, all: rightSprites);
        }
    }
}
