using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSpellingBee
{
    [Question("What word was asked to be spelled in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Audio, ForeignAudioID = "spellingBee")]
    Word
}

public partial class SouvenirModule
{
    [Handler("spellingBee", "Spelling Bee", typeof(SSpellingBee), "Quinn Wuest")]
    [ManualQuestion("What word was asked to be spelled?")]
    private IEnumerator<SouvenirInstruction> ProcessSpellingBee(ModuleData module)
    {
        var comp = GetComponent(module, "spellingBeeScript");
        var wordList = GetField<List<string>>(comp, "wordList", isPublic: true).Get();
        var audioClips = wordList.Select(word => Sounds.GetForeignClip("spellingBee", word)).ToArray();

        yield return WaitForSolve;
        var word = GetField<int>(comp, "chosenWord").Get();

        yield return question(SSpellingBee.Word).Answers(audioClips[word], all: audioClips);
    }
}
