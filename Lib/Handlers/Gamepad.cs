using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SGamepad
{
    [Question("What was the {1} digit on the display on {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QNumbers,

    [Discriminator("the Gamepad whose {1} digit on the display was {0}", Arguments = ["0", QandA.Ordinal], ArgumentGroupSize = 2)]
    DNumbers,
}

public partial class SouvenirModule
{
    [Handler("TheGamepadModule", "Gamepad", typeof(SGamepad), "Timwi", AddThe = true)]
    [ManualQuestion("What were the numbers?")]
    private IEnumerator<SouvenirInstruction> ProcessGamepad(ModuleData module)
    {
        var comp = GetComponent(module, "GamepadModule");
        yield return WaitForSolve;

        var x = GetIntField(comp, "x").Get(min: 1, max: 99);
        var y = GetIntField(comp, "y").Get(min: 1, max: 99);
        var digits1 = GetField<GameObject>(comp, "Digits1", isPublic: true).Get().GetComponent<TextMesh>();
        var digits2 = GetField<GameObject>(comp, "Digits2", isPublic: true).Get().GetComponent<TextMesh>();

        var digits = new[] { x / 10, x % 10, y / 10, y % 10 };

        if (digits1 == null || digits2 == null)
            throw new AbandonModuleException($"One of the three displays does not have a TextMesh ({(digits1 == null ? "null" : "not null")}, {(digits2 == null ? "null" : "not null")}).");

        digits1.GetComponent<TextMesh>().text = "--";
        digits2.GetComponent<TextMesh>().text = "--";

        for (int i = 0; i < 4; i++)
        {
            yield return new Discriminator(SGamepad.DNumbers, $"digit-{i}", digits[i], args: [digits[i].ToString(), Ordinal(i + 1)]);
            yield return question(SGamepad.QNumbers, args: [Ordinal(i + 1)])
                .AvoidDiscriminators($"digit-{i}")
                .Answers(digits[i].ToString());
        }
    }
}
