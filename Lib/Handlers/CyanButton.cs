using System.Collections.Generic;
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

        for (var stage = 0; stage < 6; stage++)
            yield return question(SCyanButton.Positions, args: [Ordinal(stage + 1)]).Answers(SCyanButton.Positions.GetAttribute().AllAnswers[positions[stage]]);
    }
}