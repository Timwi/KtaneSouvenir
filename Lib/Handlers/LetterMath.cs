using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLetterMath
{
    [SouvenirQuestion("What was the letter on the {1} display in {0}?", ThreeColumns6Answers, Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    [AnswerGenerator.Strings("A-Z")]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("letterMath", "Letter Math", typeof(SLetterMath), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessLetterMath(ModuleData module)
    {
        var comp = GetComponent(module, "LetterMathModule");

        var characters = GetArrayField<int>(comp, "characters").Get();
        var letters = Enumerable.Range(0, 2).ToArray().Select(i => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(characters[i], 1)).ToArray();

        yield return WaitForSolve;

        var wrongLetters = Enumerable.Range(0, 26).ToArray().Select(i => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(i, 1)).ToArray();

        addQuestions(module,
            makeQuestion(Question.LetterMathDisplay, module, formatArgs: new[] { "left" }, correctAnswers: new[] { letters[0] }, preferredWrongAnswers: wrongLetters),
            makeQuestion(Question.LetterMathDisplay, module, formatArgs: new[] { "right" }, correctAnswers: new[] { letters[1] }, preferredWrongAnswers: wrongLetters));
    }
}