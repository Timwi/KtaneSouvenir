using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNeutralization
{
    [SouvenirQuestion("What was the acid’s color in {0}?", TwoColumns4Answers, "Yellow", "Green", "Red", "Blue", TranslateAnswers = true)]
    Color,

    [SouvenirQuestion("What was the acid’s volume in {0}?", TwoColumns4Answers, "5", "10", "15", "20")]
    Volume
}

public partial class SouvenirModule
{
    [SouvenirHandler("neutralization", "Neutralization", typeof(SNeutralization), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessNeutralization(ModuleData module)
    {
        var comp = GetComponent(module, "neutralization");

        yield return WaitForActivate;

        var acidType = GetIntField(comp, "acidType").Get(min: 0, max: 3);
        var acidVol = GetIntField(comp, "acidVol").Get(av => av < 5 || av > 20 || av % 5 != 0 ? "unexpected acid volume" : null);

        yield return WaitForSolve;

        var colorText = GetField<GameObject>(comp, "colorText", isPublic: true).Get(nullAllowed: true);
        colorText?.SetActive(false);

        yield return question(SNeutralization.Color).Answers(new[] { "Yellow", "Green", "Red", "Blue" }[acidType]);
        yield return question(SNeutralization.Volume).Answers(acidVol.ToString());
    }
}