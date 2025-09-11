using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGrayButton
{
    [SouvenirQuestion("What was the {1} coordinate on the display in {0}?", ThreeColumns6Answers, Arguments = ["horizontal", "vertical"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    [AnswerGenerator.Integers(0, 9)]
    Coordinates
}

public partial class SouvenirModule
{
    [SouvenirHandler("GrayButtonModule", "Gray Button", typeof(SGrayButton), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessGrayButton(ModuleData module)
    {
        var comp = GetComponent(module, "GrayButtonScript");

        var text = GetField<TextMesh>(comp, "ScreenText", isPublic: true).Get();
        var m = Regex.Match(text.text, @"^(\d), (\d)$");
        yield return !m.Success ? throw new AbandonModuleException($"Unexpected text on Gray Button display: {text.text}") : (YieldInstruction) WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.GrayButtonCoordinates, module, formatArgs: new[] { "horizontal" }, correctAnswers: new[] { m.Groups[1].Value }),
            makeQuestion(Question.GrayButtonCoordinates, module, formatArgs: new[] { "vertical" }, correctAnswers: new[] { m.Groups[2].Value }));
    }
}