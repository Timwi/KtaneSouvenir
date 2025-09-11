using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBreakfastEgg
{
    [SouvenirQuestion("Which color appeared on the egg in {0}?", TwoColumns4Answers, "Crimson", "Orange", "Pink", "Beige", "Cyan", "Lime", "Petrol", TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("breakfastEgg", "Breakfast Egg", typeof(SBreakfastEgg), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessBreakfastEgg(ModuleData module)
    {
        var comp = GetComponent(module, "breakfastEggScript");
        yield return WaitForSolve;

        var colors = new[] { "Crimson", "Orange", "Pink", "Beige", "Cyan", "Lime", "Petrol" };
        var yolkA = GetIntField(comp, "yolkNumA").Get(min: 0, max: 7);
        var yolkB = GetIntField(comp, "yolkNumB").Get(min: 0, max: 7);
        addQuestion(module, Question.BreakfastEggColor, correctAnswers: new[] { colors[yolkA], colors[yolkB] });
    }
}