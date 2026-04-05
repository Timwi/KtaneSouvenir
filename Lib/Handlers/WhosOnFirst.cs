using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWhosOnFirst
{
    [Question("What was the display in the {1} stage on {0}?", TwoColumns4Answers, "", "BLANK", "C", "CEE", "DISPLAY", "FIRST", "HOLD ON", "LEAD", "LED", "LEED", "NO", "NOTHING", "OK", "OKAY", "READ", "RED", "REED", "SAY", "SAYS", "SEE", "THEIR", "THERE", "THEY ARE", "THEY’RE", "U", "UR", "YES", "YOU", "YOU ARE", "YOU’RE", "YOUR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QDisplay,

    [Discriminator("the Who’s on First that had {0} in the display in the {1} stage", Arguments = ["BLANK", QandA.Ordinal], ArgumentGroupSize = 2)]
    DDisplay
}

public partial class SouvenirModule
{
    [Handler("WhosOnFirst", "Who’s on First", typeof(SWhosOnFirst), "Andrio Celos")]
    [ManualQuestion("What were the display words?")]
    private IEnumerator<SouvenirInstruction> ProcessWhosOnFirst(ModuleData module)
    {
        var comp = GetComponent(module, "WhosOnFirstComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);
        var propStage = GetProperty<int>(comp, "CurrentStage", true);
        var propButtonsEmerged = GetProperty<bool>(comp, "ButtonsEmerged", true);
        var displayTextMesh = GetField<MonoBehaviour>(comp, "DisplayText", true).Get(); // TextMeshPro
        var propText = GetProperty<string>(displayTextMesh, "text", true);

        while (!propButtonsEmerged.Get())
            yield return new WaitForSeconds(0.1f);

        var displayWords = new string[2];
        for (var i = 0; i < 2; i++)
            while (propStage.Get() == i)
            {
                while (!propButtonsEmerged.Get())
                    yield return new WaitForSeconds(0.1f);

                displayWords[i] = propText.Get().Replace("'", "’");

                while (propButtonsEmerged.Get())
                    yield return new WaitForSeconds(0.1f);
            }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        module.SolveIndex = module.Info.NumSolved++;

        for (var stage = 0; stage < displayWords.Length; stage++)
        {
            if (displayWords[stage].Length > 0)
                yield return new Discriminator(SWhosOnFirst.DDisplay, $"display-{stage}", displayWords[stage], args: [displayWords[stage], Ordinal(stage + 1)]);
            yield return question(SWhosOnFirst.QDisplay, args: [Ordinal(stage + 1)])
                .AvoidDiscriminators($"display-{stage}")
                .Answers(displayWords[stage], preferredWrong: displayWords);
        }
    }
}
