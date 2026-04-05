using System.Collections;
using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SPrisonBreak
{
    [Question("Which cell did the prisoner start in in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 15)]
    Prisoner,

    [Question("Where did you start in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Concatenate(typeof(AnswerGenerator.Strings), ['A', 'L'], typeof(AnswerGenerator.Integers), [1, 12])]
    Defuser
}

public partial class SouvenirModule
{
    [Handler("prisonBreak", "Prison Break", typeof(SPrisonBreak), "Anonymous")]
    [ManualQuestion("Where did the prisoner and defuser start?")]
    private IEnumerator<SouvenirInstruction> ProcessPrisonBreak(ModuleData module)
    {
        var comp = GetComponent(module, "prisonBreakScript");
        var startPos = GetIntField(comp, "currentPos").Get(v => v is < 0 or > 598 ? "Out of range [0, 598]" : (v % 25) % 2 != 1 ? "Invalid X position" : (v / 25) % 2 != 1 ? "Invalid Y position" : null);
        string error = null;

        IEnumerator waitForReset()
        {
            if (error is not null) yield break;
            var timer = GetField<TextMesh>(comp, "timer", true).Get();
            yield return new WaitUntil(() => timer.text == "-:--");
            startPos = GetIntField(comp, "currentPos").Get();
            error = startPos is < 0 or > 598 ? "Out of range [0, 598]"
                    : (startPos % 25) % 2 != 1 ? "Invalid X position"
                    : (startPos / 25) % 2 != 1 ? "Invalid Y position"
                    : null;
        }
        module.Module.OnStrike += () =>
        {
            StartCoroutine(waitForReset());
            return false;
        };

        yield return WaitForSolve;

        if (error != null)
            throw new AbandonModuleException($"currentPos ({startPos}) invalid: {error}");

        var cell = GetIntField(comp, "cell").Get(min: 0, max: 14);

        yield return question(SPrisonBreak.Prisoner).Answers((cell + 1).ToString());
        yield return question(SPrisonBreak.Defuser).Answers($"{(char) ('A' + (startPos % 25 - 1) / 2)}{(startPos / 25 + 1) / 2}");
    }
}
