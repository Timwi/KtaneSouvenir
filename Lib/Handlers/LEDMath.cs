using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLEDMath
{
    [Question("What color was {1} in {0}?", TwoColumns4Answers, "Red", "Blue", "Yellow", "Green", TranslateAnswers = true, Arguments = ["LED A", "LED B", "the operator LED"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Lights
}

public partial class SouvenirModule
{
    [Handler("lgndLEDMath", "LED Math", typeof(SLEDMath), "TasThiluna")]
    [ManualQuestion("What were the LED colors?")]
    private IEnumerator<SouvenirInstruction> ProcessLEDMath(ModuleData module)
    {
        var comp = GetComponent(module, "LEDMathScript");
        var ledA = GetIntField(comp, "ledAIndex").Get(min: 0, max: 3);
        var ledB = GetIntField(comp, "ledBIndex").Get(min: 0, max: 3);
        var ledOp = GetIntField(comp, "ledOpIndex").Get(min: 0, max: 3);

        yield return WaitForSolve;

        var ledColors = new[] { "Red", "Blue", "Green", "Yellow" };

        yield return question(SLEDMath.Lights, args: ["LED A"]).Answers(ledColors[ledA]);
        yield return question(SLEDMath.Lights, args: ["LED B"]).Answers(ledColors[ledB]);
        yield return question(SLEDMath.Lights, args: ["the operator LED"]).Answers(ledColors[ledOp]);
    }
}