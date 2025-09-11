using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SQnA
{
    [SouvenirQuestion("What was the {1} question asked in {0}?", ThreeColumns6Answers, "WHAT", "WHEN", "WHERE", "WHO", "HOW", "WHY", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Questions
}

public partial class SouvenirModule
{
    [SouvenirHandler("q&a", "Q & A", typeof(SQnA), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessQnA(ModuleData module)
    {
        var comp = GetComponent(module, "QnA");
        var allQs = new[] { "WHAT", "WHEN", "WHERE", "WHO", "HOW", "WHY" };
        var qs = GetArrayField<string>(comp, "questions").Get(expectedLength: 6, validator: s => allQs.Contains(s) ? null : "Unknown question phrase");

        yield return WaitForSolve;

        addQuestions(module, qs.Select((q, i) => makeQuestion(Question.QnAQuestions, module, correctAnswers: new[] { q }, formatArgs: new[] { Ordinal(i + 1) })));
    }
}