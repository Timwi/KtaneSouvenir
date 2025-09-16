using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPie
{
    [SouvenirQuestion("What was the {1} digit of the displayed number in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("pieModule", "Pie", typeof(SPie), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessPie(ModuleData module)
    {
        var comp = GetComponent(module, "PieScript");
        var digits = GetArrayField<string>(comp, "codes").Get(expectedLength: 5);

        yield return WaitForSolve;

        for (var ix = 0; ix < digits.Length; ix++)
            yield return question(SPie.Digits, args: [Ordinal(ix + 1)]).Answers(digits[ix], preferredWrong: digits);
    }
}