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
    Time
}

public partial class SouvenirModule
{
    [SouvenirHandler("hickoryDickoryDockModule", "Hickory Dickory Dock", typeof(SHickoryDickoryDock), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessHickoryDickoryDock(ModuleData module)
    {
        _hickoryDickoryDocksUnsolved++;

        while (!_noUnignoredModulesLeft && module.Unsolved)
            yield return new WaitForSeconds(.1f);

        _hickoryDickoryDocksUnsolved--;

        while (_hickoryDickoryDocksUnsolved != 0)
            yield return null;

        var comp = GetComponent(module, "HickoryDickoryDockScript");
        var stageObjects = GetField<IList>(comp, "generatedStages").Get();

        if (stageObjects.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages generated.");

        var fldMinute = GetField<string>(stageObjects[0], "minuteDirection", isPublic: true);
        var fldHour = GetField<int>(stageObjects[0], "index", isPublic: true);
        var fldChimes = GetField<int>(stageObjects[0], "chimes", isPublic: true);

        var validMinutes = new[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };

        var stages = new (int h, int m)[12];
        _hickoryDickoryDockStages.Add(stages);

        foreach (var stage in stageObjects)
        {
            var m = fldMinute.GetFrom(stage, v => !validMinutes.Contains(v) ? "Expected a cardinal or secondary direction" : null);
            var h = fldHour.GetFrom(stage, v => v is < 0 or > 11 ? "Expected a number [0, 11]" : null);
            var c = fldChimes.GetFrom(stage, v => v is < 1 or > 12 ? "Expected a number [1, 12]" : null);

            stages[c - 1] = (h + 1, Array.IndexOf(validMinutes, m));
        }

        yield return null;

        if (_moduleCounts["hickoryDickoryDockModule"] is 1)
        {
            addQuestions(module, stages
                .Select((t, c) => (t, c))
                .Where(t => t.t is not (0, 0))
                .Select(t => makeQuestion(
                    question: SHickoryDickoryDock.Time,
                    moduleId: module.Module.ModuleType, solveIx: 1,
                    formatArgs: new[] { $"{t.c + 1}:00" },
                    correctAnswers: new[] { $"{t.t.h}:{_hickoryDickoryDockMinutes[t.t.m]:00}" })));
            yield break;
        }

        List<QandA> qs = new();
        for (var i = 0; i < 12; i++)
        {
            if (stages[i] is (0, 0))
                continue;

            var unique = Enumerable.Range(0, 12).Where(s => s != i && stages[s] is not (0, 0) && _hickoryDickoryDockStages.Count(d => d[s] == stages[s]) is 1);
            if (!unique.Any())
                continue;

            var used = unique.PickRandom();
            var format = string.Format(
                translateString(SHickoryDickoryDock.Time, "the Hickory Dickory Dock which showed {0}:{1:00} when it struck {2}"),
                stages[used].h, _hickoryDickoryDockMinutes[stages[used].m], $"{used + 1}:00");

            qs.Add(makeQuestion(SHickoryDickoryDock.Time, module, formattedModuleName: format, formatArgs: new[] { $"{i + 1}:00" }, correctAnswers: new[] { $"{stages[i].h}:{_hickoryDickoryDockMinutes[stages[i].m]:00}" }));
        }

        if (qs.Count == 0)
            yield return legitimatelyNoQuestion(module, $"There were not enough stages where this one (#{GetIntField(comp, "moduleId").Get()}) was unique.");

        addQuestions(module, qs);
    }
}
