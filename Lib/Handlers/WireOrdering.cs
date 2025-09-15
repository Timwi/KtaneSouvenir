using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWireOrdering
{
    [SouvenirQuestion("What color was the {1} display from the left in {0}?", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "white", "black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayColor,

    [SouvenirQuestion("What number was on the {1} display from the left in {0}?", TwoColumns4Answers, "1", "2", "3", "4", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayNumber,

    [SouvenirQuestion("What color was the {1} wire from the left in {0}?", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "white", "black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    WireColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("kataWireOrdering", "Wire Ordering", typeof(SWireOrdering), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessWireOrdering(ModuleData module)
    {
        var comp = GetComponent(module, "WireOrderingScript");
        var fldChosenColorsDisplay = GetArrayField<int>(comp, "_chosenColorsDis");
        var fldChosenColorsWire = GetArrayField<int>(comp, "_chosenColorsWire");
        var fldChosenDisplayNumbers = GetArrayField<int>(comp, "_chosenDisNum");

        yield return WaitForSolve;

        var colors = Question.WireOrderingDisplayColor.GetAnswers();
        var chosenColorsDisplay = fldChosenColorsDisplay.Get(expectedLength: 4);
        var chosenDisplayNumbers = fldChosenDisplayNumbers.Get(expectedLength: 4);
        var chosenColorsWire = fldChosenColorsWire.Get(expectedLength: 4);
        for (var ix = 0; ix < 4; ix++)
        {
            yield return question(SWireOrdering.DisplayColor, args: [Ordinal(ix + 1)]).Answers(colors[chosenColorsDisplay[ix]]);
            yield return question(SWireOrdering.DisplayNumber, args: [Ordinal(ix + 1)]).Answers(chosenDisplayNumbers[ix].ToString());
            yield return question(SWireOrdering.WireColor, args: [Ordinal(ix + 1)]).Answers(colors[chosenColorsWire[ix]]);
        }
    }
}