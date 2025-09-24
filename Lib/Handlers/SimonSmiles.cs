using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonSmiles
{
    [SouvenirQuestion("What sound did the {1} button press make in {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, Type = AnswerType.Audio, AudioFieldName = "SimonSmilesAudio", AudioSizeMultiplier = 6)]
    Sounds
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonSmiles", "Simon Smiles", typeof(SSimonSmiles), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSmiles(ModuleData module)
    {
        var comp = GetComponent(module, "ShitassSays");

        yield return WaitForSolve;

        var shitassMode = GetField<bool>(GetField<object>(comp, "Settings").Get(), "shitassMode", true).Get();
        var sounds = GetField<int[]>(comp, "Sounds")
            .Get(a => a.Select((b, i) => b < 0 ? $"Sounds[{i}] = {b} < 0" : b > 2 ? $"Sounds[{i}] = {b} > 2" : null).Aggregate((x, y) => x is null ? y : y is null ? x : x + ", " + y));
        var allAnswers = shitassMode ? SimonSmilesAudio.Skip(3).ToArray() : SimonSmilesAudio.Take(3).ToArray();
        for (var ix = 0; ix < 9; ix++)
            yield return question(SSimonSmiles.Sounds, args: [Ordinal(ix + 1)]).Answers(allAnswers[sounds[ix]], all: allAnswers);
    }
}
