using System.Collections.Generic;
using System.Linq;
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

        var num1 = GetIntField(comp, "Num1").Get().ToString("00000000");
        var num2 = GetIntField(comp, "Num2").Get().ToString("00000000");
        var num3 = GetIntField(comp, "Num3").Get().ToString("00000000");

        addQuestions(module, new[] { num1, num2, num3 }.SelectMany((n, i) => Enumerable.Range(0, 8).Select(d =>
            makeQuestion(SEntryNumberFour.Digits, module, formatArgs: new[] { Ordinal(d + 1), Ordinal(i + 1) }, correctAnswers: new[] { n[d].ToString() }))));
    }
}