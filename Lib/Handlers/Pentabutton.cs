using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPentabutton
{
    [SouvenirQuestion("What was the base colour in {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White", TranslateAnswers = true)]
    BaseColor,

    [SouvenirDiscriminator("the Pentabutton labelled “{0}”", Arguments = ["press", "detonate", "hold", "abort", "release", "poke", "punch", "depress", "push", "select", "explode", "boom", "ignite", "escape", "colour", "penta", "button"], ArgumentGroupSize = 1)]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSPentabutton", "Pentabutton", typeof(SPentabutton), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessPentabutton(ModuleData module)
    {
        var comp = GetComponent(module, "PentabuttonScript");

        var label = GetField<TextMesh>(comp, "Label", isPublic: true).Get().text;
        yield return new Discriminator(SPentabutton.Label, "label", label, [label]);

        yield return WaitForSolve;

        var colors = new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White" };
        var ans = GetField<int>(comp, "RndColour").Get(i => i is < 0 or > 6 ? $"Unknown color index {i}" : null);
        yield return question(SPentabutton.BaseColor).Answers(colors[ans]);
    }
}
