using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPunctuationMarks
{
    [SouvenirQuestion("What was the displayed number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    DisplayedNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("punctuationMarks", "...?", typeof(SPunctuationMarks), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessPunctuationMarks(ModuleData module)
    {
        var comp = GetComponent(module, "script");

        yield return WaitForSolve;

        var number = GetIntField(comp, "memoryBankNumber").Get(min: 0, max: 99).ToString("00");
        addQuestion(module, Question.PunctuationMarksDisplayedNumber, correctAnswers: new[] { number });
    }
}