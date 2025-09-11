using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SReGretBFiltering
{
    [SouvenirQuestion("Which calculation was used for the {1} stage of {0}?", ThreeColumns6Answers, "+", "×", "÷", "⊻", "∧", "∨", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Operator
}

public partial class SouvenirModule
{
    [SouvenirHandler("regretbFiltering", "ReGret-B Filtering", typeof(SReGretBFiltering), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessReGretBFiltering(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "regretScript");
        var operators = GetArrayField<int>(comp, "operators").Get(expectedLength: 3, validator: v => v is > 5 or < 0 ? "Out of range [0, 5]" : null);

        addQuestions(module, operators.Select((op, i) =>
            makeQuestion(Question.ReGretBFilteringOperator, module,
                correctAnswers: new[] { Question.ReGretBFilteringOperator.GetAnswers()[op] },
                formatArgs: new[] { Ordinal(i + 1) })));
    }
}