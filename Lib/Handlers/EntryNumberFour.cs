using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEntryNumberFour
{
    [SouvenirQuestion("What was the {1} digit in the {2} number shown in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSEntryNumberFour", "Entry Number Four", typeof(SEntryNumberFour), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> ProcessEntryNumberFour(ModuleData module)
    {
        var comp = GetComponent(module, "EntryNumberFourScript");
        yield return WaitForSolve;

        for (var i = 0; i < 3; i++)
        {
            var number = GetIntField(comp, $"Num{i + 1}").Get().ToString("00000000");
            for (var d = 0; d < 8; d++)
                yield return question(SEntryNumberFour.Digits, args: [Ordinal(d + 1), Ordinal(i + 1)]).Answers(number[d].ToString());
        }
    }
}
