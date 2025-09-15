using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SModuleManeuvers
{
    [SouvenirQuestion("What was the goal location in {0}?", ThreeColumns6Answers, ExampleAnswers = ["0, 0", "1, 0", "2, -1", "-2, 0", "3, 3", "12, -15"], TranslatableStrings = ["{0}, {1}"])]
    Goal
}

public partial class SouvenirModule
{
    [SouvenirHandler("moduleManeuvers", "Module Maneuvers", typeof(SModuleManeuvers), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessModuleManeuvers(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "ModuleManeuversScript");
        var end = GetField<object>(comp, "endPos").Get();
        var x = GetProperty<int>(end, "x", isPublic: true).Get();
        var y = GetProperty<int>(end, "y", isPublic: true).Get();

        var template = translateString(Question.ModuleManeuversGoal, "{0}, {1}");
        // Use answers in a 5×5 grid around the correct one, as well as reflections in all four quadrants
        var allAnswers = from dx in new[] { -2, -1, 0, 1, 2 }
                         from dy in new[] { -2, -1, 0, 1, 2 }
                         from sx in new[] { -1, 1 }
                         from sy in new[] { -1, 1 }
                         select string.Format(template, sx * x + dx, sy * y + dy);

        addQuestion(module, Question.ModuleManeuversGoal, correctAnswers: new[] { string.Format(template, x, y) }, allAnswers: allAnswers.Distinct().ToArray());
    }
}