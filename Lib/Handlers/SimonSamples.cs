using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SSimonSamples
{
    [SouvenirQuestion("What were the call samples {1} of {0}?", TwoColumns4Answers, ForeignAudioID = Sounds.Generated, Type = AnswerType.Audio, TranslateArguments = [true], Arguments = ["played in the first stage", "added in the second stage", "added in the third stage"], ArgumentGroupSize = 1)]
    Samples
}

public partial class SouvenirModule
{
    public static AudioClip Temp => Sounds.GetForeignClip("simonSamples", "Kick");

    [SouvenirHandler("simonSamples", "Simon Samples", typeof(SSimonSamples), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSamples(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSamples");
        var possibleCalls = "0012|0112|0212|0213|0011|0211|0312|0313|0011|1010|1221|0232".Split('|');

        var foreignClips = new string[] { "Kick", "Snare", "HiHat", "OpenHiHat" }.Select(s => Sounds.GetForeignClip("simonSamples", s)).ToArray();

        AudioClip generate(string call) =>
            Sounds.Combine($"samples-{call.Replace('0', 'K').Replace('1', 'S').Replace('2', 'H').Replace('3', 'O')}",
                (0.0f, foreignClips[int.Parse(call.Substring(0, 1))]),
                (0.5f, foreignClips[int.Parse(call.Substring(1, 1))]),
                (1.0f, foreignClips[int.Parse(call.Substring(2, 1))]),
                (1.5f, foreignClips[int.Parse(call.Substring(3, 1))])
            );

        AudioClip[] allCalls = possibleCalls.Select(generate).ToArray();
        _unityObjectsToDestroyLater.AddRange(allCalls);

        yield return WaitForSolve;

        var calls = GetListField<string>(comp, "_calls").Get(expectedLength: 3);
        if (Enumerable.Range(1, 2).Any(i => calls[i].Length <= calls[i - 1].Length || !calls[i].StartsWith(calls[i - 1])))
            throw new AbandonModuleException($"_calls=[{calls.Select(c => $"“{c}”").JoinString(", ")}]; expected each element to start with the previous.");
        var check = Enumerable.Range(0, 3)
            .Select(i => calls[i].Substring(4 * i))
            .Select((s, i) => !possibleCalls.Skip(4 * i).Take(4).Contains(s) ? $"Invalid call for stage {i + 1}: {s}" : null)
            .Where(s => s is not null)
            .Aggregate<string, string>(null, (a, b) => a is null ? b : a + "; " + b);
        if (check is not null)
            throw new AbandonModuleException(check);

        var formatArgs = new[] { "played in the first stage", "added in the second stage", "added in the third stage" };
        for (var ix = 0; ix < calls.Count; ix++)
            yield return question(SSimonSamples.Samples, args: [formatArgs[ix]]).Answers(allCalls[Array.IndexOf(possibleCalls, calls[ix].Substring(ix * 4))], all: allCalls.Skip(4 * ix).Take(4).ToArray());
    }
}
