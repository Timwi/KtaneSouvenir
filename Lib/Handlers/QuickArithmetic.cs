using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SQuickArithmetic
{
    [SouvenirQuestion("What was the {1} color in the primary sequence in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "white", "black", "orange", "pink", "purple", "cyan", "brown", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors,

    [SouvenirQuestion("What was the {1} digit in the {2} sequence in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, "primary", QandA.Ordinal, "secondary"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    [AnswerGenerator.Integers(0, 9)]
    PrimSecDigits
}

public partial class SouvenirModule
{
    [SouvenirHandler("QuickArithmetic", "Quick Arithmetic", typeof(SQuickArithmetic), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessQuickArithmetic(ModuleData module)
    {
        var comp = GetComponent(module, "QuickArithmetic");
        yield return WaitForSolve;

        var seqColors = GetArrayField<int>(comp, "ColorSequence").Get(expectedLength: 8);
        var primSeqDigits = GetArrayField<int>(comp, "LeftSequenceN").Get(expectedLength: 8);
        var secSeqDigits = GetArrayField<int>(comp, "RightSequence").Get(expectedLength: 8);
        var colorRef = new[] { "red", "blue", "green", "yellow", "white", "black", "orange", "pink", "purple", "cyan", "brown" };
        for (var x = 0; x < 8; x++)
        {
            yield return question(SQuickArithmetic.Colors, args: [Ordinal(x + 1)]).Answers(colorRef[seqColors[x]], preferredWrong: colorRef);
            yield return question(SQuickArithmetic.PrimSecDigits, args: [Ordinal(x + 1), "primary"]).Answers(primSeqDigits[x].ToString());
            yield return question(SQuickArithmetic.PrimSecDigits, args: [Ordinal(x + 1), "secondary"]).Answers(secSeqDigits[x].ToString());
        }
    }
}