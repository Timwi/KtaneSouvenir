using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SVexillology
{
    [SouvenirQuestion("What was the {1} flagpole color on {0}?", ThreeColumns6Answers, "Red", "Orange", "Green", "Yellow", "Blue", "Aqua", "White", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("vexillology", "Vexillology", typeof(SVexillology), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessVexillology(ModuleData module)
    {
        var comp = GetComponent(module, "vexillologyScript");

        var colors = GetArrayField<string>(comp, "coloursStrings").Get();
        var color1 = GetIntField(comp, "ActiveFlagTop1").Get(min: 0, max: colors.Length - 1);
        var color2 = GetIntField(comp, "ActiveFlagTop2").Get(min: 0, max: colors.Length - 1);
        var color3 = GetIntField(comp, "ActiveFlagTop3").Get(min: 0, max: colors.Length - 1);

        yield return WaitForSolve;

        yield return question(SVexillology.Colors, args: [Ordinal(1)]).Answers(colors[color1], preferredWrong: [colors[color2], colors[color3]]);
        yield return question(SVexillology.Colors, args: [Ordinal(2)]).Answers(colors[color2], preferredWrong: [colors[color1], colors[color3]]);
        yield return question(SVexillology.Colors, args: [Ordinal(3)]).Answers(colors[color3], preferredWrong: [colors[color2], colors[color1]]);
    }
}
