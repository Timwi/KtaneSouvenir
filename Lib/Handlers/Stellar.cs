using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SStellar
{
    [SouvenirQuestion("What was the {1} letter in {0}?", ThreeColumns6Answers, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", Arguments = ["Morse code", "tap code", "Braille"], TranslateArguments = [true], ArgumentGroupSize = 1)]
    Letters
}

public partial class SouvenirModule
{
    [SouvenirHandler("stellar", "Stellar", typeof(SStellar), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessStellar(ModuleData module)
    {
        var comp = GetComponent(module, "StellarScript");
        yield return WaitForSolve;

        var lastPlayed = GetField<string>(comp, "lastPlayed").Get(validator: str => str.Length != 3 ? "expected length 3" : str.Any(ch => ch is < 'a' or > 'z') ? "expected letters a–z" : null);
        var allLetters = lastPlayed.Select(c => c.ToString()).ToArray();
        addQuestions(module,
            makeQuestion(Question.StellarLetters, module, formatArgs: new[] { "Braille" }, correctAnswers: new[] { lastPlayed[0].ToString() }, preferredWrongAnswers: allLetters),
            makeQuestion(Question.StellarLetters, module, formatArgs: new[] { "tap code" }, correctAnswers: new[] { lastPlayed[1].ToString() }, preferredWrongAnswers: allLetters),
            makeQuestion(Question.StellarLetters, module, formatArgs: new[] { "Morse code" }, correctAnswers: new[] { lastPlayed[2].ToString() }, preferredWrongAnswers: allLetters));
    }
}