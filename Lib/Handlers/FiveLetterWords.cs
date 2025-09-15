using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFiveLetterWords
{
    [SouvenirQuestion("Which of these words was on the display in {0}?", ThreeColumns6Answers, ExampleAnswers = ["ABAFF", "MAYOR", "PANUS", "FRIZE", "NIRIS", "TEJON"])]
    DisplayedWords
}

public partial class SouvenirModule
{
    [SouvenirHandler("FiveLetterWords", "Five Letter Words", typeof(SFiveLetterWords), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessFiveLetterWords(ModuleData module)
    {
        var comp = GetComponent(module, "FiveLetterWords");

        yield return WaitForSolve;

        var wordList = JsonConvert.DeserializeObject<string[]>(GetField<TextAsset>(comp, "FiverData", isPublic: true).Get().text);
        var displayedWords = GetArrayField<string>(comp, "TheNames").Get(expectedLength: 3, validator: name => name.Length != 5 ? "expected length 5" : null);
        addQuestion(module, Question.FiveLetterWordsDisplayedWords, correctAnswers: displayedWords, preferredWrongAnswers: wordList);
    }
}