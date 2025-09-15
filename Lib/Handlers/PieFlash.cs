using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPieFlash
{
    [SouvenirQuestion("What number was not displayed in {0}?", TwoColumns4Answers, ExampleAnswers = ["31415", "62643", "28410", "93105"])]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("pieFlash", "Pie Flash", typeof(SPieFlash), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessPieFlash(ModuleData module)
    {
        var comp = GetComponent(module, "pieFlashScript");
        var digits = GetArrayField<string>(comp, "codes").Get(expectedLength: 3);

        yield return WaitForSolve;

        // Find valid answers within pi that do not overlap with any of the other strings
        var piString = GetField<string>(comp, "pi").Get();
        var validAnswers = Enumerable.Range(0, piString.Length - 5).Select(ix => piString.Substring(ix, 5)).Where(sps => !digits.Contains(sps)).ToArray();
        addQuestion(module, Question.PieFlashDigits, correctAnswers: validAnswers, preferredWrongAnswers: digits);
    }
}