using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHexamaze
{
    [SouvenirQuestion("What was the color of the pawn in {0}?", ThreeColumns6Answers, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink", TranslateAnswers = true)]
    PawnColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("HexamazeModule", "Hexamaze", typeof(SHexamaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessHexamaze(ModuleData module)
    {
        var comp = GetComponent(module, "HexamazeModule");
        yield return WaitForSolve;
        yield return question(SHexamaze.PawnColor).Answers(new[] { "Red", "Yellow", "Green", "Cyan", "Blue", "Pink" }[GetIntField(comp, "_pawnColor").Get(0, 5)]);
    }
}