using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SColorAddition
{
    [SouvenirQuestion("What was {1}’s number in {0}?", ThreeColumns6Answers, Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings(3, "0123456789")]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("colorAddition", "Color Addition", typeof(SColorAddition), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessColorAddition(ModuleData module)
    {
        var script = GetComponent(module, "ColorAddition");
        var numbersField = GetArrayField<string>(script, "numbers");
        yield return WaitForSolve;

        var numbersObtained = numbersField.Get(expectedLength: 3);
        var channelRefs = new[] { "red", "green", "blue" };
        for (var idx = 0; idx < channelRefs.Length; idx++)
            yield return question(SColorAddition.Numbers, args: [channelRefs[idx]]).Answers(numbersObtained[idx]);
    }
}