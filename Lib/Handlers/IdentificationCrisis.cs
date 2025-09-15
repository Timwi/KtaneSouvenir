using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SIdentificationCrisis
{
    [SouvenirQuestion("What was the {1} shape used in {0}?", TwoColumns4Answers, "Circle", "Square", "Diamond", "Heart", "Star", "Triangle", "Pentagon", "Hexagon", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Shape,

    [SouvenirQuestion("What was the {1} identification module used in {0}?", OneColumn4Answers, "Morse Identification", "Boozleglyph Identification", "Plant Identification", "Pickup Identification", "Emotiguy Identification", "Ars Goetia Identification", "Mii Identification", "Customer Identification", "Spongebob Birthday Identification", "VTuber Identification", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Dataset
}

public partial class SouvenirModule
{
    [SouvenirHandler("identificationCrisis", "Identification Crisis", typeof(SIdentificationCrisis), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessIdentificationCrisis(ModuleData module)
    {
        var comp = GetComponent(module, "identificationCrisis");
        yield return WaitForSolve;

        var shapes = GetArrayField<int>(comp, "shapesUsed").Get();
        var datasets = GetArrayField<int>(comp, "datasetsUsed").Get();
        var shapeNames = GetStaticField<string[]>(comp.GetType(), "shapeNames").Get();
        var datasetNames = new[] { "Morse Identification", "Boozleglyph Identification", "Plant Identification", "Pickup Identification", "Emotiguy Identification", "Ars Goetia Identification", "Mii Identification", "Customer Identification", "Spongebob Birthday Identification", "VTuber Identification" };
        for (var i = 0; i < 3; i++)
        {
            yield return question(SIdentificationCrisis.Shape, args: [Ordinal(i + 1)]).Answers(shapeNames[shapes[i]]);
            yield return question(SIdentificationCrisis.Dataset, args: [Ordinal(i + 1)]).Answers(datasetNames[datasets[i]]);
        }
    }
}