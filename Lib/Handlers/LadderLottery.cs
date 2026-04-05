using System;
using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLadderLottery
{
    [Question("Which light was on in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "LadderLotterySprites")]
    LightOn
}

public partial class SouvenirModule
{
    [Handler("ladderLottery", "Ladder Lottery", typeof(SLadderLottery), "Hawker")]
    [ManualQuestion("Which light was on?")]
    private IEnumerator<SouvenirInstruction> ProcessLadderLottery(ModuleData module)
    {
        var comp = GetComponent(module, "LadderLottery");
        var fldPoint = GetField<object>(comp, "_startingPoint");

        yield return WaitForSolve;

        int startingPoint;
        try
        {
            startingPoint = (int) fldPoint.Get();
            if (startingPoint is not >= 0 or not < 4)
                throw new AbandonModuleException($"‘_startingPoint’ was {startingPoint} but expected 0–4.");
        }
        catch (InvalidCastException)
        {
            throw new AbandonModuleException($"‘_startingPoint’ was not castable to int.");
        }
        yield return question(SLadderLottery.LightOn).Answers(LadderLotterySprites[startingPoint]);
    }
}
