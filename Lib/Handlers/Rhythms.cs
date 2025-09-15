using System.Collections.Generic;
using Souvenir;

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
        yield return question(SRhythms.Color).Answers(new[] { "Blue", "Red", "Green", "Yellow" }[color]);
    }
}