using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSonicKnuckles
{
    [SouvenirQuestion("Which badnik was shown in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "SonicKnucklesBadniksSprites")]
    Badnik,

    [SouvenirQuestion("Which monitor was shown in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "SonicKnucklesMonitorsSprites")]
    Monitor
}

public partial class SouvenirModule
{
    [SouvenirHandler("sonicKnuckles", "Sonic & Knuckles", typeof(SSonicKnuckles), "Hawker")]
    [SouvenirManualQuestion("Which monitor and badnik were shown?")]
    private IEnumerator<SouvenirInstruction> ProcessSonicKnuckles(ModuleData module)
    {
        var comp = GetComponent(module, "sonicKnucklesScript");

        var badniksArr = GetArrayField<object>(comp, "badniks", isPublic: true).Get();
        var monitorArr = GetArrayField<object>(comp, "monitors", isPublic: true).Get();

        var fldLabel = GetField<string>(badniksArr[0], "label", isPublic: true);

        yield return WaitForSolve;

        var badnik = badniksArr[GetIntField(comp, "badnikIndex").Get(0, badniksArr.Length - 1)];
        var monitor = monitorArr[GetIntField(comp, "monitorIndex").Get(0, monitorArr.Length - 1)];

        var badnikName = fldLabel.GetFrom(badnik, v => !SonicKnucklesBadniksSprites.Any(s => s.name == v) ? "not a recognized badnik name" : null);
        var monitorName = fldLabel.GetFrom(monitor, v => !SonicKnucklesMonitorsSprites.Any(s => s.name == v) ? "not a recognized monitor name" : null);

        yield return question(SSonicKnuckles.Badnik).Answers(SonicKnucklesBadniksSprites.First(sprite => sprite.name == badnikName), preferredWrong: SonicKnucklesBadniksSprites);
        yield return question(SSonicKnuckles.Monitor).Answers(SonicKnucklesMonitorsSprites.First(sprite => sprite.name == monitorName), preferredWrong: SonicKnucklesMonitorsSprites);
    }
}
