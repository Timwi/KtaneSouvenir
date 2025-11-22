using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNoiseIdentification
{
    [SouvenirQuestion("What was the first displayed Noise Type in {0}?", TwoColumns4Answers, "Crystal", "Liquid", "Moisture", "Perlin", "Voronoi", "White")]
    Noises
}

public partial class SouvenirModule
{
    [SouvenirHandler("noiseIdentification", "Noise Identification", typeof(SNoiseIdentification), "thunder725")]
    private IEnumerator<SouvenirInstruction> ProcessNoiseIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "noiseIdentificationScript");
        var noiseAnswer = GetField<string>(comp, "stageOneNoiseString", isPublic: true).Get();

        yield return WaitForSolve;

        yield return question(SNoiseIdentification.Noises).Answers(noiseAnswer);
    }
}
