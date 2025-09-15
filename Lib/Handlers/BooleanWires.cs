using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBooleanWires
{
    [SouvenirQuestion("Which operator did you submit in the {1} stage of {0}?", TwoColumns4Answers, "OR", "XOR", "AND", "NAND", "NOR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    EnteredOperators
}

public partial class SouvenirModule
{
    [SouvenirHandler("booleanWires", "Boolean Wires", typeof(SBooleanWires), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessBooleanWires(ModuleData module)
    {
        var comp = GetComponent(module, "BooleanWiresScript");

        yield return WaitForSolve;

        var operators = GetListField<string>(comp, "Entered", isPublic: true).Get(expectedLength: 10);
        for (var pos = 0; pos < 5; pos++)
            yield return question(SBooleanWires.EnteredOperators, args: [Ordinal(pos + 1)]).Answers(operators[2 * pos]);
    }
}