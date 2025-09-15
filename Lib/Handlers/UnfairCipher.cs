using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUnfairCipher
{
    [SouvenirQuestion("What was the {1} received instruction in {0}?", ThreeColumns6Answers, "PCR", "PCG", "PCB", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Instructions
}

public partial class SouvenirModule
{
    [SouvenirHandler("unfairCipher", "Unfair Cipher", typeof(SUnfairCipher), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessUnfairCipher(ModuleData module)
    {
        var comp = GetComponent(module, "unfairCipherScript");
        yield return WaitForSolve;

        var instructions = GetArrayField<string>(comp, "Message").Get(expectedLength: 4);
        addQuestions(module,
            makeQuestion(Question.UnfairCipherInstructions, module, formatArgs: new[] { "first" }, correctAnswers: new[] { instructions[0] }),
            makeQuestion(Question.UnfairCipherInstructions, module, formatArgs: new[] { "second" }, correctAnswers: new[] { instructions[1] }),
            makeQuestion(Question.UnfairCipherInstructions, module, formatArgs: new[] { "third" }, correctAnswers: new[] { instructions[2] }),
            makeQuestion(Question.UnfairCipherInstructions, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { instructions[3] }));
    }
}