using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMadMemory
{
    [SouvenirQuestion("What was on the display in the {1} stage of {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "01", "02", "03", "04", "ONE", "TWO", "THREE", "FOUR", "WON", "TOO", "TREE", "FOR", Arguments = ["first", "second", "third", "4th"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Displays
}

public partial class SouvenirModule
{
    [SouvenirHandler("MadMemory", "Mad Memory", typeof(SMadMemory), "Kuro")]
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