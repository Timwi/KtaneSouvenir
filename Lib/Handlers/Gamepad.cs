using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SGamepad
{
    [SouvenirQuestion("What were the numbers on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("2*0-9", ":", "2*0-9")]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheGamepadModule", "Gamepad", typeof(SGamepad), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessGamepad(ModuleData module)
    {
        var comp = GetComponent(module, "GamepadModule");
        yield return WaitForSolve;

        var x = GetIntField(comp, "x").Get(min: 1, max: 99);
        var y = GetIntField(comp, "y").Get(min: 1, max: 99);
        var display = GetField<GameObject>(comp, "Input", isPublic: true).Get().GetComponent<TextMesh>();
        var digits1 = GetField<GameObject>(comp, "Digits1", isPublic: true).Get().GetComponent<TextMesh>();
        var digits2 = GetField<GameObject>(comp, "Digits2", isPublic: true).Get().GetComponent<TextMesh>();

        if (display == null || digits1 == null || digits2 == null)
            throw new AbandonModuleException($"One of the three displays does not have a TextMesh ({(display == null ? "null" : "not null")}, {(digits1 == null ? "null" : "not null")}, {(digits2 == null ? "null" : "not null")}).");

        yield return question(SGamepad.Numbers).Answers($"{x:00}:{y:00}", preferredWrong: Enumerable.Range(0, int.MaxValue).Select(i => $"{Rnd.Range(1, 99):00}:{Rnd.Range(1, 99):00}").Distinct().Take(6).ToArray());
        digits1.GetComponent<TextMesh>().text = "--";
        digits2.GetComponent<TextMesh>().text = "--";
    }
}
