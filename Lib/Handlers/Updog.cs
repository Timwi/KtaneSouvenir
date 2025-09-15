using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SUpdog
{
    [SouvenirQuestion("What was the text on {0}?", ThreeColumns6Answers, "dog", "DOG", "dawg", "DAWG", "doge", "DOGE", "dag", "DAG", "dogg", "DOGG", "dage", "DAGE")]
    Word,
    
    [SouvenirQuestion("What was the {1} color in the sequence on {0}?", ThreeColumns6Answers, "Red", "Yellow", "Orange", "Green", "Blue", "Purple", TranslateArguments = [true], TranslateAnswers = true, Arguments = ["first", "last"], ArgumentGroupSize = 1)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("Updog", "Updog", typeof(SUpdog), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessUpdog(ModuleData module)
    {
        var comp = GetComponent(module, "UpdogScript");
        yield return WaitForSolve;
        var word = GetField<string>(comp, "_souvenirWord").Get(v => Ut.Attributes[Question.UpdogWord].AllAnswers.Contains(v) ? null : $"Bad word {v}");
        var colors = GetArrayField<Color>(comp, "_souvenirColors").Get(expectedLength: 10);

        static string colorName(Color c) => (c.r, c.g, c.b) switch
        {
            (0.4f, 0.05f, 0.05f) => "Red",
            (0.4f, 0.3f, 0.05f) => "Orange",
            (0.5f, 0.5f, 0.05f) => "Yellow",
            (0.05f, 0.05f, 0.5f) => "Blue",
            (0.05f, 0.5f, 0.05f) => "Green",
            (0.5f, 0.05f, 0.5f) => "Purple",
            _ => throw new AbandonModuleException($"Unexpected color: {c.r}, {c.g}, {c.b}"),
        };

        var firstCol = colorName(colors[0]);
        var lastCol = colorName(colors[6]);

        addQuestions(module,
            makeQuestion(Question.UpdogWord, module, correctAnswers: new[] { word }),
            makeQuestion(Question.UpdogColor, module, correctAnswers: new[] { firstCol }, formatArgs: new[] { "first" }),
            makeQuestion(Question.UpdogColor, module, correctAnswers: new[] { lastCol }, formatArgs: new[] { "last" }));
    }
}