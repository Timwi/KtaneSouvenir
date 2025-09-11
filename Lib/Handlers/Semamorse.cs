using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSemamorse
{
    [SouvenirQuestion("What was the color of the display involved in the starting value in {0}?", TwoColumns4Answers, "red", "green", "cyan", "indigo", "pink", TranslateAnswers = true)]
    Color,
    
    [SouvenirQuestion("What was the {1} letter involved in the starting value in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", Arguments = ["Morse", "semaphore"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Letters
}

public partial class SouvenirModule
{
    [SouvenirHandler("semamorse", "Semamorse", typeof(SSemamorse), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSemamorse(ModuleData module)
    {
        var comp = GetComponent(module, "semamorse");
        yield return WaitForSolve;

        var letters = GetArrayField<int[]>(comp, "displayedLetters").Get(expectedLength: 2, validator: arr => arr.Length != 5 ? "expected length 5" : arr.Any(v => v is < 0 or > 25) ? "expected range 0–25" : null);
        var relevantIx = Enumerable.Range(0, letters[0].Length).First(ix => letters[0][ix] != letters[1][ix]);
        var colorNames = new[] { "red", "green", "cyan", "indigo", "pink" };
        var colors = GetArrayField<int>(comp, "displayedColors").Get(expectedLength: 5, validator: c => c < 0 || c >= colorNames.Length ? $"expected range 0–{colorNames.Length - 1}" : null);
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.SemamorseColor, module, correctAnswers: new[] { colorNames[colors[relevantIx]] }));
        qs.Add(makeQuestion(Question.SemamorseLetters, module, formatArgs: new[] { "semaphore" }, correctAnswers: new[] { ((char) ('A' + letters[0][relevantIx])).ToString() }));
        qs.Add(makeQuestion(Question.SemamorseLetters, module, formatArgs: new[] { "Morse" }, correctAnswers: new[] { ((char) ('A' + letters[1][relevantIx])).ToString() }));
        addQuestions(module, qs);
    }
}