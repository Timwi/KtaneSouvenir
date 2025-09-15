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
        addQuestions(module,
            makeQuestion(Question.UnfairsRevengeInstructions, module, formatArgs: new[] { "first" }, correctAnswers: new[] { instructions[0] }),
            makeQuestion(Question.UnfairsRevengeInstructions, module, formatArgs: new[] { "second" }, correctAnswers: new[] { instructions[1] }),
            makeQuestion(Question.UnfairsRevengeInstructions, module, formatArgs: new[] { "third" }, correctAnswers: new[] { instructions[2] }),
            makeQuestion(Question.UnfairsRevengeInstructions, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { instructions[3] }));
    }
}