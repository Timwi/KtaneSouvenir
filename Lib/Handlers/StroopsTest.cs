using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SStroopsTest
{
    [SouvenirQuestion("What was the {1} submitted word in {0}?", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QWord,

    [SouvenirQuestion("What was the {1} submitted word’s color in {0}?", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QColor,

    [SouvenirDiscriminator("the Stroop’s Test whose {0} submitted word was “{1}”", Arguments = [QandA.Ordinal, "red", QandA.Ordinal, "yellow", QandA.Ordinal, "green", QandA.Ordinal, "blue", QandA.Ordinal, "magenta", QandA.Ordinal, "white"], ArgumentGroupSize = 2)]
    DWord,

    [SouvenirDiscriminator("the Stroop’s Test whose {0} submitted word’s color was {1}", Arguments = [QandA.Ordinal, "red", QandA.Ordinal, "yellow", QandA.Ordinal, "green", QandA.Ordinal, "blue", QandA.Ordinal, "magenta", QandA.Ordinal, "white"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DColor,
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

        // It's not wrong to conditionally yield return discriminators here because the condition is global.
        var usingColors = Bomb.GetSerialNumberNumbers().Last() % 2 == 0;

        if (usingColors)
        {
            for (var i = 0; i < usedColors.Length; i++)
                yield return new Discriminator(SStroopsTest.DColor, $"color{i}", usedColors[i], [Ordinal(i + 1), SStroopsTest.QColor.GetAnswers()[usedColors[i]].ToLowerInvariant()]);

            for (var i = 0; i < usedColors.Length; i++)
                yield return question(SStroopsTest.QColor, args: [Ordinal(i + 1)])
                    .AvoidDiscriminators($"color{i}")
                    .Answers(SStroopsTest.QColor.GetAnswers()[usedColors[i]]);
        }
        else
        {
            for (var i = 0; i < usedWords.Length; i++)
                yield return new Discriminator(SStroopsTest.DWord, $"word{i}", usedWords[i], [Ordinal(i + 1), SStroopsTest.QWord.GetAnswers()[usedWords[i]].ToLowerInvariant()]);

            for (var i = 0; i < usedWords.Length; i++)
                yield return question(SStroopsTest.QWord, args: [Ordinal(i + 1)])
                    .AvoidDiscriminators($"word{i}")
                    .Answers(SStroopsTest.QWord.GetAnswers()[usedWords[i]]);
        }
    }
}
