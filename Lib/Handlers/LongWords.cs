using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLongWords
{
    [SouvenirQuestion("What was the word on the top display on {0}?", ThreeColumns6Answers, ExampleAnswers = ["ABOARD", "ABRUPT", "SAFEST", "LAMBDA", "NARROW", "ECHOES", "VALVES", "YONDER", "ZIGGED", "UNBIND"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("LongWords", "Long Words", typeof(SLongWords), "GoodHood")]
    private IEnumerator<SouvenirInstruction> ProcessLongWords(ModuleData module)
    {
        var comp = GetComponent(module, "LongWords");
        var fldPossibleWords = GetField<List<string>>(comp, "SixLetterWords");
        var word = GetField<string>(comp, "chosenSixLetterWord").Get(str => str.Length != 6 ? $"length is {str.Length} instead of 6" : null);

        yield return WaitForSolve;

        addQuestion(module, Question.LongWordsWord, correctAnswers: new[] { word }, preferredWrongAnswers: fldPossibleWords.Get().ToArray());
    }
}