using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPapasPizzeria
{
    [SouvenirQuestion("What was the letter in the order number on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("ACQBJMSD")]
    Letter,

    [SouvenirQuestion("What was the {1} digit in the order number on {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 7)]
    Digit
}

public partial class SouvenirModule
{
    [SouvenirHandler("papasPizzeria", "Papa’s Pizzeria", typeof(SPapasPizzeria), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessPapasPizzeria(ModuleData module)
    {
        var comp = GetComponent(module, "papasPizzeriaScript");
        yield return WaitForSolve;
        var request = GetField<string>(comp, "request").Get(x => Regex.IsMatch(x, @"^[0-7]{3}[ACQBJMSD]$") ? null : "Unexpected order number.");
        addQuestions(module, Enumerable.Range(0, 4).Select(i =>
            makeQuestion(i == 3 ? Question.PapasPizzeriaLetter : Question.PapasPizzeriaDigit, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { request[i].ToString() })));
    }
}