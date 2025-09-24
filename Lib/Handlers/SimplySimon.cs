using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
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
    [SouvenirHandler("simplysimon", "Simply Simon", typeof(SSimplySimon), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimplySimon(ModuleData module)
    {
        var comp = GetComponent(module, "simplysimon");
        var prevStages = new List<(string flash, string answer)>();
        var fldFlash = GetField<string>(comp, "flsh");
        var fldAnswer = GetField<string>(comp, "ans");
        var fldStage = GetIntField(comp, "_stagesDone");

        bool onstrike() { prevStages.Clear(); return true; }
        module.Module.OnStrike += onstrike;

        while (module.Unsolved)
        {
            // _stagesDone does not advance for the last stage (it is in fact always set to 3, even if there was already a stage 3).
            // The only other way to detect a new stage is if the flashes or the answer has changed.
            // Fortunately, ‘flsh’, ‘ans’ and ‘_stagesDone’ all change in tandem.
            // Therefore, the only times we miss the last stage is when it is identical to the previous stage.
            var stage = fldStage.Get(min: 0);
            var flash = fldFlash.Get(v => Regex.IsMatch(v, "^[RGBY]{1,4}$") ? null : "Expected 1–4 RGBY");
            var answer = fldAnswer.Get(v => v.Length != flash.Length ? $"Expected answer of same length as flash ({flash})" : !Regex.IsMatch(v, "^[RGBY]*$") ? "Expected only RGBY" : null);
            if (stage > prevStages.Count || flash != prevStages.Last().flash || answer != prevStages.Last().answer)
                prevStages.Add((flash, answer));
            yield return null;
        }

        module.Module.OnStrike -= onstrike;

        var flashes = prevStages.Select(stage => stage.flash).ToArray();
        for (var i = 0; i < prevStages.Count; i++)
            yield return question(SSimplySimon.Flash, args: [Ordinal(i + 1)]).Answers(flashes[i], preferredWrong: flashes);
    }
}
