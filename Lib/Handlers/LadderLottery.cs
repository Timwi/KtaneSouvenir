using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLadderLottery
{
    [SouvenirQuestion("Which light was on in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "LadderLotterySprites")]
    LightOn
}

public partial class SouvenirModule
{
    [SouvenirHandler("ladderLottery", "Ladder Lottery", typeof(SLadderLottery), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessLadderLottery(ModuleData module)
    {
        var comp = GetComponent(module, "LadderLottery");
        var fldPoint = GetField<object>(comp, "_startingPoint");

        yield return WaitForSolve;

        try
        {
            var startingPoint = (int) fldPoint.Get();
            if (startingPoint is not >= 0 or not < 4)
                throw new AbandonModuleException($"‘_startingPoint’ was {startingPoint} but expected 0–4.");
            addQuestion(module, Question.LadderLotteryLightOn, correctAnswers: new[] { LadderLotterySprites[startingPoint] });
        }
        catch (InvalidCastException)
        {
            throw new AbandonModuleException($"‘_startingPoint’ was not castable to int.");
        }
    }
}