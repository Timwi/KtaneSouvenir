using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SHunting
{
    [SouvenirQuestion("Which of the first three stages of {0} had the {1} symbol {2}?", TwoColumns4Answers, "none", "first", "second", "first two", "third", "first & third", "second & third", "all three", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["column", QandA.Ordinal, "row", QandA.Ordinal], ArgumentGroupSize = 2)]
    ColumnsRows
}

public partial class SouvenirModule
{
    [SouvenirHandler("hunting", "Hunting", typeof(SHunting), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessHunting(ModuleData module)
    {
        var comp = GetComponent(module, "hunting");
        var fldStage = GetIntField(comp, "stage");
        var fldReverseClues = GetField<bool>(comp, "reverseClues");

        yield return WaitForActivate;

        var hasRowFirst = new bool[4];
        while (module.Unsolved)
        {
            hasRowFirst[fldStage.Get() - 1] = fldReverseClues.Get();
            yield return new WaitForSeconds(.1f);
        }
        foreach (var row in new[] { false, true })
            foreach (var first in new[] { false, true })
                yield return question(SHunting.ColumnsRows, args: [row ? "row" : "column", first ? "first" : "second"])
                    .Answers(SHunting.ColumnsRows.GetAnswers()[(hasRowFirst[0] ^ row ^ first ? 1 : 0) | (hasRowFirst[1] ^ row ^ first ? 2 : 0) | (hasRowFirst[2] ^ row ^ first ? 4 : 0)]);
    }
}
