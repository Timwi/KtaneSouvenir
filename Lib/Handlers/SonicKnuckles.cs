using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSonicKnuckles
{
    [SouvenirQuestion("Which sound was played but not featured in the chosen zone in {0}?", OneColumn4Answers, Type = AnswerType.Audio, ForeignAudioID = "sonicKnuckles")]
    Sounds,

    [SouvenirQuestion("Which badnik was shown in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "SonicKnucklesBadniksSprites")]
    Badnik,

    [SouvenirQuestion("Which monitor was shown in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "SonicKnucklesMonitorsSprites")]
    Monitor
}

public partial class SouvenirModule
{
    [SouvenirHandler("sonicKnuckles", "Sonic & Knuckles", typeof(SSonicKnuckles), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessSonicKnuckles(ModuleData module)
    {
        var comp = GetComponent(module, "sonicKnucklesScript");

        var heroArr = GetArrayField<object>(comp, "heroes", isPublic: true).Get();
        var badniksArr = GetArrayField<object>(comp, "badniks", isPublic: true).Get();
        var monitorArr = GetArrayField<object>(comp, "monitors", isPublic: true).Get();

        var fldAttachedSound = GetField<AudioClip>(heroArr[0], "attachedSound", isPublic: true);
        var fldContainsIllegalSound = GetField<bool>(heroArr[0], "containsIllegalSound", isPublic: true);
        var fldLabel = GetField<string>(heroArr[0], "label", isPublic: true);

        yield return WaitForSolve;

        var hero = heroArr[GetIntField(comp, "heroIndex").Get(0, heroArr.Length - 1)];
        var badnik = badniksArr[GetIntField(comp, "badnikIndex").Get(0, badniksArr.Length - 1)];
        var monitor = monitorArr[GetIntField(comp, "monitorIndex").Get(0, monitorArr.Length - 1)];

        var badnikName = fldLabel.GetFrom(badnik, v => !SonicKnucklesBadniksSprites.Any(s => s.name == v) ? "not a recognized badnik name" : null);
        var monitorName = fldLabel.GetFrom(monitor, v => !SonicKnucklesMonitorsSprites.Any(s => s.name == v) ? "not a recognized monitor name" : null);
        var illegalSound =
            fldContainsIllegalSound.GetFrom(hero) ? fldAttachedSound.GetFrom(hero) :
            fldContainsIllegalSound.GetFrom(monitor) ? fldAttachedSound.GetFrom(monitor) :
            fldContainsIllegalSound.GetFrom(badnik) ? fldAttachedSound.GetFrom(badnik) :
            throw new AbandonModuleException("None of the three items (hero, monitor, badnik) contain the illegal sound.");

        var usedSounds = new[] { fldAttachedSound.GetFrom(hero), fldAttachedSound.GetFrom(hero), fldAttachedSound.GetFrom(badnik) };
        var allSounds = GetArrayField<AudioClip>(comp, "mushroomSounds", true).Get(expectedLength: 4).Concat(
                GetArrayField<AudioClip>(comp, "noMushroomSounds", true).Get(expectedLength: 20)
            ).ToArray();

        yield return question(SSonicKnuckles.Sounds).Answers(illegalSound, all: allSounds, preferredWrong: usedSounds);
        yield return question(SSonicKnuckles.Badnik).Answers(SonicKnucklesBadniksSprites.First(sprite => sprite.name == badnikName), preferredWrong: SonicKnucklesBadniksSprites);
        yield return question(SSonicKnuckles.Monitor).Answers(SonicKnucklesMonitorsSprites.First(sprite => sprite.name == monitorName), preferredWrong: SonicKnucklesMonitorsSprites);
    }
}