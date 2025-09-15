using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPie
{
    [SouvenirQuestion("What was the {1} digit of the displayed number in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("pieModule", "Pie", typeof(SPie), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessPie(ModuleData module)
    {
        var comp = GetComponent(module, "PieScript");
        var digits = GetArrayField<string>(comp, "codes").Get(expectedLength: 5);

        yield return WaitForSolve;

        addQuestions(module, digits.Select((digit, ix) => makeQuestion(Question.PieDigits, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { digit }, preferredWrongAnswers: digits)));
    }
}