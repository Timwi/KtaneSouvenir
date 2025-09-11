using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCruelBinary
{
    [SouvenirQuestion("What was the displayed word in {0}?", TwoColumns4Answers, ExampleAnswers = ["LEAST", "YELLOW", "SIERRA", "WHITE"])]
    DisplayedWord
}

public partial class SouvenirModule
{
    [SouvenirHandler("CruelBinary", "Cruel Binary", typeof(SCruelBinary), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessCruelBinary(ModuleData module)
    {
        var comp = GetComponent(module, "CruelBinary");

        yield return WaitForSolve;

        var wordList = GetArrayField<string>(comp, "_WordList", isPublic: true).Get();
        var displayedWord = GetField<string>(comp, "h", isPublic: true).Get();
        addQuestion(module, Question.CruelBinaryDisplayedWord, correctAnswers: new[] { displayedWord }, preferredWrongAnswers: wordList);
    }
}