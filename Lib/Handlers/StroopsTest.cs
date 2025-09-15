using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SStroopsTest
{
    [SouvenirQuestion("What was the {1} submitted word in {0}?", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Word,

    [SouvenirQuestion("What was the {1} submitted word’s color in {0}?", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("stroopsTest", "Stroop’s Test", typeof(SStroopsTest), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessStroopsTest(ModuleData module)
    {
        var comp = GetComponent(module, "StroopsTestScript");
        var words = GetListField<int>(comp, "wordList").Get();
        var colors = GetListField<int>(comp, "colorList").Get();
        var fldStage = GetIntField(comp, "stage");

        var usedWords = new int[] { -1, -1, -1 };
        var usedColors = new int[] { -1, -1, -1 };

        var buttons = GetArrayField<KMSelectable>(comp, "buttons", true).Get(expectedLength: 2);
        foreach (var b in buttons)
        {
            var oldInteract = b.OnInteract;
            b.OnInteract = () =>
            {
                if (module.Unsolved)
                {
                    var stage = fldStage.Get();
                    if (stage is >= 0 and < 3)
                    {
                        usedWords[stage] = words.Last();
                        usedColors[stage] = colors.Last();
                    }
                }
                return oldInteract();
            };
        }

        yield return WaitForSolve;

        if (usedWords.Any(s => s is -1) || usedColors.Any(s => s is -1))
            throw new AbandonModuleException($"A stage was somehow missed ({usedWords.Stringify()}), ({usedColors.Stringify()})");

        addQuestions(module,
            usedWords.Select((w, i) =>
                makeQuestion(Question.StroopsTestWord, module,
                    correctAnswers: new[] { Question.StroopsTestWord.GetAnswers()[w] },
                    formatArgs: new[] { Ordinal(i + 1) }))
            .Concat(usedColors.Select((c, i) =>
                makeQuestion(Question.StroopsTestColor, module,
                    correctAnswers: new[] { Question.StroopsTestColor.GetAnswers()[c] },
                    formatArgs: new[] { Ordinal(i + 1) }))));
    }
}