using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDivisibleNumbers
{
    [SouvenirQuestion("What was the {1} stage’s number in {0}?", ThreeColumns6Answers, null, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9999)]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("divisibleNumbers", "Divisible Numbers", typeof(SDivisibleNumbers), "shortc1rcuit")]
    private IEnumerator<SouvenirInstruction> ProcessDivisibleNumbers(ModuleData module)
    {
        var comp = GetComponent(module, "DivisableNumbers");
        yield return WaitForSolve;

        var finalNumbers = GetArrayField<int>(comp, "finalNumbers").Get(expectedLength: 3, validator: number => number is < 0 or > 9999 ? "expected range 0–9999" : null);
        var finalNumbersStr = finalNumbers.Select(n => n.ToString()).ToArray();
        for (var i = 0; i < finalNumbers.Length; i++)
            yield return question(SDivisibleNumbers.Numbers, args: [Ordinal(i + 1)]).Answers(finalNumbersStr[i], preferredWrong: finalNumbersStr);
    }
}