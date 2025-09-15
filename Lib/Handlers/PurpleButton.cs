using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPurpleButton
{
    [SouvenirQuestion("What was the {1} number in the cyclic sequence on {0}?", ThreeColumns6Answers, ExampleAnswers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("PurpleButtonModule", "Purple Button", typeof(SPurpleButton), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessPurpleButton(ModuleData module)
    {
        var comp = GetComponent(module, "PurpleButtonScript");
        var cyclingNumbers = GetArrayField<int>(comp, "_cyclingNumbers").Get(expectedLength: 6);
        yield return WaitForSolve;

        var preferredWrongNumbers = Enumerable.Range(0, cyclingNumbers.Max() + 1).ToList();
        while (preferredWrongNumbers.Count < 6)
            preferredWrongNumbers.Add(preferredWrongNumbers.Max() + 1);
        var preferredWrongAnswers = preferredWrongNumbers.Select(n => n.ToString()).ToArray();

        addQuestions(module, Enumerable.Range(0, 6).Select(ix =>
            makeQuestion(SPurpleButton.Numbers, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { cyclingNumbers[ix].ToString() }, preferredWrongAnswers: preferredWrongAnswers)));
    }
}