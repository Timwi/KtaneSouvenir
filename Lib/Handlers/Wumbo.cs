using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWumbo
{
    [SouvenirQuestion("What was the number in {0}?", OneColumn4Answers, ExampleAnswers = ["30030", "813244863240810000", "0", "376639725", "27081081027000", "901800900"])]
    [AnswerGenerator.Wumbo]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("wumbo", "Wumbo", typeof(SWumbo), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessWumbo(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "wumboScript");
        var num = GetField<ulong>(comp, "number").Get(v => v is < 0uL or > 813244863240810000uL ? "Out of range [0, 813244863240810000]" : null);

        addQuestion(module, Question.WumboNumber, correctAnswers: new[] { num.ToString() });
    }
}