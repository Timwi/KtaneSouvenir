using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SYellowArrows
{
    [SouvenirQuestion("What was the starting row letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    StartingRow
}

public partial class SouvenirModule
{
    [SouvenirHandler("yellowArrowsModule", "Yellow Arrows", typeof(SYellowArrows), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessYellowArrows(ModuleData module)
    {
        var comp = GetComponent(module, "YellowArrowsScript");
        yield return WaitForSolve;

        var letterIndex = GetIntField(comp, "_displayedLetterIx").Get(min: 0, max: 25);
        yield return question(SYellowArrows.StartingRow).Answers(((char) ('A' + letterIndex)).ToString());
    }
}