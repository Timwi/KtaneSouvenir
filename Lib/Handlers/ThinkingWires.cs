using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SThinkingWires
{
    [Question("What color was the {1} wire in the first stage of {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    WireColor,

    [Question("What was the display number in {0}?", ThreeColumns6Answers, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69")]
    DisplayNumber
}

public partial class SouvenirModule
{
    [Handler("thinkingWiresModule", "Thinking Wires", typeof(SThinkingWires), "Espik")]
    [ManualQuestion("What were the wire colors in the first stage?")]
    [ManualQuestion("What was the display number?")]
    private IEnumerator<SouvenirInstruction> ProcessThinkingWires(ModuleData module)
    {
        var comp = GetComponent(module, "thinkingWiresScript");
        var foundWireColors = new string[7];

        IEnumerator RetriveWireColors()
        {
            yield return null;
            var currentWireColors = GetField<IList>(comp, "_wiresColors").Get();

            for (var i = 0; i < 7; i++)
                foundWireColors[i] = currentWireColors[i].ToString();
        }

        yield return WaitForActivate;
        StartCoroutine(RetriveWireColors());

        module.Module.OnStrike += delegate
        {
            StartCoroutine(RetriveWireColors());
            return false;
        };

        yield return WaitForSolve;

        var displayNumber = GetField<string>(comp, "screenNumber").Get();

        // List of valid display numbers for validation. 69 happens in the case of "Any" while 11 is expected to be the longest.
        // Basic calculations by hand and algorithm seem to confirm this, but may want to recalculate to ensure it is right.
        if (!new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69" }.Contains(displayNumber))
            throw new AbandonModuleException($"‘displayNumber’ has an unexpected value: {displayNumber}");

        yield return question(SThinkingWires.DisplayNumber).Answers(displayNumber);

        for (var i = 0; i < 7; i++)
            yield return question(SThinkingWires.WireColor, args: [Ordinal(i + 1)]).Answers(foundWireColors[i]);
    }
}
