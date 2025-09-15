using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonSamples
{
    [SouvenirQuestion("What were the call samples {1} of {0}?", TwoColumns4Answers, AudioFieldName = "SimonSamplesAudio", Type = AnswerType.Audio, TranslateArguments = [true], Arguments = ["played in the first stage", "added in the second stage", "added in the third stage"], ArgumentGroupSize = 1)]
    Samples
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSamples", "Simon Samples", typeof(SSimonSamples), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSamples(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSamples");
        yield return WaitForSolve;

        var calls = GetListField<string>(comp, "_calls").Get(expectedLength: 3);
        if (Enumerable.Range(1, 2).Any(i => calls[i].Length <= calls[i - 1].Length || !calls[i].StartsWith(calls[i - 1])))
            throw new AbandonModuleException($"_calls=[{calls.Select(c => $"“{c}”").JoinString(", ")}]; expected each element to start with the previous.");
        var possibleCalls = "0012|0112|0212|0213|0011|0211|0312|0313|0011|1010|1221|0232".Split('|');
        var check = Enumerable.Range(0, 3)
            .Select(i => calls[i].Substring(4 * i))
            .Select((s, i) => !possibleCalls.Skip(4 * i).Take(4).Contains(s) ? $"Invalid call for stage {i + 1}: {s}" : null)
            .Where(s => s is not null)
            .Aggregate<string, string>(null, (a, b) => a is null ? b : a + "; " + b);
        if (check is not null)
            throw new AbandonModuleException(check);

        var formatArgs = new[] { "played in the first stage", "added in the second stage", "added in the third stage" };
        addQuestions(module, calls.Select((c, ix) =>
            makeQuestion(Question.SimonSamplesSamples, module, formatArgs: new[] { formatArgs[ix] },
            correctAnswers: new[] { SimonSamplesAudio[Array.IndexOf(possibleCalls, c.Substring(ix * 4))] }, allAnswers: SimonSamplesAudio.Skip(4 * ix).Take(4).ToArray())));
    }
}