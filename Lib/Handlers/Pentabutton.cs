using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPentabutton
{
    [SouvenirQuestion("What was the base colour in {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White", TranslateAnswers = true, TranslatableStrings = ["the Pentabutton labelled “{0}”"])]
    BaseColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSPentabutton", "Pentabutton", typeof(SPentabutton), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessPentabutton(ModuleData module)
    {
        var comp = GetComponent(module, "PentabuttonScript");

        var label = GetField<TextMesh>(comp, "Label", isPublic: true).Get().text;
        _pentabuttonLabels.Add(label);

        yield return WaitForSolve;

        string format = null;
        if (_pentabuttonLabels.Count(x => x == label) == 1)
            format = string.Format(translateString(Question.PentabuttonBaseColor, "the Pentabutton labelled “{0}”"), label.ToUpperInvariant());

        var colors = new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White" };
        var ans = GetField<int>(comp, "RndColour").Get(i => i is < 0 or > 6 ? $"Unknown color index {i}" : null);
        addQuestion(module, Question.PentabuttonBaseColor, formattedModuleName: format, correctAnswers: new[] { colors[ans] });
    }
}