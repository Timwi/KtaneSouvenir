using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMandNs
{
    [SouvenirQuestion("What color was the text on the {1} button in {0}?", ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors,

    [SouvenirQuestion("What was the text on the correct button in {0}?", ThreeColumns6Answers)]
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
        var qs = new List<QandA>();
        for (var i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.MandNsColors, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colorNames[colors[i]] }));
        qs.Add(makeQuestion(Question.MandNsLabel, module, correctAnswers: new[] { labels[solution] }, preferredWrongAnswers: labels));
        addQuestions(module, qs);
    }
}