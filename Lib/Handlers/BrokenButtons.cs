using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBrokenButtons
{
    [SouvenirQuestion("What was the {1} correct button you pressed in {0}?", ThreeColumns6Answers, "bomb", "blast", "boom", "burst", "wire", "button", "module", "light", "led", "switch", "RJ-45", "DVI-D", "RCA", "PS/2", "serial", "port", "row", "column", "one", "two", "three", "four", "five", "six", "seven", "eight", "size", "this", "that", "other", "submit", "abort", "drop", "thing", "blank", "broken", "too", "to", "yes", "see", "sea", "c", "wait", "word", "bob", "no", "not", "first", "hold", "late", "fail", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    
}

public partial class SouvenirModule
{
    [SouvenirHandler("BrokenButtonsModule", "Broken Buttons", typeof(SBrokenButtons), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessBrokenButtons(ModuleData module)
    {
        var comp = GetComponent(module, "BrokenButtonModule");
        yield return WaitForSolve;

        var pressed = GetListField<string>(comp, "Pressed").Get();
        if (pressed.All(p => p.Length == 0))
            yield return legitimatelyNoQuestion(module, "The only buttons you pressed were literally blank.");

        // skip the literally blank buttons.
        addQuestions(module, pressed.Select((p, i) => p.Length == 0 ? null : makeQuestion(Question.BrokenButtons, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { p }, preferredWrongAnswers: pressed.Except(new[] { "" }).ToArray())));
    }
}