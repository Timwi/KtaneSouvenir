using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBraille
{
    [SouvenirQuestion("What was the {1} pattern in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Circles(2, 3, 20, 20, SuppressEmpty = true)]
    Pattern
}

public partial class SouvenirModule
{
    [SouvenirHandler("BrailleModule", "Braille", typeof(SBraille), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessBraille(ModuleData module)
    {
        var comp = GetComponent(module, "BrailleModule");
        yield return WaitForSolve;

        var braillePatterns = GetArrayField<int>(comp, "BraillePatterns").Get(expectedLength: 4);
        for (var ix = 0; ix < braillePatterns.Length; ix++)
            yield return question(SBraille.Pattern, args: [Ordinal(ix + 1)]).Answers([Sprites.GenerateCirclesSprite(2, 3, braillePatterns[ix], 20, 20, vertical: true)]);
    }
}
