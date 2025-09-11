using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SQuantumPasswords
{
    [SouvenirQuestion("Which word was used in {0}?", ThreeColumns6Answers, "Argue", "Blaze", "Cajun", "Depth", "Endow", "Foyer", "Gimpy", "Heavy", "Index", "Joker", "Kylix", "Lambs", "Mercy", "Nifty", "Omens", "Pupil", "Risky", "Stoic", "Taboo", "Unbox", "Viced", "Waltz", "Xerus", "Yuzus", "Zilch")]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("quantumPasswords", "Quantum Passwords", typeof(SQuantumPasswords), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessQuantumPasswords(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "QuantumPasswordsScript");
        var words = GetArrayField<string>(comp, "selectedWords").Get(expectedLength: 2, validator: v => v is { Length: 5 } ? null : "Expected word length 5");

        addQuestion(module, Question.QuantumPasswordsWord, correctAnswers: Question.QuantumPasswordsWord.GetAnswers()
            .Where(word => words.Any(w => word.ToUpperInvariant().OrderBy(c => c).SequenceEqual(w.OrderBy(c => c)))).ToArray());
    }
}