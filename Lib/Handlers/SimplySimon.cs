using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimplySimon
{
    [SouvenirQuestion("What were the flashes in the {1} stage of {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("RBYG")]
    [AnswerGenerator.Strings("2*RBYG")]
    [AnswerGenerator.Strings("3*RBYG")]
    [AnswerGenerator.Strings("4*RBYG")]
    Flash
}

public partial class SouvenirModule
{
    [SouvenirHandler("simplysimon", "Simply Simon", typeof(SSimplySimon), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSimplySimon(ModuleData module)
    {
        var comp = GetComponent(module, "simplysimon");
        var flashes = new List<string>();
        var fldFlash = GetField<string>(comp, "flsh");
        var fldStage = GetIntField(comp, "_stagesDone");

        bool onstrike() { flashes.Clear(); return true; }
        module.Module.OnStrike += onstrike;

        while (module.Unsolved)
        {
            var stage = fldStage.Get(min: 0);
            if (stage > flashes.Count)
                flashes.Add(fldFlash.Get(v => Regex.IsMatch(v, "^[RGBY]{1,4}$") ? null : "Expected match for /^[RGBY]{1,4}$/"));
            yield return null;
        }

        module.Module.OnStrike -= onstrike;

        addQuestions(module, flashes.Select((f, i) =>
            makeQuestion(Question.SimplySimonFlash, module,
                correctAnswers: new[] { f },
                preferredWrongAnswers: flashes.ToArray(),
                formatArgs: new[] { Ordinal(i + 1) })));
    }
}