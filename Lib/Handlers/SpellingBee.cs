using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSpellingBee
{
    [SouvenirQuestion("What word was asked to be spelled in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "SpellingBeeAudio")]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("spellingBee", "Spelling Bee", typeof(SSpellingBee), "BigCrunch22")]
    [SouvenirManualQuestion("What word was asked to be spelled?")]
    private IEnumerator<SouvenirInstruction> ProcessSpellingBee(ModuleData module)
    {
        var comp = GetComponent(module, "spellingBeeScript");
        var wordList = GetField<List<string>>(comp, "wordList", isPublic: true).Get();

        yield return WaitForSolve;
        var focus = GetField<int>(comp, "chosenWord").Get();
        var str = $"SpellingBee_{wordList[focus]}";
        Debug.Log("<>" + str);

        yield return question(SSpellingBee.Word).Answers(SpellingBeeAudio[focus], preferredWrong: SpellingBeeAudio);
    }
}