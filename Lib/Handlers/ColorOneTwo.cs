using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SColorOneTwo
{
    [Question("What color was the {1} LED in {0}?", TwoColumns4Answers, "Red", "Blue", "Green", "Yellow", TranslateAnswers = true, Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Color
}

public partial class SouvenirModule
{
    [Handler("colorOneTwo", "Color One Two", typeof(SColorOneTwo), "Anonymous")]
    [ManualQuestion("What colors were the LEDs?")]
    private IEnumerator<SouvenirInstruction> ProcessColorOneTwo(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "colorOneTwoScript");
        var left = GetIntField(comp, "leftLEDColor").Get(0, 3);
        var right = GetIntField(comp, "rightLEDColor").Get(0, 3);
        var colors = new[] { "Red", "Blue", "Green", "Yellow" };
        yield return question(SColorOneTwo.Color, args: ["left"]).Answers(colors[left]);
        yield return question(SColorOneTwo.Color, args: ["right"]).Answers(colors[right]);
    }
}