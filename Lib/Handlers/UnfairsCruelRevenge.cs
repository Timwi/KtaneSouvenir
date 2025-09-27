using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SUnfairsCruelRevenge
{
    [SouvenirQuestion("What was the {1} decrypted instruction in {0}?", ThreeColumns6Answers, "PCR", "PCG", "PCB", "SCC", "SCM", "SCY", "SUB", "PVP", "NXP", "PVS", "NXS", "REP", "EAT", "STR", "IKE", "PRN", "CHK", "MOT", "OPP", "SKP", "INV", "ERT", "SWP", "AGN", "SCN", "FIN", "ISH", "ALE", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Instructions,

    [SouvenirQuestion("What was the {1} decrypted instruction in {0}?", ThreeColumns6Answers, "PCR", "PCG", "PCB", "SCC", "SCM", "SCY", "SUB", "PVP", "NXP", "PVS", "NXS", "REP", "EAT", "STR", "IKE", "PRN", "CHK", "MOT", "OPP", "FIN", "ISH", "ALE", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    InstructionsLegacy
}

public partial class SouvenirModule
{
    [SouvenirHandler("unfairsRevengeCruel", "Unfair's Cruel Revenge", typeof(SUnfairsCruelRevenge), "KiloBites")]
    private IEnumerator<SouvenirInstruction> ProcessUnfairsCruelRevenge(ModuleData module)
    {
        var comp = GetComponent(module, "UnfairsCruelRevengeHandler");
        yield return WaitForSolve;

        var cruelerRevenge = GetField<bool>(comp, "harderUCR").Get();
        var isLegacyUCR = GetField<bool>(comp, "legacyUCR").Get();
        var instructions = GetListField<string>(comp, "splittedInstructions").Get(expectedLength: cruelerRevenge ? 10 : 6);

        for (var i = 0; i < instructions.Count; i++)
            yield return question(isLegacyUCR ? SUnfairsCruelRevenge.InstructionsLegacy : SUnfairsCruelRevenge.Instructions, args: [Ordinal(i + 1)]).Answers(instructions[i]);
    }
}
