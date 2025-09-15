using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSeaShells
{
    [SouvenirQuestion("What were the first and second words in the {1} phrase in {0}?", TwoColumns4Answers, "she sells", "she shells", "sea shells", "sea sells", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Question1,

    [SouvenirQuestion("What were the third and fourth words in the {1} phrase in {0}?", TwoColumns4Answers, "sea shells", "she shells", "sea sells", "she sells", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Question2,

    [SouvenirQuestion("What was the end of the {1} phrase in {0}?", TwoColumns4Answers, "sea shore", "she sore", "she sure", "seesaw", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Question3
}

public partial class SouvenirModule
{
    [SouvenirHandler("SeaShells", "Sea Shells", typeof(SSeaShells), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSeaShells(ModuleData module)
    {
        var comp = GetComponent(module, "SeaShellsModule");
        var fldRow = GetIntField(comp, "row");
        var fldCol = GetIntField(comp, "col");
        var fldKeynum = GetIntField(comp, "keynum");
        var fldStage = GetIntField(comp, "stage");
        var fldDisplay = GetField<TextMesh>(comp, "Display", isPublic: true);

        yield return WaitForActivate;

        var rows = new int[3];
        var cols = new int[3];
        var keynums = new int[3];
        while (true)
        {
            while (fldDisplay.Get().text == " ")
            {
                yield return new WaitForSeconds(.1f);
                if (module.IsSolved)
                    goto solved;
            }

            var stage = fldStage.Get(min: 0, max: 2);
            rows[stage] = fldRow.Get();
            cols[stage] = fldCol.Get();
            keynums[stage] = fldKeynum.Get();

            while (fldDisplay.Get().text != " ")
            {
                yield return new WaitForSeconds(.1f);
                if (module.IsSolved)
                    goto solved;
            }
        }

        solved:
        var qs = new List<QandA>();
        for (var i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.SeaShells1, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { new[] { "she sells", "she shells", "sea shells", "sea sells" }[rows[i]] }));
            qs.Add(makeQuestion(Question.SeaShells2, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { new[] { "sea shells", "she shells", "sea sells", "she sells" }[cols[i]] }));
            qs.Add(makeQuestion(Question.SeaShells3, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { new[] { "sea shore", "she sore", "she sure", "seesaw" }[keynums[i]] }));
        }
        addQuestions(module, qs);
    }
}
