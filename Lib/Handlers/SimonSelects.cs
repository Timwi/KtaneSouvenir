using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSelects
{
    [SouvenirQuestion("Which color was among the colors that flashed in the {1} stage of {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Magenta", "Cyan", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Flashes
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSelectsModule", "Simon Selects", typeof(SSimonSelects), "Quinn Wuest")]
    [SouvenirManualQuestion("Which colors flashed in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSelects(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSelectsScript");
        yield return WaitForSolve;

        var order = Enumerable.Range(0, 3).Select(i => GetArrayField<int>(comp, $"stg{i + 1}order").Get(minLength: 3, maxLength: 5)).ToArray();
        var btnRenderers = GetArrayField<Renderer>(comp, "buttonrend", isPublic: true).Get(expectedLength: 8);

        var seqs = new string[3][];

        for (var stage = 0; stage < 3; stage++)
        {
            var parsedString = new string[order[stage].Length];
            for (var flash = 0; flash < order[stage].Length; flash++)
                parsedString[flash] = btnRenderers[order[stage][flash]].material.name.Replace(" (Instance)", "");
            seqs[stage] = parsedString;
        }

        for (int stage = 0; stage < seqs.Length; stage++)
            yield return question(SSimonSelects.Flashes, args: [Ordinal(stage + 1)]).Answers(seqs[stage]);
    }
}
