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
        var qs = new List<QandA>();
        for (var i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.IdentificationCrisisShape, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { shapeNames[shapes[i]] }));
            qs.Add(makeQuestion(Question.IdentificationCrisisDataset, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { datasetNames[datasets[i]] }));
        }
        addQuestions(module, qs);
    }
}