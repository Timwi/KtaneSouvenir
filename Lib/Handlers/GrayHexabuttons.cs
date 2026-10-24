using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGrayHexabuttons
{
    [Question("What was the value shown when pressing the {1} button in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["top-left", "top-right", "middle-left", "middle-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(-11, 18)]
    QValues,

    [Discriminator("the Gray Hexabuttons where the {0} button showed the value \u2060{1}", TranslateArguments = [true, false], Arguments = ["top-left", "-7", "top-right", "-2", "middle-left", "3", "middle-right", "8", "bottom-left", "13", "bottom-right", "18"], ArgumentGroupSize = 2)]
    DValues
}

public partial class SouvenirModule
{
    [Handler("grayHexabuttons", "Gray Hexabuttons", typeof(SGrayHexabuttons), "Infusion")]
    [ManualQuestion("What were the values shown when pressing each of the outer buttons?")]
    private IEnumerator<SouvenirInstruction> ProcessGrayHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "grayHexabuttons");
        var results = GetArrayField<int>(comp, "results").Get(expectedLength: 6);
        var positions = new string[] { "top-left", "top-right", "middle-left", "middle-right", "bottom-left", "bottom-right" };

        yield return WaitForSolve;

        for (var i = 0; i < 6; i++)
        {
            yield return new Discriminator(SGrayHexabuttons.DValues, $"value{i}", results[i], args: [positions[i], results[i].ToString()]);
            yield return question(SGrayHexabuttons.QValues, args: [positions[i]]).AvoidDiscriminators($"value{i}").Answers(results[i].ToString());
        }
    }
}
