using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDoubleExpert
{
    [SouvenirQuestion("What was the starting key number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(30, 69)]
    StartingKeyNumber,
    
    [SouvenirQuestion("What was the word you submitted in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Echo", "November", "Rodeo", "Words", "Victor", "Zulu"])]
    SubmittedWord
}

public partial class SouvenirModule
{
    [SouvenirHandler("doubleExpert", "Double Expert", typeof(SDoubleExpert), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleExpert(ModuleData module)
    {
        var comp = GetComponent(module, "doubleExpertScript");

        yield return WaitForSolve;

        var startingKeyNumber = GetIntField(comp, "startKeyNumber").Get(min: 30, max: 69);
        var keywords = GetListField<string>(comp, "keywords").Get().ToArray();
        var correctKeywordIndex = GetIntField(comp, "correctKeyword").Get(min: 0, max: keywords.Length - 1);

        addQuestions(
            module,
            makeQuestion(Question.DoubleExpertStartingKeyNumber, module, correctAnswers: new[] { startingKeyNumber.ToString() }),
            makeQuestion(Question.DoubleExpertSubmittedWord, module, correctAnswers: new[] { keywords[correctKeywordIndex] }, preferredWrongAnswers: keywords)
        );
    }
}