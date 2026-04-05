using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SYellowArrows
{
    [Question("What was the starting row letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    StartingRow
}

public partial class SouvenirModule
{
    [Handler("yellowArrowsModule", "Yellow Arrows", typeof(SYellowArrows), "kavinkul")]
    [ManualQuestion("What was the starting row letter?")]
    private IEnumerator<SouvenirInstruction> ProcessYellowArrows(ModuleData module)
    {
        var comp = GetComponent(module, "YellowArrowsScript");
        yield return WaitForSolve;

        var letterIndex = GetIntField(comp, "_displayedLetterIx").Get(min: 0, max: 25);
        yield return question(SYellowArrows.StartingRow).Answers(((char) ('A' + letterIndex)).ToString());
    }
}