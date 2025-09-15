using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SXenocryst
{
    [SouvenirQuestion("What was the color of the {1} flash in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Red", "Orange", "Yellow", "Green", "Blue", "Indigo"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Question
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSXenocryst", "Xenocryst", typeof(SXenocryst), "GhostSalt", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessXenocryst(ModuleData module)
    {
        var comp = GetComponent(module, "XenocrystScript");
        yield return WaitForSolve;

        var flashes = GetArrayField<int>(comp, "Outputs").Get();

        var qs = new List<QandA>();

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };
        for (var i = 0; i < 10; i++)
            qs.Add(makeQuestion(Question.Xenocryst, module,
                formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { colorNames[flashes[i]] },
                preferredWrongAnswers: colorNames));

        addQuestions(module, qs);
    }
}