using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPentabutton
{
    [Question("What was the base colour in {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White", TranslateAnswers = true)]
    BaseColor
}

public partial class SouvenirModule
{
    [Handler("GSPentabutton", "Pentabutton", typeof(SPentabutton), "Anonymous", AddThe = true)]
    [ManualQuestion("What was the base colour?")]
    private IEnumerator<SouvenirInstruction> ProcessPentabutton(ModuleData module)
    {
        var comp = GetComponent(module, "PentabuttonScript");

        yield return WaitForSolve;

        var colors = new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White" };
        var ans = GetField<int>(comp, "RndColour").Get(i => i is < 0 or > 6 ? $"Unknown color index {i}" : null);
        yield return question(SPentabutton.BaseColor).Answers(colors[ans]);
    }
}
