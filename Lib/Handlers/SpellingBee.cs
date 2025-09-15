using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSpellingBee
{
    [SouvenirQuestion("What word was asked to be spelled in {0}?", TwoColumns4Answers, ExampleAnswers = ["allocation", "auxiliary", "cloying", "connoisseur", "controversial", "deceit", "garrulous", "malachite", "perambulate", "sedge"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("spellingBee", "Spelling Bee", typeof(SSpellingBee), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessSpellingBee(ModuleData module)
    {
        var comp = GetComponent(module, "spellingBeeScript");
        var wordList = GetField<List<string>>(comp, "wordList", isPublic: true).Get();

        yield return WaitForSolve;
        var focus = GetField<int>(comp, "chosenWord").Get();
        yield return question(SSpellingBee.Word).Answers(wordList[focus], preferredWrong: wordList.ToArray());
    }
}