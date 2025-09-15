using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SVectors
{
    [SouvenirQuestion("What was the color of the {1} vector in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["first", "second", "third", "only"], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("vectorsModule", "Vectors", typeof(SVectors), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessVectors(ModuleData module)
    {
        var comp = GetComponent(module, "VectorsScript");

        yield return WaitForSolve;

        var colorsName = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple" };
        var vectorCount = GetIntField(comp, "vectorct").Get(min: 1, max: 3);
        var colors = GetArrayField<string>(comp, "colors").Get(expectedLength: 24, nullContentAllowed: true);
        var pickedVectors = GetArrayField<int>(comp, "vectorsPicked").Get(expectedLength: 3, validator: v => v < 0 || v >= colors.Length ? $"expected range 0–{colors.Length - 1}" : null);
        var nullIx = pickedVectors.Take(vectorCount).IndexOf(ix => colors[ix] == null);
        if (nullIx != -1)
            throw new AbandonModuleException($"‘colors[{pickedVectors[nullIx]}]’ was null; ‘pickedVectors’ = [{pickedVectors.JoinString(", ")}]");

        for (var i = 0; i < vectorCount; i++)
            if (!colorsName.Contains(colors[pickedVectors[i]]))
                throw new AbandonModuleException($"‘colors[{pickedVectors[i]}]’ pointed to illegal color “{colors[pickedVectors[i]]}” (colors=[{colors.JoinString(", ")}], pickedVectors=[{pickedVectors.JoinString(", ")}], index {i}).");

        var qs = new List<QandA>();
        for (var i = 0; i < vectorCount; i++)
            qs.Add(makeQuestion(Question.VectorsColors, module, formatArgs: new[] { vectorCount == 1 ? "only" : Ordinal(i + 1) }, correctAnswers: new[] { colors[pickedVectors[i]] }));
        addQuestions(module, qs);
    }
}