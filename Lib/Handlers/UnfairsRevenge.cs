using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUnfairsRevenge
{
    [SouvenirQuestion("What was the {1} decrypted instruction in {0}?", ThreeColumns6Answers, "PCR", "PCG", "PCB", "SCC", "SCM", "SCY", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE", "SIG", "PVP", "NXP", "PVS", "NXS", "OPP", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Instructions
}

public partial class SouvenirModule
{
    [SouvenirHandler("unfairsRevenge", "Unfair’s Revenge", typeof(SUnfairsRevenge), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessUnfairsRevenge(ModuleData module)
    {
        var comp = GetComponent(module, "UnfairsRevengeHandler");
        yield return WaitForSolve;

        var instructions = GetListField<string>(comp, "splittedInstructions").Get(expectedLength: 4);

        for (var i = 0; i < 4; i++)
            yield return question(SUnfairsRevenge.Instructions, args: [Ordinal(i + 1)]).Answers(instructions[i]);
    }
}
