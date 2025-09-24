using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSimonSwizzles
{
    [SouvenirQuestion("Where was {1} in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, ArgumentGroupSize = 1, TranslateArguments = [true], Arguments = ["OFF", "ON"])]
    [AnswerGenerator.Grid(4, 4)]
    Button,

    [SouvenirQuestion("What was the hidden number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("6*01")]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSwizzles", "Simon Swizzles", typeof(SSimonSwizzles), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSwizzles(ModuleData module)
    {
        yield return WaitForSolve;
        var comp = GetComponent(module, "flashingnonsense");
        var hidden = GetField<string>(comp, "inside").Get(v => !Regex.IsMatch(v, "^[01]{6}$") ? "Expected six-bit binary string" : null);
        var gates = GetArrayField<int>(comp, "Gatestore").Get(expectedLength: 16, validator: v => v is < 0 or > 15 ? "Expected gates in range 0–15" : null);
        if (gates.Distinct().Count() is not 16)
            throw new AbandonModuleException($"Expected 16 distinct gates, got {gates.Stringify()}");

        var gateNames = new[] { "OFF", "ON" };
        for (var i = 0; i < gates.Length; i++)
            if (gates[i] is 0 or 15)
                yield return question(SSimonSwizzles.Button, args: [gateNames[gates[i] / 15]]).Answers(Sprites.GenerateGridSprite(4, 4, i));
        yield return question(SSimonSwizzles.Number).Answers(hidden);
    }
}
