using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPalindromes
{
    [SouvenirQuestion("What was {1}’s {2} digit from the right in {0}?", ThreeColumns6Answers, TranslateArguments = [true, false], Arguments = ["X", QandA.Ordinal, "Y", QandA.Ordinal, "Z", QandA.Ordinal, "the screen", QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("palindromes", "Palindromes", typeof(SPalindromes), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessPalindromes(ModuleData module)
    {
        var comp = GetComponent(module, "Palindromes");
        var fldX = GetField<string>(comp, "x");
        var fldY = GetField<string>(comp, "y");
        var fldZ = GetField<string>(comp, "z");
        var fldN = GetField<string>(comp, "n");

        yield return WaitForSolve;

        var vars = new[] { fldN, fldX, fldY, fldZ };
        var qs = new List<QandA>();
        for (var varIx = 0; varIx < vars.Length; varIx++)
            for (var digitIx = 0; digitIx < (varIx < 2 ? 5 : 4); digitIx++)       // 5 if x or n, else 4
            {
                var numString = vars[varIx].Get();
                var digit = numString[numString.Length - 1 - digitIx];
                if (digit is < '0' or > '9')
                    throw new AbandonModuleException($"The chosen character ('{digit}') was unexpected (expected a digit 0–9).");

                var labels = new[] { "the screen", "X", "Y", "Z" };
                qs.Add(makeQuestion(Question.PalindromesNumbers, module, formatArgs: new[] { labels[varIx], Ordinal(digitIx + 1) }, correctAnswers: new[] { digit.ToString() }));
            }
        addQuestions(module, qs);
    }
}