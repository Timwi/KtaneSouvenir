using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEntryNumberOne
{
    [SouvenirQuestion("What was the {1} digit in the {2} number shown in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSEntryNumberOne", "Entry Number One", typeof(SEntryNumberOne), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> ProcessEntryNumberOne(ModuleData module)
    {
        var comp = GetComponent(module, "EntryNumberOneScript");
        yield return WaitForSolve;

        for (var i = 2; i <= 4; i++)
        {
            var num = GetIntField(comp, $"Num{i}").Get().ToString("00000000");
            for (var d = 0; d < 8; d++)
                yield return question(SEntryNumberOne.Digits, args: [Ordinal(d + 1), Ordinal(i)]).Answers(num[d].ToString());
        }
    }
}
