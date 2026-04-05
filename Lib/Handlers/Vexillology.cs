using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SVexillology
{
    [Question("What was the {1} flagpole color on {0}?", ThreeColumns6Answers, "Red", "Orange", "Green", "Yellow", "Blue", "Aqua", "White", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [Handler("vexillology", "Vexillology", typeof(SVexillology), "luisdiogo98")]
    [ManualQuestion("What were the flagpole colors?")]
    private IEnumerator<SouvenirInstruction> ProcessVexillology(ModuleData module)
    {
        var comp = GetComponent(module, "vexillologyScript");

        var colors = GetArrayField<string>(comp, "coloursStrings").Get();
        var color1 = GetIntField(comp, "ActiveFlagTop1").Get(min: 0, max: colors.Length - 1);
        var color2 = GetIntField(comp, "ActiveFlagTop2").Get(min: 0, max: colors.Length - 1);
        var color3 = GetIntField(comp, "ActiveFlagTop3").Get(min: 0, max: colors.Length - 1);

        yield return WaitForSolve;

        var flagType = GetIntField(comp, "ActiveFlag").Get(min: 0, max: 4);
        var answer1 = GetIntField(comp, "AnswerColour1").Get(min: 0, max: colors.Length - 1);

        if (flagType == 1 && answer1 == 1)
            yield return legitimatelyNoQuestion(module, "This specific answer can be reverse engineered.");

        yield return question(SVexillology.Colors, args: [Ordinal(1)]).Answers(colors[color1], preferredWrong: [colors[color2], colors[color3]]);
        yield return question(SVexillology.Colors, args: [Ordinal(2)]).Answers(colors[color2], preferredWrong: [colors[color1], colors[color3]]);

        if (!(flagType >= 2 && answer1 == 2)) // This specific answer can be reverse engineered
            yield return question(SVexillology.Colors, args: [Ordinal(3)]).Answers(colors[color3], preferredWrong: [colors[color2], colors[color1]]);
    }
}
