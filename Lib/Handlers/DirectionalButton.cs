using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDirectionalButton
{
    [SouvenirQuestion("How many times did you press the button in the {1} stage of {0}?", TwoColumns4Answers, "1", "2", "3", "4", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ButtonCount
}

public partial class SouvenirModule
{
    [SouvenirHandler("directionalButton", "Directional Button", typeof(SDirectionalButton), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessDirectionalButton(ModuleData module)
    {
        var comp = GetComponent(module, "DirectrionalButtonScripty");
        var fldStage = GetIntField(comp, "Stage");
        var fldCorrectPres = GetIntField(comp, "CorrectPres");

        var currentStage = 0;
        var currentCorrectPress = 0;
        var buttonPresses = new int[5];

        while (module.Unsolved)
        {
            var stage = fldStage.Get();
            var correctPress = fldCorrectPres.Get();

            if (stage != currentStage || correctPress != currentCorrectPress)
            {
                currentStage = stage;
                currentCorrectPress = correctPress;
                buttonPresses[currentStage - 1] = currentCorrectPress;
            }

            yield return null;
        }

        for (var i = 0; i < 5; i++)
            yield return question(SDirectionalButton.ButtonCount, args: [Ordinal(i + 1)]).Answers(buttonPresses[i].ToString());
    }
}