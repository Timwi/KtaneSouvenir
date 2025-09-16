using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWhoOF
{
    [SouvenirQuestion("What was the display in the {1} stage on {0}?", ThreeColumns6Answers, "FIRST", "OKAY", "C", "BLANK", "YOU", "READ", "YOUR", "UR", "YES", "LED", "THEIR", "RED", "HIRE", "THERE", "THEY", "THING", "CEE", "LEED", "NO", "HOLD", "PLAY", "LEAD", "HARE", "HERE", " ", "REED", "SAYS", "SEE", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("whoOF", "WhoOF", typeof(SWhoOF), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessWhoOF(ModuleData module)
    {
        var comp = GetComponent(module, "whoOFScript");
        var displayTextMesh = GetField<TextMesh>(comp, "Disp_Text", isPublic: true);
        var curStageField = GetField<int>(comp, "stage");
        var storedDisplays = new string[2];
        for (var x = 0; x < 2; x++)
            while (curStageField.Get() == x + 1)
            {
                storedDisplays[x] = displayTextMesh.Get().text;
                yield return new WaitForSeconds(0.1f);
            }
        yield return WaitForSolve;
        for (var stage = 0; stage < storedDisplays.Length; stage++)
            yield return question(SWhoOF.Display, args: [Ordinal(stage + 1)]).Answers(storedDisplays[stage], preferredWrong: storedDisplays);
    }
}
