using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSignLanguage
{
    [SouvenirQuestion("What was the deciphered word in {0}?", TwoColumns4Answers, "PHALANX", "DIGITAL", "ACHIRAL", "DEAFENS", "LISTENS", "EXPLAIN", "SPEAKER", "TURTLES", "QUOTING", "MISTAKE", "REALIZE", "HELPERS", "HEARING", "STROKES", "OVERJOY", "ROYALTY", "EARDRUM", "COCHLEA", "AUDIBLE", "KABOOMS", "REFUGEE", "SWINGER", "BALANCE", "LIQUIDS", "VOYAGED")]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("signLanguage", "Sign Language", typeof(SSignLanguage), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessSignLanguage(ModuleData module)
    {
        var comp = GetComponent(module, "SignLanguageAlphabetScript");
        yield return WaitForSolve;

        var entryObj = GetField<object>(comp, "entry").Get();
        var answer = GetField<string>(entryObj, "word").Get();

        addQuestion(module, Question.SignLanguageWord, correctAnswers: new[] { answer });
    }
}