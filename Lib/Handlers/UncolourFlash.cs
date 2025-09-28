using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUncolourFlash
{
    [SouvenirQuestion("What was the {1} in the {2} position of the “{3}” sequence of {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "White", "Magenta", Arguments = ["word", QandA.Ordinal, "YES", "colour of the word", QandA.Ordinal, "NO"], ArgumentGroupSize = 3, TranslateArguments = [true, false, false])]
    Displays,

    [SouvenirDiscriminator("the Uncolour Flash where the {0} in the {1} position of the “{2}” sequence was {3}", Arguments = ["word", QandA.Ordinal, "YES", "Red", "word", QandA.Ordinal, "YES", "Green", "word", QandA.Ordinal, "YES", "Blue", "colour of the word", QandA.Ordinal, "NO", "Yellow", "colour of the word", QandA.Ordinal, "NO", "White", "colour of the word", QandA.Ordinal, "NO", "Magenta"], ArgumentGroupSize = 4, TranslateArguments = [true, false, false, false])]
    Discriminator
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
                    {
                        yield return new Discriminator(SUncolourFlash.Discriminator, $"{displayIx}-{yesno}-{wordcolor}", fldInitseq[yesno][wordcolor][displayIx],
                            args: [wordcolor == 0 ? "word" : "colour of the word", Ordinal(displayIx + 1), yesno == 0 ? "YES" : "NO", colornames[fldInitseq[yesno][wordcolor][displayIx]]]);
                        yield return question(SUncolourFlash.Displays, args: [wordcolor == 0 ? "word" : "colour of the word", Ordinal(displayIx + 1), yesno == 0 ? "YES" : "NO"])
                            .AvoidDiscriminators(Enumerable.Range(0, 12 * 2 * 2).Where(i => i % 12 == displayIx || i / 12 % 2 == yesno || i / 12 / 2 == wordcolor).Select(i => $"{i % 12}-{i / 12 % 2}-{i / 12 / 2}"))
                            .Answers(colornames[fldInitseq[yesno][wordcolor][displayIx]]);
                    }
    }
}
