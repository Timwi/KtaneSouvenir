using System.Collections.Generic;
using Souvenir;

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

        yield return question(SMetapuzzle.Answer).Answers(answer, preferredWrong: words);
    }
}