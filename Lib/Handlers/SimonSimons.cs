using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSimons
{
    [SouvenirQuestion("What was the {1} flash in the final sequence in {0}?", ThreeColumns6Answers, "TR", "TY", "TG", "TB", "LR", "LY", "LG", "LB", "RR", "RY", "RG", "RB", "BR", "BY", "BG", "BB", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    FlashingColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSimons", "Simon Simons", typeof(SSimonSimons), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSimons(ModuleData module)
    {
        var comp = GetComponent(module, "simonsScript");
        yield return WaitForSolve;

        var flashes = new[] { "TR", "TY", "TG", "TB", "LR", "LY", "LG", "LB", "RR", "RY", "RG", "RB", "BR", "BY", "BG", "BB" };
        var buttonFlashes = GetArrayField<KMSelectable>(comp, "selButtons").Get(expectedLength: 5, validator: sel => !flashes.Contains(sel.name.ToUpperInvariant()) ? "invalid flash" : null);
        addQuestions(module, buttonFlashes.Select((btn, i) =>
            makeQuestion(Question.SimonSimonsFlashingColors, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { btn.name.ToUpperInvariant() })));
    }
}