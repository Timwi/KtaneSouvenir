using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDirectionalButton
{
    [Question("What color was the button in the {1} stage of {0}?", ThreeColumns3Answers, "Red", "Blue", "White", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Color,

    [Question("What label was on the button in the {1} stage of {0}?", TwoColumns2Answers, "Abort", "Detonate", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Label
}

public partial class SouvenirModule
{
    [Handler("directionalButton", "Directional Button", typeof(SDirectionalButton), "Espik")]
    [ManualQuestion("What were the button's color and label in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessDirectionalButton(ModuleData module)
    {
        var comp = GetComponent(module, "DirectrionalButtonScripty");
        var fldStage = GetIntField(comp, "Stage");

        var fldButtonColor = GetIntField(comp, "a");
        var fldButtonLabel = GetIntField(comp, "b");

        var allColors = new int[5];
        var allLabels = new int[5];

        var possibleColors = SDirectionalButton.Color.GetAnswers();
        var possibleLabels = SDirectionalButton.Label.GetAnswers();

        while (module.Unsolved)
        {
            var stage = fldStage.Get() - 1;

            allColors[stage] = fldButtonColor.Get();
            allLabels[stage] = fldButtonLabel.Get();

            yield return null;
        }

        for (var i = 0; i < 4; i++) // Don't ask about the fifth stage since the color stays on the module
            yield return question(SDirectionalButton.Color, args: [Ordinal(i + 1)]).Answers(possibleColors[allColors[i]].ToString());

        for (var i = 0; i < 5; i++)
            yield return question(SDirectionalButton.Label, args: [Ordinal(i + 1)]).Answers(possibleLabels[allLabels[i]].ToString());
    }
}
