using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SOddOneOut
{
    [SouvenirQuestion("What was the button you pressed in the {1} stage of {0}?", TwoColumns4Answers, "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Button
}

public partial class SouvenirModule
{
    [SouvenirHandler("OddOneOutModule", "Odd One Out", typeof(SOddOneOut), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessOddOneOut(ModuleData module)
    {
        var comp = GetComponent(module, "OddOneOutModule");

        yield return WaitForSolve;

        var stages = GetField<Array>(comp, "_stages").Get(ar => ar.Length != 6 ? "expected length 6" : ar.Cast<object>().Any(obj => obj == null) ? "contains null" : null);
        var btnNames = new[] { "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right" };
        var stageBtn = stages.Cast<object>().Select(x => GetIntField(x, "CorrectIndex", isPublic: true).Get(min: 0, max: btnNames.Length - 1)).ToArray();

        for (var i = 0; i < 6; i++)
            yield return question(SOddOneOut.Button, args: [Ordinal(i + 1)]).Answers(btnNames[stageBtn[i]]);
    }
}
