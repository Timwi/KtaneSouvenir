using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSphere
{
    [SouvenirQuestion("What was the {1} flashed color in {0}?", ThreeColumns6Answers, "red", "blue", "green", "orange", "pink", "purple", "grey", "white", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("sphere", "Sphere", typeof(SSphere), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessSphere(ModuleData module)
    {
        var comp = GetComponent(module, "theSphereScript");

        var colorNames = GetArrayField<string>(comp, "colourNames", isPublic: true).Get();
        var colors = GetArrayField<int>(comp, "selectedColourIndices", isPublic: true).Get(expectedLength: 5, validator: c => c < 0 || c >= colorNames.Length ? $"expected range 0–{colorNames.Length - 1}" : null);

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "first" }, correctAnswers: new[] { colorNames[colors[0]] }),
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "second" }, correctAnswers: new[] { colorNames[colors[1]] }),
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "third" }, correctAnswers: new[] { colorNames[colors[2]] }),
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colorNames[colors[3]] }),
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { colorNames[colors[4]] }));
    }
}