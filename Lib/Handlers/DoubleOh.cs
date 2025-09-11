using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDoubleOh
{
    [SouvenirQuestion("Which button was the submit button in {0}?", ThreeColumns6Answers, "↕", "⇕", "↔", "⇔", "◆")]
    SubmitButton
}

public partial class SouvenirModule
{
    [SouvenirHandler("DoubleOhModule", "Double-Oh", typeof(SDoubleOh), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleOh(ModuleData module)
    {
        var comp = GetComponent(module, "DoubleOhModule");
        yield return WaitForSolve;

        var submitIndex = GetField<Array>(comp, "_functions").Get().Cast<object>().IndexOf(f => f.ToString() == "Submit");
        if (submitIndex is < 0 or > 4)
            throw new AbandonModuleException($"Submit button is at index {submitIndex} (expected 0–4).");

        addQuestion(module, Question.DoubleOhSubmitButton, correctAnswers: new[] { "↕↔⇔⇕◆".Substring(submitIndex, 1) });
    }
}