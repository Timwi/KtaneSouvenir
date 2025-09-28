using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SMultiverseHotline
{
    [SouvenirQuestion("What was the origin universe in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "MultiverseHotline", AudioSizeMultiplier = 4)]
    OriginUniverse,

    [SouvenirQuestion("What was the origin universe’s initial number in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings("3*0-9")]
    OriginUniverseInitNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("MultiverseHotline", "Multiverse Hotline", typeof(SMultiverseHotline), "KiloBites", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessMultiverseHotline(ModuleData module)
    {
        var comp = GetComponent(module, "MultiverseHotlineScript");
        yield return WaitForSolve;

        var originUniverseIx = GetField<int>(comp, "originUniverse").Get();
        var voiceIx = GetField<int>(comp, "mainVoice").Get();
        var audio = GetArrayField<AudioClip>(comp, "UniClips", isPublic: true).Get(expectedLength: 32);
        var originUniverseNum = GetField<string>(comp, "originNumber").Get();

        yield return question(SMultiverseHotline.OriginUniverse).Answers(audio[voiceIx * 16 + originUniverseIx], all: audio.Skip(16 * voiceIx).Take(16).ToArray());
        yield return question(SMultiverseHotline.OriginUniverseInitNumber).Answers(originUniverseNum);
    }
}
