using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCyanButton
{
    [SouvenirQuestion("Where was the button at the {1} stage in {0}?", TwoColumns4Answers, "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Positions
}

public partial class SouvenirModule
{
    [SouvenirHandler("CyanButtonModule", "Cyan Button", typeof(SCyanButton), "Quinn Wuest", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessCyanButton(ModuleData module)
    {
        var comp = GetComponent(module, "CyanButtonScript");
        yield return WaitForSolve;
        var positions = GetArrayField<int>(comp, "_buttonPositions").Get(expectedLength: 6);

        addQuestions(module, Enumerable.Range(0, 6).Select(stage => makeQuestion(Question.CyanButtonPositions, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { Question.CyanButtonPositions.GetAttribute().AllAnswers[positions[stage]] })));
    }
}