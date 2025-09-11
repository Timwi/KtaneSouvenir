using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMetamorse
{
    [SouvenirQuestion("What was the extracted letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z")]
    ExtractedLetter
}

public partial class SouvenirModule
{
    [SouvenirHandler("metamorse", "Metamorse", typeof(SMetamorse), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessMetamorse(ModuleData module)
    {
        var comp = GetComponent(module, "MetamorseScript");
        var fldBigChar = GetField<char>(comp, "greaterLetter");

        yield return WaitForSolve;
        addQuestion(module, Question.MetamorseExtractedLetter, correctAnswers: new[] { fldBigChar.Get().ToString() });
    }
}