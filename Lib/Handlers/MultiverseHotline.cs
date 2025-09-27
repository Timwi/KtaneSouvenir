using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMultiverseHotline
{
    [SouvenirQuestion("What was the universe origin in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "MultiverseHotlineAudio", AudioSizeMultiplier = 4)]
    UniverseOrigin,
    [SouvenirQuestion("What was the universe origin's initial number in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(3, "0123456789")]
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
        var universeOriginNum = GetField<string>(comp, "originNumber").Get();

        yield return question(SMultiverseHotline.UniverseOrigin).Answers(MultiverseHotlineAudio[universeOriginIx]);
        yield return question(SMultiverseHotline.UniverseOriginInitNumber).Answers(universeOriginNum);
    }
}
