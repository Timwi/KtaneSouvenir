using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWordCount
{
    [SouvenirQuestion("What was the displayed number on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 1000)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("wordCount", "Word Count", typeof(SWordCount), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessWordCount(ModuleData module)
    {
        var comp = GetComponent(module, "WordCountScript");
        yield return WaitForSolve;

        var displayNumber = GetIntField(comp, "DisplayNumber").Get();

        addQuestion(module, Question.WordCountNumber, correctAnswers: new[] { displayNumber.ToString() });
    }
}