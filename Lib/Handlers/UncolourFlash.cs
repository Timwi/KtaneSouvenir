using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUncolourFlash
{
    [SouvenirQuestion("What was the {1} in the {2} position of the {3} sequence of {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "White", "Magenta", Arguments = ["word", QandA.Ordinal, "“YES”", "colour of the word", QandA.Ordinal, "“NO”"], ArgumentGroupSize = 3)]
    Displays
}

public partial class SouvenirModule
{
    [SouvenirHandler("uncolourFlash", "Uncolour Flash", typeof(SUncolourFlash), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessUncolourFlash(ModuleData module)
    {
        var comp = GetComponent(module, "UCFScript");
        yield return WaitForSolve;

        var fldInitseq = GetArrayField<int[][]>(comp, "initseq").Get();
        var colornames = new[] { "Red", "Green", "Blue", "Yellow", "Magenta", "White" };
        for (var displayIx = 0; displayIx < 12; displayIx++)
            for (var yesno = 0; yesno < 2; yesno++)
                if (yesno != 0 || displayIx < 6)
                    for (var wordcolor = 0; wordcolor < 2; wordcolor++)
                        yield return question(SUncolourFlash.Displays, args: [wordcolor == 0 ? "word" : "colour of the word", Ordinal(displayIx + 1), yesno == 0 ? "“YES”" : "“NO”"]).Answers(colornames[fldInitseq[yesno][wordcolor][displayIx]]);
    }
}