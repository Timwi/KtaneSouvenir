using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SForgetOurVoices
{
    [SouvenirQuestion("What was played in the {1} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "forgetOurVoices", AudioSizeMultiplier = 3, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Voice,

    [SouvenirDiscriminator("the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage", Arguments = ["1", "Umbra Moruka", QandA.Ordinal, "1", "Dicey", QandA.Ordinal, "1", "MásQuéÉlite", QandA.Ordinal, "1", "Obvious", QandA.Ordinal, "1", "1254", QandA.Ordinal, "1", "Dbros1000", QandA.Ordinal, "1", "Bomberjack", QandA.Ordinal, "1", "Danielstigman", QandA.Ordinal, "1", "Depresso", QandA.Ordinal, "1", "ktane1", QandA.Ordinal, "1", "OEGamer", QandA.Ordinal, "1", "jTIS", QandA.Ordinal, "1", "Krispy", QandA.Ordinal, "1", "Grunkle Squeaky", QandA.Ordinal, "1", "Arceus", QandA.Ordinal, "1", "ScopingLandscape", QandA.Ordinal, "1", "Emik", QandA.Ordinal, "1", "GhostSalt", QandA.Ordinal, "1", "Short_c1rcuit", QandA.Ordinal, "1", "Eltrick", QandA.Ordinal, "1", "Axodeau", QandA.Ordinal, "1", "Asew", QandA.Ordinal, "1", "Cooldoom", QandA.Ordinal, "1", "Piissii", QandA.Ordinal, "1", "CrazyCaleb", QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [false, true, false])]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("forgetOurVoices", "Forget Our Voices", typeof(SForgetOurVoices), "Anonymous", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessForgetOurVoices(ModuleData module)
    {
        while (!_noUnignoredModulesLeft)
            yield return null;

        var comp = GetComponent(module, "FOVscript");

        var numStages = GetField<int>(comp, "currentStage").Get();
        var soundIxs = GetArrayField<int>(comp, "initialString").Get(minLength: numStages, nullArrayAllowed: true, validator: x => x is < 0 or >= 250 ? "Out of range [0, 249]" : null);

        if (soundIxs is null)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        var usedSoundIxs = soundIxs.Take(numStages).ToList();
        var allAudio = GetArrayField<AudioClip>(comp, "speakers", isPublic: true).Get(expectedLength: 250);

        var names = Ut.NewArray(
            "Umbra Moruka", "Dicey", "MásQuéÉlite", "Obvious", "1254",
            "Dbros1000", "Bomberjack", "Danielstigman", "Depresso", "ktane1",
            "OEGamer", "jTIS", "Krispy", "Grunkle Squeaky", "Arceus",
            "ScopingLandscape", "Emik", "GhostSalt", "Short_c1rcuit", "Eltrick",
            "Axodeau", "Asew", "Cooldoom", "Piissii", "CrazyCaleb");

        for (var stage = 0; stage < usedSoundIxs.Count; stage++)
        {
            var soundIx = usedSoundIxs[stage];
            yield return new Discriminator(SForgetOurVoices.Discriminator, $"stage{stage}", soundIx, [(soundIx % 10).ToString(), names[soundIx / 10]]);
            yield return question(SForgetOurVoices.Voice, args: [Ordinal(stage + 1)]).Answers(allAudio[soundIx], all: allAudio);
        }
    }
}
