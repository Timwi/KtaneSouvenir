using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMadMemory
{
    [Question("What was on the display in the {1} stage of {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "01", "02", "03", "04", "ONE", "TWO", "THREE", "FOUR", "WON", "TOO", "TREE", "FOR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Displays
}

public partial class SouvenirModule
{
    [Handler("MadMemory", "Mad Memory", typeof(SMadMemory), "Kuro")]
    [ManualQuestion("What was on the display in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessMadMemory(ModuleData module)
    {
        var comp = GetComponent(module, "MadMemory");

        yield return WaitForSolve;

        var possibleTexts = GetArrayField<string>(comp, "screenTexts", true).Get(expectedLength: 16);
        var displayedLabels = GetArrayField<int>(comp, "screenLabels", true).Get(expectedLength: 4);
        for (var stageNum = 0; stageNum < 4; stageNum++)
            yield return question(SMadMemory.Displays, args: [Ordinal(stageNum + 1)]).Answers(possibleTexts[displayedLabels[stageNum]]);
    }
}
