using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SListeningPianoKeys
{
    [Question("What was the correctly submitted tune in {0}?", ThreeColumns6Answers, ForeignAudioID = Sounds.Generated, AnswerType = InfoType.Audio)]
    Sound
}

public partial class SouvenirModule
{
    [Handler("listeningPiano", "Listening Piano Keys", typeof(SListeningPianoKeys), "MykiiVT")]
    [ManualQuestion("What was the correctly submitted tune?")]
    private IEnumerator<SouvenirInstruction> ProcessListeningPianoKeys(ModuleData module)
    {
        var comp = GetComponent(module, "listeningPianoScript");
        var noteSounds = GetArrayField<AudioClip>(comp, "PianoSounds", true).Get(expectedLength: 12);

        yield return WaitForSolve;

        var answers = GetArrayField<int[]>(comp, "SouvenirList", true).Get();
        var namesList = GetArrayField<string>(comp, "SouvenirNamesList", true).Get();
        var moduleId = GetField<int>(comp, "moduleId", true).Get();
        var clips = new AudioClip[6];

        for (var i = 0; i < 6; i++)
        {
            var inputList = new List<Sounds.AudioPosition>();
            for (var j = 1; j < answers[i].Length; j++)
                if (answers[i][j] != -1)
                    inputList.Add(new Sounds.AudioPosition(noteSounds[answers[i][j]], j * (60f / answers[i][0])));
            clips[i] = Sounds.Combine($"{namesList[i]}_{moduleId}", inputList.ToArray());
        }
        yield return question(SListeningPianoKeys.Sound).Answers(clips[0], all: clips);
    }
}
