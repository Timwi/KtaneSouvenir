using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFreeParking
{
    [SouvenirQuestion("What was the player token in {0}?", ThreeColumns6Answers, "Dog", "Wheelbarrow", "Cat", "Iron", "Top Hat", "Car", "Battleship", TranslateAnswers = true)]
    Token
}

public partial class SouvenirModule
{
    [SouvenirHandler("freeParking", "Free Parking", typeof(SFreeParking), "luisdiogo98")]
    [SouvenirManualQuestion("What was the player token?")]
    private IEnumerator<SouvenirInstruction> ProcessFreeParking(ModuleData module)
    {
        var comp = GetComponent(module, "FreeParkingScript");

        var tokens = GetArrayField<Material>(comp, "tokenOptions", isPublic: true).Get(expectedLength: 7);
        var selected = GetIntField(comp, "tokenIndex").Get(0, tokens.Length - 1);

        yield return WaitForSolve;
        var baseMoney = GetIntField(comp, "baseMoneyInt").Get();
        var originalBaseMoneyStr = GetField<string>(comp, "baseMoneyEdit").Get();
        if (originalBaseMoneyStr == "")
            yield return legitimatelyNoQuestion(module, "Unicorn rule applied.");

        if (!int.TryParse(originalBaseMoneyStr, out var originalBaseMoney))
            throw new AbandonModuleException($"not a valid integer: {originalBaseMoneyStr}");

        originalBaseMoney %= 5000;

        if (baseMoney == originalBaseMoney)
            yield return legitimatelyNoQuestion(module, "Base money never changed. Assuming no solved modules and no relevant modules were present.");

        yield return question(SFreeParking.Token).Answers(tokens[selected].name);
    }
}
