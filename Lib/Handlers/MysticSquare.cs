using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMysticSquare
{
    [SouvenirQuestion("What digit was initially in the center in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 8)]
    CenterTile
}

public partial class SouvenirModule
{
    [SouvenirHandler("MysticSquareModule", "Mystic Square", typeof(SMysticSquare), "Quinn Wuest")]
    [SouvenirManualQuestion("What digit was initially in the center?")]
    private IEnumerator<SouvenirInstruction> ProcessMysticSquare(ModuleData module)
    {
        var comp = GetComponent(module, "MysticSquareModule");
        var centerTile = GetArrayField<int>(comp, "_field").Get()[4];

        yield return WaitForSolve;

        if (centerTile == 0)
            yield return legitimatelyNoQuestion(module, "The center tile is blank, and therefore is not needed for calculation.");
        yield return question(SMysticSquare.CenterTile).Answers(centerTile.ToString());
    }
}