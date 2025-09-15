using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPurpleArrows
{
    [SouvenirQuestion("What was the target word on {0}?", ThreeColumns6Answers, ExampleAnswers = ["Thesis", "Immune", "Agency", "Height", "Active", "Bother", "Viable"])]
    Finish
}

public partial class SouvenirModule
{
    [SouvenirHandler("purpleArrowsModule", "Purple Arrows", typeof(SPurpleArrows), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessPurpleArrows(ModuleData module)
    {
        var comp = GetComponent(module, "PurpleArrowsScript");

        yield return WaitForSolve;

        var finishWord = GetField<string>(comp, "finish").Get(str => str.Length != 6 ? "expected length 6" : null);
        var wordList = GetArrayField<string>(comp, "words").Get(v => v.Length == 0 ? "wordlist is empty" : null);

        if (!wordList.Contains(finishWord))
            throw new AbandonModuleException($"‘words’ does not contain ‘finish’: [Length: {wordList.Length}, finishWord: {finishWord}].");

        var wordScreen = GetField<GameObject>(comp, "wordDisplay", isPublic: true).Get();
        var wordScreenTextMesh = wordScreen.GetComponent<TextMesh>() ?? throw new AbandonModuleException("‘wordDisplay’ does not have a TextMesh component.");
        wordScreenTextMesh.text = "SOLVED";

        yield return question(SPurpleArrows.Finish).Answers(Regex.Replace(finishWord, @"(?<!^).", m => m.Value.ToLowerInvariant()), preferredWrong: wordList.Select(w => w[0] + w.Substring(1).ToLowerInvariant()).ToArray());
    }
}