using System.Collections.Generic;
using Souvenir;
using UnityEngine;

public enum SouvPentabutton
{
    [SouvenirQuestion("What was the base colour in {0}?", AnswerLayout.TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White", TranslateAnswers = true)]
    QBaseColor,

    [SouvenirDiscriminator("the Pentabutton labelled “{0}”", Arguments = ["PRESS", "PENTA", "PUNCH"], ArgumentGroupSize = 1)]
    DLabel
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSPentabutton", "Pentabutton", typeof(SouvPentabutton), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessPentabutton(ModuleData module)
    {
        var comp = GetComponent(module, "PentabuttonScript");

        var label = GetField<TextMesh>(comp, "Label", isPublic: true).Get().text;
        yield return new Discriminator(SouvPentabutton.DLabel, "Label", label, args: [label.ToUpperInvariant()]);

        yield return WaitForSolve;

        var colors = new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White" };
        var ans = GetField<int>(comp, "RndColour").Get(i => i is < 0 or > 6 ? $"Unknown color index {i}" : null);
        yield return question(SouvPentabutton.QBaseColor).Answers(colors[ans]);
    }
}
