using System.Collections.Generic;
using System.Linq;
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

        var num2 = GetIntField(comp, "Num2").Get().ToString("00000000");
        var num3 = GetIntField(comp, "Num3").Get().ToString("00000000");
        var num4 = GetIntField(comp, "Num4").Get().ToString("00000000");

        addQuestions(module, new[] { num2, num3, num4 }.SelectMany((n, i) => Enumerable.Range(0, 8).Select(d =>
            makeQuestion(Question.EntryNumberOneDigits, module, formatArgs: new[] { Ordinal(d + 1), Ordinal(i + 2) }, correctAnswers: new[] { n[d].ToString() }))));
    }
}