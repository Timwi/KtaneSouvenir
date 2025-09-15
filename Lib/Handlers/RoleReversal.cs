using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRoleReversal
{
    [SouvenirQuestion("How many {1} wires were there in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", Arguments = ["warm-colored", "cold-colored", "primary-colored", "secondary-colored"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Wires,

    [SouvenirQuestion("What was the number corresponding to the correct condition in {0}?", ThreeColumns6Answers, "2", "3", "4", "5", "6", "7", "8")]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("roleReversal", "Role Reversal", typeof(SRoleReversal), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessRoleReversal(ModuleData module)
    {
        var comp = GetComponent(module, "roleReversal");
        yield return WaitForSolve;

        var redWires = GetListField<byte>(comp, "redWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var orangeWires = GetListField<byte>(comp, "orangeWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var yellowWires = GetListField<byte>(comp, "yellowWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var greenWires = GetListField<byte>(comp, "greenWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var blueWires = GetListField<byte>(comp, "blueWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var purpleWires = GetListField<byte>(comp, "purpleWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);

        var totalWires = redWires.Count + orangeWires.Count + yellowWires.Count + greenWires.Count + blueWires.Count + purpleWires.Count;
        if (totalWires is < 2 or > 7)
            throw new AbandonModuleException($"All wires combined has unexpected value (expected 2-7): {totalWires}");

        var answerIndex = GetField<byte>(comp, "souvenir").Get(b => b is < 2 or > 8 ? "expected range 2–8" : null);
        addQuestions(module,
            makeQuestion(Question.RoleReversalWires, module, formatArgs: new[] { "warm-colored" }, correctAnswers: new[] { (redWires.Count + orangeWires.Count + yellowWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, module, formatArgs: new[] { "cold-colored" }, correctAnswers: new[] { (greenWires.Count + blueWires.Count + purpleWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, module, formatArgs: new[] { "primary-colored" }, correctAnswers: new[] { (redWires.Count + yellowWires.Count + blueWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, module, formatArgs: new[] { "secondary-colored" }, correctAnswers: new[] { (orangeWires.Count + greenWires.Count + purpleWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalNumber, module, correctAnswers: new[] { answerIndex.ToString() }, preferredWrongAnswers: new[] { "2", "3", "4", "5", "6", "7", "8" }));
    }
}