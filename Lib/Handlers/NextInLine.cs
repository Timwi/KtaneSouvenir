using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNextInLine
{
    [Question("What color was the first wire in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Black", "White", "Gray", TranslateAnswers = true)]
    FirstWire
}

public partial class SouvenirModule
{
    [Handler("NextInLine", "Next In Line", typeof(SNextInLine), "Anonymous")]
    [ManualQuestion("What color was the first wire?")]
    private IEnumerator<SouvenirInstruction> ProcessNextInLine(ModuleData module)
    {
        var comp = GetComponent(module, "NextInLine");
        var color = GetIntField(comp, "CurrentColor").Get(min: 0, max: 7);

        var hasStruck = false;
        module.Module.OnStrike += () => hasStruck = true;

        yield return WaitForSolve;

        if (hasStruck)
            yield return legitimatelyNoQuestion(module, "No question for Next In Line because the module struck, so the first wire color may be irretrievable.");

        var colors = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Black", "White", "Gray" };
        yield return question(SNextInLine.FirstWire).Answers(colors[color]);
    }
}