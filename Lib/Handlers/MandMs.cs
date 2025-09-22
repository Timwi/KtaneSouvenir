using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMandMs
{
    [SouvenirQuestion("What color was the text on the {1} button in {0}?", ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors,

    [SouvenirQuestion("What was the text on the {1} button in {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(5, 'M', 'N')]
    Labels
}

public partial class SouvenirModule
{
    [SouvenirHandler("MandMs", "M&Ms", typeof(SMandMs), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessMandMs(ModuleData module)
    {
        var comp = GetComponent(module, "MandMs");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "orange", "blue", "yellow", "brown" };
        var colors = GetArrayField<int>(comp, "buttonColors").Get();
        var labels = GetArrayField<string>(comp, "labels").Get();
        for (var i = 0; i < 5; i++)
        {
            yield return question(SMandMs.Colors, args: [Ordinal(i + 1)]).Answers(colorNames[colors[i]]);
            yield return question(SMandMs.Labels, args: [Ordinal(i + 1)]).Answers(labels[i], preferredWrong: labels);
        }
    }
}
