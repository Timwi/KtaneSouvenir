using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum S3DTapCode
{
    [SouvenirQuestion("What was the received word in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Aback", "Backs", "Habit", "Oasis", "Unzip", "Vogue"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("3DTapCodeModule", "3D Tap Code", typeof(S3DTapCode), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> Process3DTapCode(ModuleData module)
    {
        var comp = GetComponent(module, "ThreeDTapCodeScript");
        yield return WaitForSolve;

        var uncapitalizedWord = GetField<string>(comp, "_chosenWord").Get();
        var word = uncapitalizedWord[0] + uncapitalizedWord.Substring(1).ToLowerInvariant();
        var allWords = GetArrayField<string>(comp, "_chosenWordList").Get(expectedLength: 125).Select(x => x[0] + x.Substring(1).ToLowerInvariant()).ToArray();
        addQuestion(module, Question._3DTapCodeWord, correctAnswers: new[] { word }, preferredWrongAnswers: allWords);
    }
}