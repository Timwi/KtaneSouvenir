using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMemoryWires
{
    [SouvenirQuestion("What was the colour of wire {1} in {0}?", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", "Black", Arguments = ["1", "2", "3", "4", "29", "30"], ArgumentGroupSize = 1, TranslateAnswers = true)]
    WireColours,

    [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    DisplayedDigits
}

public partial class SouvenirModule
{
    [SouvenirHandler("memoryWires", "Memory Wires", typeof(SMemoryWires), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessMemoryWires(ModuleData module)
    {
        var comp = GetComponent(module, "MemoryWiresScript");

        var fldStageNumber = GetIntField(comp, "stage");
        var fldDisplayNumber = GetIntField(comp, "dispnum");
        var displayedDigits = new int[5];
        var currentStage = 0;

        module.Module.OnStrike += () => { currentStage = -1; return false; };

        while (!_isActivated)
            yield return null; // Do not wait 0.1 seconds to make sure we get the right number.
        displayedDigits[0] = fldDisplayNumber.Get(min: 1, max: 6);

        while (module.Unsolved)
        {
            var stage = fldStageNumber.Get(min: 0, max: 4);
            if (currentStage != stage)
            {
                displayedDigits[stage] = fldDisplayNumber.Get(min: 1, max: 6);
                currentStage = stage;
            }
            yield return null; // Do not wait 0.1 seconds to make sure we get the right number each time.
        }

        var allColours = GetArrayField<string>(comp, "logcol").Get(expectedLength: 5);
        var wireColours = GetArrayField<int[]>(comp, "colset").Get(expectedLength: 5,
            validator: innerArr => innerArr.Length != 6 ? "expected length 6" : innerArr.Any(i => i is < 0 or > 4) ? "inner array contained value outside expected range 0-4" : null);
        for (var pos = 0; pos < 30; pos++)
            yield return question(SMemoryWires.WireColours, args: [(pos + 1).ToString()]).Answers(allColours[wireColours[pos / 6][pos % 6]]);
        for (var stage = 0; stage < 5; stage++)
            yield return question(SMemoryWires.DisplayedDigits, args: [(stage + 1).ToString()]).Answers(displayedDigits[stage].ToString());
    }
}