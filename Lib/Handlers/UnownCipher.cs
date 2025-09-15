using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUnownCipher
{
    [SouvenirQuestion("What was the {1} submitted letter in {0}?", ThreeColumns6Answers, Type = AnswerType.UnownFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    Answers
}

public partial class SouvenirModule
{
    [SouvenirHandler("UnownCipher", "Unown Cipher", typeof(SUnownCipher), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessUnownCipher(ModuleData module)
    {
        var comp = GetComponent(module, "UnownCipher");
        yield return WaitForSolve;

        var unownAnswer = GetArrayField<int>(comp, "letterIndexes").Get(expectedLength: 5, validator: v => v is < 0 or > 25 ? "expected 0–25" : null);
        addQuestions(module, unownAnswer.Select((ans, i) => makeQuestion(Question.UnownCipherAnswers, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { ((char) ('A' + ans)).ToString() })));
    }
}