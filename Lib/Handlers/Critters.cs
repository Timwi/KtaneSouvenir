using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCritters
{
    [SouvenirQuestion("What was the color in {0}?", TwoColumns4Answers, "Yellow", "Pink", "Blue", "White", TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("CrittersModule", "Critters", typeof(SCritters), "Eltrick")]
    private IEnumerator<SouvenirInstruction> ProcessCritters(ModuleData module)
    {
        var comp = GetComponent(module, "CrittersScript");
        var fldColorIx = GetIntField(comp, "_randomiser");

        yield return WaitForSolve;

        var colorNames = new[] { "Yellow", "Pink", "Blue" };
        var colorIx = fldColorIx.Get(min: 0, max: 2);

        addQuestions(module, makeQuestion(Question.CrittersColor, module, correctAnswers: new[] { colorNames[colorIx] }));
    }
}