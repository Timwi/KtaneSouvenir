using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNextInLine
{
    [SouvenirQuestion("What color was the first wire in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Black", "White", "Gray", TranslateAnswers = true)]
    FirstWire
}

public partial class SouvenirModule
{
    [SouvenirHandler("NextInLine", "Next In Line", typeof(SNextInLine), "Anonymous")]
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
        addQuestion(module, Question.NextInLineFirstWire, correctAnswers: new[] { colors[color] });
    }
}