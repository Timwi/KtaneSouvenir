using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDuck
{
    [SouvenirQuestion("What was the color of the curtain in {0}?", TwoColumns4Answers, "blue", "yellow", "green", "orange", "red", TranslateAnswers = true)]
    CurtainColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("theDuck", "Duck", typeof(SDuck), "Kuro", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessDuck(ModuleData module)
    {
        var comp = GetComponent(module, "theDuckScript");

        yield return WaitForSolve;

        var colorNames = new[] { "blue", "yellow", "green", "orange", "red" };
        var curtainColor = colorNames[GetIntField(comp, "curtainColor").Get(min: 0, max: 4)];

        addQuestions(
            module,
            makeQuestion(Question.DuckCurtainColor, module, correctAnswers: new[] { curtainColor })
        );
    }
}