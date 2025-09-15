using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SThinkingWires
{
    [SouvenirQuestion("What was the position from top to bottom of the first wire needing to be cut in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7")]
    FirstWire,

    [SouvenirQuestion("What color did the second valid wire to cut have to have in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", "Any", TranslateAnswers = true)]
    SecondWire,

    [SouvenirQuestion("What was the display number in {0}?", ThreeColumns6Answers, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69")]
    DisplayNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("thinkingWiresModule", "Thinking Wires", typeof(SThinkingWires), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessThinkingWires(ModuleData module)
    {
        var comp = GetComponent(module, "thinkingWiresScript");
        yield return WaitForSolve;

        var validWires = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", "Any" };
        var firstCorrectWire = GetIntField(comp, "firstWireToCut").Get(min: 1, max: 7);
        var secondCorrectWire = GetField<string>(comp, "secondWireToCut").Get(str => !validWires.Contains(str) ? $"invalid color; expected: {validWires.JoinString(", ")}" : null);
        var displayNumber = GetField<string>(comp, "screenNumber").Get();

        // List of valid display numbers for validation. 69 happens in the case of "Any" while 11 is expected to be the longest.
        // Basic calculations by hand and algorithm seem to confirm this, but may want to recalculate to ensure it is right.
        if (!new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69" }.Contains(displayNumber))
            throw new AbandonModuleException($"‘displayNumber’ has an unexpected value: {displayNumber}");

        yield return question(SThinkingWires.FirstWire).Answers(firstCorrectWire.ToString());
        yield return question(SThinkingWires.SecondWire).Answers(secondCorrectWire);
        yield return question(SThinkingWires.DisplayNumber).Answers(displayNumber);
    }
}
