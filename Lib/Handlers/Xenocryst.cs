using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SXenocryst
{
    [Question("What was the color of the {1} flash in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Question
}

public partial class SouvenirModule
{
    [Handler("GSXenocryst", "Xenocryst", typeof(SXenocryst), "GhostSalt", AddThe = true)]
    [ManualQuestion("What was the color of each flash?")]
    private IEnumerator<SouvenirInstruction> ProcessXenocryst(ModuleData module)
    {
        var comp = GetComponent(module, "XenocrystScript");
        yield return WaitForSolve;

        var flashes = GetArrayField<int>(comp, "Outputs").Get();

        var colorNames = SXenocryst.Question.GetAnswers();
        for (var i = 0; i < 10; i++)
            yield return question(SXenocryst.Question, args: [Ordinal(i + 1)]).Answers(colorNames[flashes[i]]);
    }
}
