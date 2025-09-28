using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRuleOfThree
{
    [SouvenirQuestion("What was the {1} coordinate of the {2} vertex in {0}?", ThreeColumns6Answers, Arguments = ["X", "red", "Y", "yellow", "Z", "blue"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    [AnswerGenerator.Integers(-13, 13)]
    QCoordinates,

    [SouvenirQuestion("What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?", ThreeColumns3Answers, "-", "0", "+", TranslateArguments = [true, false, false], Arguments = ["red", "X", QandA.Ordinal, "yellow", "Y", QandA.Ordinal, "blue", "Z", QandA.Ordinal], ArgumentGroupSize = 3)]
    QCycles,

    [SouvenirDiscriminator("the Rule of Three where the {1} coordinate of the {2} vertex was {0}", Arguments = ["0", "X", "red", "1", "Y", "yellow", "-1", "Z", "blue"], ArgumentGroupSize = 3, TranslateArguments = [false, false, true])]
    DCoordinates,

    [SouvenirDiscriminator("the Rule of Three where the {1} sphere was {0} on the {2} axis in the {3} cycle", TranslateArguments = [true, true, false, false], Arguments = ["positive", "red", "X", QandA.Ordinal, "negative", "yellow", "Y", QandA.Ordinal, "zero", "blue", "Z", QandA.Ordinal], ArgumentGroupSize = 4)]
    DCycles,
}

public partial class SouvenirModule
{
    [SouvenirHandler("RuleOfThreeModule", "Rule of Three", typeof(SRuleOfThree), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessRuleOfThree(ModuleData module)
    {
        var comp = GetComponent(module, "RuleOfThreeScript");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "yellow", "blue" };

        // Coordinates
        var redValues = GetArrayField<int>(comp, "_redValues").Get(expectedLength: 3);
        var yellowValues = GetArrayField<int>(comp, "_yellowValues").Get(expectedLength: 3);
        var blueValues = GetArrayField<int>(comp, "_blueValues").Get(expectedLength: 3);
        var values = new[] { redValues, yellowValues, blueValues };
        for (var color = 0; color < 3; color++)
            for (var coord = 0; coord < 3; coord++)
            {
                yield return new Discriminator(SRuleOfThree.DCoordinates, $"coord-{color}-{coord}", values[color][coord],
                    args: [values[color][coord].ToString(), "XYZ"[coord].ToString(), colorNames[color]]);
                yield return question(SRuleOfThree.QCoordinates, args: ["XYZ"[coord].ToString(), colorNames[color]])
                    .AvoidDiscriminators(SRuleOfThree.DCoordinates)
                    .AvoidDiscriminators(Enumerable.Range(0, 9).Select(i => $"cycle-{color}-{i % 3}-{i / 3}"))
                    .Answers(values[color][coord].ToString());
            }

        // Cycles
        var redCoords = GetArrayField<int[]>(comp, "_redCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var yellowCoords = GetArrayField<int[]>(comp, "_yellowCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var blueCoords = GetArrayField<int[]>(comp, "_blueCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var coords = new[] { redCoords, yellowCoords, blueCoords };
        var coordNamesAns = new[] { "-", "0", "+" };
        var coordNamesDiscr = new[] { "negative", "zero", "positive" };
        for (var color = 0; color < 3; color++)
            for (var axis = 0; axis < 3; axis++)
                for (var cycle = 0; cycle < 3; cycle++)
                {
                    yield return new Discriminator(SRuleOfThree.DCycles, $"cycle-{color}-{axis}-{cycle}", coords[color][cycle][axis],
                        args: [coordNamesDiscr[coords[color][cycle][axis] + 1], colorNames[color], "XYZ"[axis].ToString(), Ordinal(cycle + 1)]);
                    yield return question(SRuleOfThree.QCycles, args: [colorNames[color], "XYZ"[axis].ToString(), Ordinal(cycle + 1)])
                        .AvoidDiscriminators(SRuleOfThree.DCycles)
                        .AvoidDiscriminators(Enumerable.Range(0, 3).Select(coord => $"coord-{color}-{coord}"))
                        .Answers(coordNamesAns[coords[color][cycle][axis] + 1]);
                }
    }
}
