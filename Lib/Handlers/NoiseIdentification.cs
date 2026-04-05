using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNoiseIdentification
{
    [Question("What was the first displayed noise type in {0}?", TwoColumns4Answers, "Crystal", "Liquid", "Moisture", "Perlin", "Voronoi", "White")]
    Noises
}

public partial class SouvenirModule
{
    [Handler("noiseIdentification", "Noise Identification", typeof(SNoiseIdentification), "thunder725")]
    [ManualQuestion("What was the first displayed noise type?")]
    private IEnumerator<SouvenirInstruction> ProcessNoiseIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "noiseIdentificationScript");
        var noiseAnswer = GetField<string>(comp, "stageOneNoiseString", isPublic: true).Get();

        yield return WaitForSolve;

        yield return question(SNoiseIdentification.Noises).Answers(noiseAnswer);
    }
}
