using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SHickoryDickoryDock
{
    [SouvenirQuestion("What time was shown when the clock struck {1} on {0}?", ThreeColumns6Answers, Arguments = ["1:00", "2:00", "3:00", "4:00", "5:00", "6:00", "7:00", "8:00", "9:00", "10:00", "11:00", "12:00"], ArgumentGroupSize = 1)]
    [AnswerGenerator.HickoryDickoryDock]
    Time,

    [SouvenirDiscriminator("the Hickory Dickory Dock which showed {0} when it struck {1}", Arguments = ["1:30", "2:00"], ArgumentGroupSize = 3)]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("hickoryDickoryDockModule", "Hickory Dickory Dock", typeof(SHickoryDickoryDock), "Anonymous", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessHickoryDickoryDock(ModuleData module)
    {
        // Wait until _either_ the module solves itself, _or_ no unignored modules are left
        while (!_noUnignoredModulesLeft && module.Unsolved)
            yield return new WaitForSeconds(.1f);

        var comp = GetComponent(module, "HickoryDickoryDockScript");
        var stageObjects = GetField<IList>(comp, "generatedStages").Get();

        if (stageObjects.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages generated.");

        var fldMinute = GetField<string>(stageObjects[0], "minuteDirection", isPublic: true);
        var fldHour = GetField<int>(stageObjects[0], "index", isPublic: true);
        var fldChimes = GetField<int>(stageObjects[0], "chimes", isPublic: true);

        var validMinutes = new[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
        var minuteValues = new[] { 0, 7, 15, 22, 30, 37, 45, 52 };

        var stages = new (int h, int m)[12];

        foreach (var stage in stageObjects)
        {
            var m = fldMinute.GetFrom(stage, v => !validMinutes.Contains(v) ? "Expected a cardinal or secondary direction" : null);
            var h = fldHour.GetFrom(stage, v => v is < 0 or > 11 ? "Expected a number [0, 11]" : null);
            var c = fldChimes.GetFrom(stage, v => v is < 1 or > 12 ? "Expected a number [1, 12]" : null);

            stages[c - 1] = (h + 1, Array.IndexOf(validMinutes, m));
        }

        for (var c = 0; c < stages.Length; c++)
            if (stages[c] is not (0, 0))
            {
                var struck = $"{c + 1}:00";
                var time = $"{stages[c].h}:{minuteValues[stages[c].m]:00}";
                yield return question(SHickoryDickoryDock.Time, args: [struck]).Answers(time);
                yield return new Discriminator(SHickoryDickoryDock.Discriminator, $"stage{c}", time, args: [time, struck]);
            }
    }
}
