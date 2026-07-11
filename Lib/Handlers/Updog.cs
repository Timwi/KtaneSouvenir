using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SUpdog
{
    [Question("What was the text on {0}?", ThreeColumns6Answers, "dog", "DOG", "dawg", "DAWG", "doge", "DOGE", "dag", "DAG", "dogg", "DOGG", "dage", "DAGE")]
    Word,

    [Question("What was the {1} color in the sequence on {0}?", ThreeColumns6Answers, "Red", "Yellow", "Orange", "Green", "Blue", "Purple", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color1,

    [Question("What was the {1} color in the sequence on {0}?", ThreeColumns3Answers, "Red", "Green", "Blue", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color2
}

public partial class SouvenirModule
{
    [Handler("Updog", "Updog", typeof(SUpdog), "Anonymous")]
    [ManualQuestion("What was the displayed word?")]
    [ManualQuestion("What were the flashing colors?")]
    private IEnumerator<SouvenirInstruction> ProcessUpdog(ModuleData module)
    {
        var comp = GetComponent(module, "UpdogScript");
        yield return WaitForSolve;
        var word = GetField<string>(comp, "_souvenirWord").Get(v => SUpdog.Word.GetQuestionAttribute().AllAnswers.Contains(v) ? null : $"Bad word {v}");
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

        yield return question(SUpdog.Word).Answers(word);
        yield return question(SUpdog.Color1, args: [Ordinal(1)]).Answers(colorName(colors[0]));
        yield return question(SUpdog.Color2, args: [Ordinal(2)]).Answers(colorName(colors[2]));
        yield return question(SUpdog.Color2, args: [Ordinal(3)]).Answers(colorName(colors[4]));
        yield return question(SUpdog.Color1, args: [Ordinal(4)]).Answers(colorName(colors[6]));
    }
}
