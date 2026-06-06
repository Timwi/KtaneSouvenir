using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SNotWhosOnFirst
{
    [Question("What was the display in the {1} stage on {0}?", TwoColumns4Answers, "BLANK", "C", "CEE", "DISPLAY", "FIRST", "HOLD ON", "LEAD", "LED", "LEED", "NO", "NOTHING", "OK", "OKAY", "READ", "RED", "REED", "SAY", "SAYS", "SEE", "THEIR", "THERE", "THEY ARE", "THEY’RE", "U", "UR", "YES", "YOU", "YOU ARE", "YOU’RE", "YOUR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [Handler("NotWhosOnFirst", "Not Who’s on First", typeof(SNotWhosOnFirst), "Espik")]
    [ManualQuestion("What were the first four display words?")]
    private IEnumerator<SouvenirInstruction> ProcessNotWhosOnFirst(ModuleData module)
    {
        var comp = GetComponent(module, "NotWhosOnFirst");
        var fldStage = GetProperty<int>(comp, "StagesCompleted", isPublic: true);
        var displays = new string[5];

        var connector = GetProperty<object>(comp, "Connector").Get();
        var propDisplayText = GetProperty<string>(connector, "DisplayText", isPublic: true);

        while (module.Unsolved)
        {
            var stage = fldStage.Get();
            displays[stage] = propDisplayText.Get();

            yield return null;
        }

        for (var i = 0; i < 4; i++)
            yield return question(SNotWhosOnFirst.Display, args: [Ordinal(i + 1)]).Answers(displays[i]);
    }
}
