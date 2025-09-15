using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOrientationHypercube
{
    [SouvenirQuestion("What was the initial colour of the {1} face in {0}?", ThreeColumns6Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", Arguments = ["right", "left", "top", "bottom", "back", "front", "zag", "zig"], ArgumentGroupSize = 1, TranslateArguments = [true], TranslateAnswers = true)]
    InitialFaceColour,
    
    [SouvenirQuestion("What was the observer’s initial position in {0}?", TwoColumns4Answers, "front", "left", "back", "right", TranslateAnswers = true)]
    InitialObserverPosition
}

public partial class SouvenirModule
{
    [SouvenirHandler("OrientationHypercube", "Orientation Hypercube", typeof(SOrientationHypercube), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessOrientationHypercube(ModuleData module)
    {
        var comp = GetComponent(module, "OrientationHypercubeModule");
        yield return WaitForSolve;

        var initialObserverPosition = GetField<string>(comp, "_initialEyePosition").Get();
        var colourTexts = GetField<Dictionary<string, string>>(GetField<object>(comp, "_readGenerator").Get(), "_cbTexts").Get();
        var faceNames = new Dictionary<string, string>
        {
            ["+X"] = "right",
            ["-X"] = "left",
            ["+Y"] = "top",
            ["-Y"] = "bottom",
            ["+Z"] = "back",
            ["-Z"] = "front",
            ["+W"] = "zag",
            ["-W"] = "zig"
        };
        var qs = new List<QandA>();

        foreach (var key in faceNames.Keys)
            qs.Add(makeQuestion(Question.OrientationHypercubeInitialFaceColour, module, formatArgs: new[] { faceNames[key] }, correctAnswers: new[] { colourTexts[key] }));
        qs.Add(makeQuestion(Question.OrientationHypercubeInitialObserverPosition, module, correctAnswers: new[] { initialObserverPosition }));

        addQuestions(module, qs);
    }
}