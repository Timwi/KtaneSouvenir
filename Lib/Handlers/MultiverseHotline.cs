using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SMultiverseHotline
{
    [SouvenirQuestion("What was the universe origin in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "MultiverseHotline", AudioSizeMultiplier = 4)]
    UniverseOrigin,
    [SouvenirQuestion("What was the universe origin's initial number in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings("3*0-9")]
    UniverseOriginInitNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("MultiverseHotline", "Multiverse Hotline", typeof(SMultiverseHotline), "KiloBites", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessMultiverseHotline(ModuleData module)
    {
        var comp = GetComponent(module, "MultiverseHotlineScript");
        yield return WaitForSolve;

        var universeOriginIx = GetField<int>(comp, "originUniverse").Get();
        var voiceIx = GetField<int>(comp, "mainVoice").Get();
        var audio = GetArrayField<AudioClip>(comp, "UniClips", isPublic: true).Get(expectedLength: 32);
        var universeOriginNum = GetField<string>(comp, "originNumber").Get();

        yield return question(SMultiverseHotline.UniverseOrigin).Answers(audio[voiceIx * 16 + universeOriginIx], all: audio.Skip(16 * voiceIx).Take(16).ToArray());
        yield return question(SMultiverseHotline.UniverseOriginInitNumber).Answers(universeOriginNum);
    }
}
