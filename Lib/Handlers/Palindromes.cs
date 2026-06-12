using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPalindromes
{
    [Question("What was the screens’s {1} digit from the right in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Numbers
}

public partial class SouvenirModule
{
    [Handler("palindromes", "Palindromes", typeof(SPalindromes), "Emik")]
    [ManualQuestion("What number was the screen display?")]
    private IEnumerator<SouvenirInstruction> ProcessPalindromes(ModuleData module)
    {
        var comp = GetComponent(module, "Palindromes");
        var fldN = GetField<string>(comp, "n");

        yield return WaitForSolve;

        for (var digitIx = 0; digitIx < 9; digitIx++)
        {
            var numString = fldN.Get();
            var digit = numString[numString.Length - 1 - digitIx];
            if (digit is < '0' or > '9')
                throw new AbandonModuleException($"The chosen character ('{digit}') was unexpected (expected a digit 0–9).");
        
            yield return question(SPalindromes.Numbers, args: [Ordinal(digitIx + 1)]).Answers(digit.ToString());
        }
    }
}
