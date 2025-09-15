using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SKeypadCombination
{
    [SouvenirQuestion("Which number was displayed on the {1} button, but not part of the answer on {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    WrongNumbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("keypadCombinations", "Keypad Combinations", typeof(SKeypadCombination), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessKeypadCombination(ModuleData module)
    {
        var comp = GetComponent(module, "KeypadCombinations");
        yield return WaitForSolve;

        var buttonNums = GetField<int[,]>(comp, "buttonnum").Get(v => v.GetLength(0) != 4 || v.GetLength(1) != 3 ? "expected 4×3 array" : null);
        var moduleAnswer = GetField<string>(comp, "answer").Get();

        addQuestions(module, Enumerable.Range(0, 4).Select(i => makeQuestion(Question.KeypadCombinationWrongNumbers, module,
            formatArgs: new[] { Ordinal(i + 1) },
            correctAnswers: Enumerable.Range(0, 3).Select(buttonIndex => buttonNums[i, buttonIndex])
                .Where(num => num != moduleAnswer[i] - '0').Select(num => num.ToString()).ToArray())));
    }
}