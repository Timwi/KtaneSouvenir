using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SSimonSays
{
    [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", TwoColumns4Answers, "red", "yellow", "green", "blue", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Flash
}

public partial class SouvenirModule
{
    [SouvenirHandler("Simon", "Simon Says", typeof(SSimonSays), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSays(ModuleData module)
    {
        var comp = GetComponent(module, "SimonComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        module.SolveIndex = module.Info.NumSolved++;

        var colorNames = new[] { "red", "blue", "green", "yellow" };
        var sequence = GetArrayField<int>(comp, "currentSequence").Get(validator: arr => arr.Any(i => i < 0 || i >= colorNames.Length) ? "expected values 0–3" : null);
        for (var i = 0; i < sequence.Length; i++)
            yield return question(SSimonSays.Flash, args: [Ordinal(i + 1)]).Answers(colorNames[sequence[i]]);
    }
}
