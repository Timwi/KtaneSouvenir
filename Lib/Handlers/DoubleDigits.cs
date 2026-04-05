using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDoubleDigits
{
    [Question("What was the digit on the {1} display in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Displays
}

public partial class SouvenirModule
{
    [Handler("doubleDigitsModule", "Double Digits", typeof(SDoubleDigits), "Quinn Wuest")]
    [ManualQuestion("What were the numbers on the displays?")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleDigits(ModuleData module)
    {
        var comp = GetComponent(module, "DoubleDigitsScript");
        yield return WaitForSolve;

        var d = GetArrayField<int>(comp, "digits").Get();
        var digits = Enumerable.Range(0, d.Length).Select(str => d[str].ToString()).ToArray();

        yield return question(SDoubleDigits.Displays, args: ["left"]).Answers(digits[0]);
        yield return question(SDoubleDigits.Displays, args: ["right"]).Answers(digits[1]);
    }
}