using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMetapuzzle
{
    [SouvenirQuestion("What was the final answer in {0}?", TwoColumns4Answers, ExampleAnswers = ["GIBBONS", "GIRAFFE", "MISUSED", "RUSHING", "DUSTMAN", "STATICS"])]
    Answer
}

public partial class SouvenirModule
{
    [SouvenirHandler("metapuzzle", "Metapuzzle", typeof(SMetapuzzle), "GoodHood")]
    private IEnumerator<SouvenirInstruction> ProcessMetapuzzle(ModuleData module)
    {
        var comp = GetComponent(module, "metapuzzleScript");

        var wordsType = comp.GetType().Assembly.GetType("SevenLetterWords") ?? throw new AbandonModuleException("I cannot find the SevenLetterWords type.");
        var words = GetStaticField<string[]>(wordsType, "List", isPublic: true).Get();

        yield return WaitForSolve;

        var answer = GetField<string>(comp, "metaAnswer").Get(x => x.Length != 7 ? "expected length 7" : null);

        addQuestion(module, Question.MetapuzzleAnswer, correctAnswers: new[] { answer }, preferredWrongAnswers: words);
    }
}