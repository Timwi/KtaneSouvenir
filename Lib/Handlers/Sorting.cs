using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSorting
{
    [SouvenirQuestion("What positions were the last swap used to solve {0}?", ThreeColumns6Answers, "1 & 2", "1 & 3", "1 & 4", "1 & 5", "2 & 3", "2 & 4", "2 & 5", "3 & 4", "3 & 5", "4 & 5")]
    LastSwap
}

public partial class SouvenirModule
{
    [SouvenirHandler("sorting", "Sorting", typeof(SSorting), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessSorting(ModuleData module)
    {
        var comp = GetComponent(module, "Sorting");
        yield return WaitForSolve;

        var lastSwap = GetField<byte>(comp, "swapButtons").Get();
        if (lastSwap % 10 == 0 || lastSwap % 10 > 5 || lastSwap / 10 == 0 || lastSwap / 10 > 5 || lastSwap / 10 == lastSwap % 10)
            throw new AbandonModuleException($"‘swap’ has unexpected value (expected two digit number, each with a unique digit from 1-5): {lastSwap}");

        addQuestion(module, Question.SortingLastSwap, correctAnswers: new[] { lastSwap.ToString().Insert(1, " & ") });
    }
}