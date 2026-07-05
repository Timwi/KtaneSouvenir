using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SEeBgnillepS
{
    [Question("What word was asked to be spelled in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Audio, ForeignAudioID = "eebgnilleps")]
    [ReverseQuestionGimmick]
    Word
}

public partial class SouvenirModule
{
    [Handler("eeBgnilleps", "eeB gnillepS", typeof(SEeBgnillepS), "Quinn Wuest")]
    [ManualQuestion("What word was asked to be spelled?")]
    private IEnumerator<SouvenirInstruction> ProcessEeBgnillepS(ModuleData module)
    {
        var comp = GetComponent(module, "tpircSeeBgnillepS");
        var audioClips = GetArrayField<AudioClip>(comp, "sdrow", isPublic: true).Get(expectedLength: 70);

        yield return WaitForSolve;
        var wordIx = GetField<int>(comp, "modnar").Get(v => v < 0 || v >= audioClips.Length ? $"expected range 0–{audioClips.Length - 1}" : null);

        yield return question(SEeBgnillepS.Word).Answers(audioClips[wordIx], all: audioClips);
    }
}
