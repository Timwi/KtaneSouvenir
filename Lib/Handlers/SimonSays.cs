using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SSimonSays
{
    [Question("What color flashed {1} in the final sequence in {0}?", TwoColumns4Answers, "red", "yellow", "green", "blue", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QFlash,

    [Discriminator("the Simon Says whose {0} flash was {1}", Arguments = [QandA.Ordinal, "red", QandA.Ordinal, "yellow", QandA.Ordinal, "green", QandA.Ordinal, "blue"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DFlash
}

public partial class SouvenirModule
{
    [Handler("Simon", "Simon Says", typeof(SSimonSays), "Andrio Celos")]
    [ManualQuestion("Which colors flashed in the final sequence?")]
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
        {
            yield return new Discriminator(SSimonSays.DFlash, $"flash-{i}", colorNames[sequence[i]], args: [Ordinal(i + 1), colorNames[sequence[i]]]);
            yield return question(SSimonSays.QFlash, args: [Ordinal(i + 1)])
                .AvoidDiscriminators($"flash-{i}")
                .Answers(colorNames[sequence[i]]);
        }
    }
}
