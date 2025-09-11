using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNotMorsematics
{
    [SouvenirQuestion("What was the transmitted word on {0}?", ThreeColumns6Answers, ExampleAnswers = ["ABORT", "AFTER", "AGONY", "ALIGN", "AMONG", "AMBER", "ANGST", "AZURE", "BAKER", "BAYOU", "BEACH", "BLACK", "BOGUS", "BOXES", "BRASH", "BUDGE", "CABLE", "CAULK", "CHIEF", "CLOVE", "CODEX", "CRAZE", "CRISP", "CRUEL"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("notMorsematics", "Not Morsematics", typeof(SNotMorsematics), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotMorsematics(ModuleData module)
    {
        var comp = GetComponent(module, "NMorScript");
        yield return WaitForSolve;

        var word = GetArrayField<string>(comp, "word").Get(expectedLength: 2);
        var wordList = GetArrayField<string>(comp, "keywords").Get();

        var wordLower = word[0].Substring(0, 1) + word[0].Substring(1).ToLowerInvariant();
        var wordListLower = Enumerable.Range(0, wordList.Length).Select(word => wordList[word].Substring(0, 1) + wordList[word].Substring(1).ToLowerInvariant()).ToArray();

        addQuestion(module, Question.NotMorsematicsWord, correctAnswers: new[] { wordLower }, preferredWrongAnswers: wordListLower);
    }
}