using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMorseAMaze
{
    [Question("What was the starting location in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-F", "1-6")]
    StartingCoordinate,

    [Question("What was the ending location in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-F", "1-6")]
    EndingCoordinate,

    [Question("What was the word shown as Morse code in {0}?", ThreeColumns6Answers, ExampleAnswers = ["couch", "strobe", "smoke", "assay", "monkey", "glass", "starts", "strode", "office", "essays", "couple", "bosses"])]
    MorseCodeWord
}

public partial class SouvenirModule
{
    [Handler("MorseAMaze", "Morse-A-Maze", typeof(SMorseAMaze), "CaitSith2")]
    [ManualQuestion("What were the starting and ending locations?")]
    [ManualQuestion("What was the Morse code word played?")]
    private IEnumerator<SouvenirInstruction> ProcessMorseAMaze(ModuleData module)
    {
        var comp = GetComponent(module, "MorseAMaze");

        yield return WaitForActivate;

        var start = GetField<string>(comp, "_souvenirQuestionStartingLocation").Get(str => str.Length != 2 ? "expected length 2" : null);
        var end = GetField<string>(comp, "_souvenirQuestionEndingLocation").Get(str => str.Length != 2 ? "expected length 2" : null);
        var word = GetField<string>(comp, "_souvenirQuestionWordPlaying").Get(str => str.Length < 4 ? "expected length ≥ 4" : null);
        var words = GetArrayField<string>(comp, "_souvenirQuestionWordList").Get(expectedLength: 36);

        yield return WaitForSolve;
        yield return question(SMorseAMaze.StartingCoordinate).Answers(start);
        yield return question(SMorseAMaze.EndingCoordinate).Answers(end);
        yield return question(SMorseAMaze.MorseCodeWord).Answers(word, preferredWrong: words);
    }
}