using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SIPA
{
    [SouvenirQuestion("What sound played in {0}?", TwoColumns4Answers, Type = AnswerType.Audio, ForeignAudioID = "ipa", AudioSizeMultiplier = 4)]
    Sound
}

public partial class SouvenirModule
{
    [SouvenirHandler("ipa", "IPA", typeof(SIPA), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessIPA(ModuleData module)
    {
        var comp = GetComponent(module, "ipa");
        yield return WaitForSolve;

        var sounds = GetArrayField<AudioClip>(comp, "sounds", true).Get(expectedLength: 71);
        var cap = GetField<int>(comp, "cap").Get(i => i is not 44 and not 71 ? $"Unknown cap value {i} (expected 44 or 71)" : null);

        var soundIx = GetIntField(comp, "soundPresent").Get(0, sounds.Length - 1);
        addQuestions(module, makeQuestion(Question.IpaSound, module,
            correctAnswers: new[] { sounds[soundIx] },
            allAnswers: sounds.Take(cap).ToArray()));
    }
}