using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SDialtones
{
    [SouvenirQuestion("What dialtones were heard in {0}?", OneColumn4Answers, Type = AnswerType.Audio, ForeignAudioID = Sounds.Generated)]
    Dialtones
}

public partial class SouvenirModule
{
    private static readonly Dictionary<string, AudioClip> _dialtonesAnswers = [];

    [SouvenirHandler("xelDialtones", "Dialtones", typeof(SDialtones), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessDialtones(ModuleData module)
    {
        var comp = GetComponent(module, "Dialtones");
        if (_dialtonesAnswers.Count is 0)
        {
            var wordlist = GetArrayField<string>(comp, "wordlist").Get(expectedLength: 30);
            var convert = GetMethod<string>(comp, "ConvertToDialtones", 1);
            foreach (var word in wordlist)
            {
                var tones = convert.Invoke(word);
                // The source code says 0.11f, but the decompilation (and my ears) says 0.2f
                var sounds = tones.Select((t, i) => (Sounds.AudioPosition) (DialtonesAudio[t - '0'], i * 0.2f)).ToArray();
                _dialtonesAnswers[tones] = Sounds.Combine($"Diatones_{tones}", sounds);
            }
        }

        yield return WaitForSolve;

        var toneString = GetField<string>(comp, "questionWord").Get();
        if (!_dialtonesAnswers.TryGetValue(toneString, out var questionDialtones))
            throw new AbandonModuleException($"Unexpected set of question dialtones {toneString}");
        toneString = GetField<string>(comp, "answerWord").Get();
        if (!_dialtonesAnswers.TryGetValue(toneString, out var solution))
            throw new AbandonModuleException($"Unexpected set of solution dialtones {toneString}");

        yield return question(SDialtones.Dialtones).Answers(questionDialtones, all: _dialtonesAnswers.Values.ToArray(), preferredWrong: [solution]);
    }
}
