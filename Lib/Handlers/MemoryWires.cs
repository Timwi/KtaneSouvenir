using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMemoryWires
{
    [SouvenirQuestion("What was the colour of wire {1} in {0}?", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", "Black", Arguments = ["1", "2", "3", "4", "29", "30"], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QWireColours,

    [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    QDisplayedDigits,

    [SouvenirDiscriminator("the Memory Wires where the colour of wire {0} was {1}", Arguments = ["1", "red", "2", "yellow", "3", "blue", "4", "white", "5", "black"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DWireColours,

    [SouvenirDiscriminator("the Memory Wires where the digit displayed in the {0} stage was {1}", Arguments = [QandA.Ordinal, "1"], ArgumentGroupSize = 2)]
    DDisplayedDigits
}

public partial class SouvenirModule
{
    [SouvenirHandler("memoryWires", "Memory Wires", typeof(SMemoryWires), "Kuro")]
    [SouvenirManualQuestion("What were the wire colours?")]
    [SouvenirManualQuestion("What were the displayed digits?")]
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
        for (var pos = 0; pos < 24; pos++)
        {
            yield return new Discriminator(SMemoryWires.DWireColours, $"wire-{pos}", allColours[wireColours[pos / 6][pos % 6]], args: [(pos + 1).ToString(), allColours[wireColours[pos / 6][pos % 6]]]);
            yield return question(SMemoryWires.QWireColours, args: [(pos + 1).ToString()])
                .AvoidDiscriminators($"wire-{pos}")
                .Answers(allColours[wireColours[pos / 6][pos % 6]]);
        }
        for (var stage = 0; stage < 5; stage++)
        {
            yield return new Discriminator(SMemoryWires.DDisplayedDigits, $"display-{stage}", displayedDigits[stage].ToString(), args: [Ordinal(stage + 1), displayedDigits[stage].ToString()]);
            yield return question(SMemoryWires.QDisplayedDigits, args: [Ordinal(stage + 1)])
                .AvoidDiscriminators($"display-{stage}")
                .Answers(displayedDigits[stage].ToString());
        }
    }
}