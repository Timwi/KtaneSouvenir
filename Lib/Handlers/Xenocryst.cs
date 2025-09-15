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

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };
        for (var i = 0; i < 10; i++)
            yield return question(SXenocryst.Question, args: [Ordinal(i + 1)]).Answers(colorNames[flashes[i]], preferredWrong: colorNames);
    }
}