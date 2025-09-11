using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRhythms
{
    [SouvenirQuestion("What was the color in {0}?", TwoColumns4Answers, "Blue", "Red", "Green", "Yellow", TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("MusicRhythms", "Rhythms", typeof(SRhythms), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessRhythms(ModuleData module)
    {
        var comp = GetComponent(module, "Rhythms");
        yield return WaitForSolve;

        var color = GetIntField(comp, "lightColor").Get(min: 0, max: 3);
        addQuestion(module, Question.RhythmsColor, correctAnswers: new[] { new[] { "Blue", "Red", "Green", "Yellow" }[color] });
    }
}