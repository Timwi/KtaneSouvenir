using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCharacterShift
{
    [SouvenirQuestion("Which letter was present but not submitted on the left slider of {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z")]
    Letters,
    
    [SouvenirQuestion("Which digit was present but not submitted on the right slider of {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("0-9")]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("characterShift", "Character Shift", typeof(SCharacterShift), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessCharacterShift(ModuleData module)
    {
        var comp = GetComponent(module, "characterShift");

        yield return WaitForSolve;

        var leftAnswer = GetField<TextMesh>(comp, "letterText", isPublic: true).Get().text;
        var rightAnswer = GetField<TextMesh>(comp, "numberText", isPublic: true).Get().text;
        var letters = GetArrayField<string>(comp, "letters").Get(expectedLength: 5).Except(new[] { leftAnswer, "*" }).ToArray();
        var digits = GetArrayField<string>(comp, "numbers").Get(expectedLength: 5).Except(new[] { rightAnswer, "*" }).ToArray();

        addQuestions(module,
            makeQuestion(Question.CharacterShiftLetters, module, correctAnswers: letters),
            makeQuestion(Question.CharacterShiftDigits, module, correctAnswers: digits));
    }
}