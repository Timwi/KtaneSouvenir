using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum STextField
{
    [SouvenirQuestion("What was the displayed letter in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F")]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("TextField", "Text Field", typeof(STextField), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessTextField(ModuleData module)
    {
        var comp = GetComponent(module, "TextField");

        var fldActivated = GetField<bool>(comp, "_lightson");
        while (!fldActivated.Get())
            yield return new WaitForSeconds(0.1f);

        var displayMeshes = GetArrayField<TextMesh>(comp, "ButtonLabels", true).Get(expectedLength: 12, validator: tm => tm.text == null ? "text is null" : null);
        var answer = displayMeshes.Select(x => x.text).FirstOrDefault(x => x is not "✓" and not "✗");
        var possibleAnswers = new[] { "A", "B", "C", "D", "E", "F" };

        yield return !possibleAnswers.Contains(answer)
            ? throw new AbandonModuleException($"Answer ‘{answer ?? "<null>"}’ is not of expected value ({possibleAnswers.JoinString(", ")}).")
            : (YieldInstruction) WaitForSolve;
        for (var i = 0; i < 12; i++)
            if (displayMeshes[i].text == answer)
                displayMeshes[i].text = "✓";

        addQuestion(module, Question.TextFieldDisplay, correctAnswers: new[] { answer });
    }
}