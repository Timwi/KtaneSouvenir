using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMandNs
{
    [SouvenirQuestion("What color was the text on the {1} button in {0}?", ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors,

    [SouvenirQuestion("What was the text on the correct button in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(5, 'M', 'N')]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("MandNs", "M&Ns", typeof(SMandNs), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessMandNs(ModuleData module)
    {
        var comp = GetComponent(module, "MandNs");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "orange", "blue", "yellow", "brown" };
        var colors = GetArrayField<int>(comp, "buttonColors").Get();
        var labels = GetArrayField<string>(comp, "convertedValues").Get();
        var solution = GetIntField(comp, "solution").Get();
        for (var i = 0; i < 5; i++)
            yield return question(SMandNs.Colors, args: [Ordinal(i + 1)]).Answers(colorNames[colors[i]]);
        yield return question(SMandNs.Label).Answers(labels[solution], preferredWrong: labels);
    }
}
