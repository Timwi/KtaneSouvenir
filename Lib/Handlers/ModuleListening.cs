using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SModuleListening
{
    [SouvenirQuestion("Which sound did the {1} button play in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "green", "blue", "yellow"], ArgumentGroupSize = 1, Type = AnswerType.Audio, ForeignAudioID = "moduleListening")]
    ButtonAudio,
    
    [SouvenirQuestion("Which sound played in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "moduleListening")]
    AnyAudio
}

public partial class SouvenirModule
{
    [SouvenirHandler("moduleListening", "Module Listening", typeof(SModuleListening), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessModuleListening(ModuleData module)
    {
        var comp = GetComponent(module, "ModuleListening");
        yield return WaitForSolve;

        var clipsPerModule = GetArrayField<AudioClip[]>(comp, "audioLibrary").Get(expectedLength: 100);
        var soundIndex = GetArrayField<int>(comp, "soundIndex").Get(expectedLength: 4);

        var moduleNames = GetArrayField<string>(comp, "moduleNames").Get();
        var indices = GetArrayField<int>(comp, "moduleIndex").Get(validator: ar => ar.Length != 4 ? "expected length 4" : ar.Any(v => v < 0 || v >= moduleNames.Length) ? $"out of range for moduleNames (0–{moduleNames.Length - 1})" : null);
        var colorNames = GetArrayField<string>(comp, "buttonColors").Get(expectedLength: 4);
        var colorOrder = GetArrayField<int>(comp, "btnColors").Get(validator: ar => ar.Length != 4 ? "expected length 4" : ar.Any(v => v < 0 || v >= colorNames.Length) ? $"out of range for colorNames (0–{colorNames.Length - 1})" : null);
        var allUsed = Enumerable.Range(0, 4).Select(i => clipsPerModule[indices[i]][soundIndex[i]]).ToArray();

        // Pick a single sound from each module to avoid (a) too many Boot To Big sounds, because there are a lot; (b) nearly-identical sounds (like Colored Squares)
        var allAnswers = clipsPerModule.Select((clips, ix) => Array.IndexOf(indices, ix) is int p && p >= 0 ? clips[soundIndex[p]] : clips.PickRandom()).ToArray();

        addQuestions(module,
            Enumerable.Range(0, 4).Select(btn =>
                makeQuestion(Question.ModuleListeningButtonAudio, module, formatArgs: new[] { colorNames[colorOrder[btn]] },
                correctAnswers: new[] { clipsPerModule[indices[btn]][soundIndex[btn]] },
                preferredWrongAnswers: allUsed,
                allAnswers: allAnswers))
            .Concat(new[] { makeQuestion(Question.ModuleListeningAnyAudio, module, correctAnswers: allUsed, allAnswers: allAnswers) }));
    }
}