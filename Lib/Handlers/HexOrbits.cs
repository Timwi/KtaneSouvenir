using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHexOrbits
{
    [SouvenirQuestion("What was the {1} shape for the {2} display in {0}?", TwoColumns4Answers, "Square", "Pentagon", "Hexagon", "Heptagon", Arguments = ["fast", QandA.Ordinal, "slow", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Shape
}

public partial class SouvenirModule
{
    [SouvenirHandler("hexOrbits", "hexOrbits", typeof(SHexOrbits), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessHexOrbits(ModuleData module)
    {
        var comp = GetComponent(module, "HexOrbitsScript");
        yield return WaitForSolve;

        var stages = GetArrayField<int>(comp, "stageValues").Get(expectedLength: 5, validator: v => v is < 0 or > 15 ? $"Bad stage value {v}" : null);
        var shapes = new[] { "Square", "Pentagon", "Hexagon", "Heptagon" };
        for (var i = 0; i < 4; i++)
        {
            yield return question(SHexOrbits.Shape, args: ["slow", Ordinal(i + 1)]).Answers(shapes[stages[i] / 4]);
            yield return question(SHexOrbits.Shape, args: ["fast", Ordinal(i + 1)]).Answers(shapes[stages[i] % 4]);
        }
    }
}
