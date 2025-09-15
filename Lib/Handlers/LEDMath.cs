using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLEDMath
{
    [SouvenirQuestion("What color was {1} in {0}?", TwoColumns4Answers, "Red", "Blue", "Yellow", "Green", TranslateAnswers = true, Arguments = ["LED A", "LED B", "the operator LED"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Lights
}

public partial class SouvenirModule
{
    [SouvenirHandler("lgndLEDMath", "LED Math", typeof(SLEDMath), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessLEDMath(ModuleData module)
    {
        var comp = GetComponent(module, "LEDMathScript");
        var ledA = GetIntField(comp, "ledAIndex").Get(min: 0, max: 3);
        var ledB = GetIntField(comp, "ledBIndex").Get(min: 0, max: 3);
        var ledOp = GetIntField(comp, "ledOpIndex").Get(min: 0, max: 3);

        yield return WaitForSolve;

        var ledColors = new[] { "Red", "Blue", "Green", "Yellow" };

        addQuestions(module,
            makeQuestion(Question.LEDMathLights, module, formatArgs: new[] { "LED A" }, correctAnswers: new[] { ledColors[ledA] }),
            makeQuestion(Question.LEDMathLights, module, formatArgs: new[] { "LED B" }, correctAnswers: new[] { ledColors[ledB] }),
            makeQuestion(Question.LEDMathLights, module, formatArgs: new[] { "the operator LED" }, correctAnswers: new[] { ledColors[ledOp] }));
    }
}