using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBurgerAlarm
{
    [SouvenirQuestion("What was the {1} displayed digit in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Digits,

    [SouvenirQuestion("What was the {1} order number in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 99, "00")]
    OrderNumbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("burgerAlarm", "Burger Alarm", typeof(SBurgerAlarm), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessBurgerAlarm(ModuleData module)
    {
        var comp = GetComponent(module, "burgerAlarmScript");
        yield return WaitForSolve;

        var displayedNumber = GetArrayField<int>(comp, "number").Get(expectedLength: 7);
        var orders = GetArrayField<string>(comp, "orderStrings").Get(expectedLength: 5);
        for (var pos = 0; pos < 7; pos++)
        {
            yield return question(SBurgerAlarm.Digits, args: [Ordinal(pos + 1)]).Answers(displayedNumber[pos].ToString());
            if (pos < 5)
                yield return question(SBurgerAlarm.OrderNumbers, args: [Ordinal(pos + 1)]).Answers(orders[pos].Replace("no.    ", ""));
        }
    }
}