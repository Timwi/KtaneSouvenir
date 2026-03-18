using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCyanButton
{
    [SouvenirQuestion("Where was the button at in the {1} stage of {0}?", TwoColumns4Answers, "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QPositions,

    [SouvenirDiscriminator("the Cyan Button where the button in the {0} stage was at the {1}", Arguments = [QandA.Ordinal, "top left", QandA.Ordinal, "top middle", QandA.Ordinal, "top right", QandA.Ordinal, "bottom left", QandA.Ordinal, "bottom middle", QandA.Ordinal, "bottom right"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DPositions
}

public partial class SouvenirModule
{
    [SouvenirHandler("CyanButtonModule", "Cyan Button", typeof(SCyanButton), "Quinn Wuest", AddThe = true)]
    [SouvenirManualQuestion("Where was the button at each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessCyanButton(ModuleData module)
    {
        var comp = GetComponent(module, "CyanButtonScript");
        yield return WaitForSolve;
        var positions = GetArrayField<int>(comp, "_buttonPositions").Get(expectedLength: 6);

        for (var stage = 0; stage < 6; stage++)
        {
            yield return new Discriminator(SCyanButton.DPositions, $"stage-{stage}", SCyanButton.QPositions.GetAnswers()[positions[stage]], args: [Ordinal(stage + 1), SCyanButton.QPositions.GetAnswers()[positions[stage]]]);
            yield return question(SCyanButton.QPositions, args: [Ordinal(stage + 1)])
                .AvoidDiscriminators($"stage-{stage}")
                .Answers(SCyanButton.QPositions.GetAnswers()[positions[stage]]);
        }
    }
}
