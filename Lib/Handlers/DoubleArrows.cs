using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDoubleArrows
{
    [Question("What was the starting position in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 81, "00")]
    Start
}

public partial class SouvenirModule
{
    [Handler("doubleArrows", "Double Arrows", typeof(SDoubleArrows), "Anonymous")]
    [ManualQuestion("What was the starting position?")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleArrows(ModuleData module)
    {
        var comp = GetComponent(module, "DoubleArrowsScript");
        var display = GetField<TextMesh>(comp, "disp", true).Get();
        var start = "";

        while (module.Unsolved)
        {
            if (display.text.Length == 2)
                start = display.text; // This resets on a strike.
            yield return new WaitForSeconds(.1f);
        }

        yield return question(SDoubleArrows.Start).Answers(start);
    }
}
