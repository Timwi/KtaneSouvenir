using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SForgetOurVoices
{
    [SouvenirQuestion("What was played in the {1} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "forgetOurVoices", AudioSizeMultiplier = 3, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage", "Umbra Moruka", "Dicey", "MásQuéÉlite", "Obvious", "1254", "Dbros1000", "Bomberjack", "Danielstigman", "Depresso", "ktane1", "OEGamer", "jTIS", "Krispy", "Grunkle Squeaky", "Arceus", "ScopingLandscape", "Emik", "GhostSalt", "Short_c1rcuit", "Eltrick", "Axodeau", "Asew", "Cooldoom", "Piissii", "CrazyCaleb"])]
    Voice
}

public partial class SouvenirModule
{
    [SouvenirHandler("forgetOurVoices", "Forget Our Voices", typeof(SForgetOurVoices), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessForgetOurVoices(ModuleData module)
    {
        while (!_noUnignoredModulesLeft)
            yield return null;

        var comp = GetComponent(module, "FOVscript");
        const string moduleId = "forgetOurVoices";

        var numStages = GetField<int>(comp, "currentStage").Get();
        var soundIxs = GetArrayField<int>(comp, "initialString").Get(minLength: numStages, nullArrayAllowed: true, validator: x => x is < 0 or >= 250 ? "Out of range [0, 249]" : null);

        if (soundIxs is null)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        var usedSoundIxs = soundIxs.Take(numStages).ToList();
        var allAudio = GetArrayField<AudioClip>(comp, "speakers", isPublic: true).Get(expectedLength: 250);

        if (_moduleCounts[moduleId] == 1)
        {
            addQuestions(module, usedSoundIxs.Select((v, i) => makeQuestion(Question.ForgetOurVoicesVoice, moduleId, 1, allAnswers: allAudio, correctAnswers: new[] { allAudio[v] }, formatArgs: new[] { Ordinal(i + 1) })));
            yield break;
        }

        _forgetOurVoicesStages.Add(usedSoundIxs);

        yield return null;

        if (_forgetOurVoicesStages.Any(s => s.Count != numStages))
            throw new AbandonModuleException("Stage counts were not consistent across modules.");

        var names = Ut.NewArray(
            "Umbra Moruka", "Dicey", "MásQuéÉlite", "Obvious", "1254",
            "Dbros1000", "Bomberjack", "Danielstigman", "Depresso", "ktane1",
            "OEGamer", "jTIS", "Krispy", "Grunkle Squeaky", "Arceus",
            "ScopingLandscape", "Emik", "GhostSalt", "Short_c1rcuit", "Eltrick",
            "Axodeau", "Asew", "Cooldoom", "Piissii", "CrazyCaleb");

        var qs = new List<QandA>();
        for (var stage = 0; stage < usedSoundIxs.Count; stage++)
        {
            var soundIx = usedSoundIxs[stage];
            var unique = Enumerable.Range(0, usedSoundIxs.Count).Where(s => s != stage && _forgetOurVoicesStages.Count(l => l[s] == usedSoundIxs[s]) == 1).ToArray();

            if (unique.Length > 0)
            {
                var u = unique.PickRandom();
                var format = string.Format(
                    translateString(Question.ForgetOurVoicesVoice, "the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage"),
                    usedSoundIxs[u] % 10, translateString(Question.ForgetOurVoicesVoice, names[usedSoundIxs[u] / 10]), Ordinal(u + 1));
                qs.Add(makeQuestion(Question.ForgetOurVoicesVoice, module, formattedModuleName: format, allAnswers: allAudio, correctAnswers: new[] { allAudio[soundIx] }, formatArgs: new[] { Ordinal(stage + 1) }));
            }
        }

        if (qs.Count == 0)
            yield return legitimatelyNoQuestion(module, $"There were not enough stages where this one (#{GetField<int>(comp, "moduleId").Get()}) was unique.");

        addQuestions(module, qs);
    }
}