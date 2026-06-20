using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNeutralization
{
    [Question("What was the acid’s color in {0}?", TwoColumns4Answers, "Yellow", "Green", "Red", "Blue", TranslateAnswers = true)]
    QColor,

    [Question("What was the acid’s volume in {0}?", TwoColumns4Answers, "5", "10", "15", "20")]
    QVolume,

    [Discriminator("the Neutralization whose acid color was {0}", Arguments = ["yellow", "green", "red", "blue"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DColor,

    [Discriminator("the Neutralization whose acid volume was {0}", Arguments = ["5", "10", "15", "20"], ArgumentGroupSize = 1)]
    DVolume
}

public partial class SouvenirModule
{
    [Handler("neutralization", "Neutralization", typeof(SNeutralization), "Timwi")]
    [ManualQuestion("What was the acid’s color/volume?")]
    private IEnumerator<SouvenirInstruction> ProcessNeutralization(ModuleData module)
    {
        var comp = GetComponent(module, "neutralization");

        yield return WaitForActivate;

        var acidType = GetIntField(comp, "acidType").Get(min: 0, max: 3);
        var acidVol = GetIntField(comp, "acidVol").Get(av => av < 5 || av > 20 || av % 5 != 0 ? "unexpected acid volume" : null);
        var textMeshes = GetArrayField<TextMesh>(comp, "Text", isPublic: true).Get(expectedLength: 3);

        yield return WaitForSolve;

        // Hide the colorblind text, the selected base, and the drop count
        var colorText = GetField<GameObject>(comp, "colorText", isPublic: true).Get(nullAllowed: true);
        colorText?.SetActive(false);
        textMeshes[0].text = "";
        textMeshes[1].text = "";

        yield return question(SNeutralization.QColor).AvoidDiscriminators(SNeutralization.DColor).Answers(new[] { "Yellow", "Green", "Red", "Blue" }[acidType]);
        yield return question(SNeutralization.QVolume).AvoidDiscriminators(SNeutralization.DVolume).Answers(acidVol.ToString());
        yield return new Discriminator(SNeutralization.DColor, "col", acidType, args: [new[] { "yellow", "green", "red", "blue" }[acidType]]);
        yield return new Discriminator(SNeutralization.DVolume, "vol", acidVol, args: [acidVol.ToString()]);
    }
}
