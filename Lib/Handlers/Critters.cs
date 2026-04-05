using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCritters
{
    [Question("What was the color in {0}?", TwoColumns4Answers, "Yellow", "Pink", "Blue", "White", TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [Handler("CrittersModule", "Critters", typeof(SCritters), "Eltrick")]
    [ManualQuestion("What was the color?")]
    private IEnumerator<SouvenirInstruction> ProcessCritters(ModuleData module)
    {
        var comp = GetComponent(module, "CrittersScript");
        var fldColorIx = GetIntField(comp, "_randomiser");

        yield return WaitForSolve;

        var colorNames = new[] { "Yellow", "Pink", "Blue" };
        var colorIx = fldColorIx.Get(min: 0, max: 2);

        yield return question(SCritters.Color).Answers(colorNames[colorIx]);
    }
}
