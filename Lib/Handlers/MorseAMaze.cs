using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMorseAMaze
{
    [SouvenirQuestion("What was the starting location in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-F", "1-6")]
    StartingCoordinate,
    
    [SouvenirQuestion("What was the ending location in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-F", "1-6")]
    EndingCoordinate,
    
    [SouvenirQuestion("What was the word shown as Morse code in {0}?", ThreeColumns6Answers, ExampleAnswers = ["couch", "strobe", "smoke", "assay", "monkey", "glass", "starts", "strode", "office", "essays", "couple", "bosses"])]
    MorseCodeWord
}

public partial class SouvenirModule
{
    [SouvenirHandler("MorseAMaze", "Morse-A-Maze", typeof(SMorseAMaze), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessMorseAMaze(ModuleData module)
    {
        var comp = GetComponent(module, "MorseAMaze");

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var start = GetField<string>(comp, "_souvenirQuestionStartingLocation").Get(str => str.Length != 2 ? "expected length 2" : null);
        var end = GetField<string>(comp, "_souvenirQuestionEndingLocation").Get(str => str.Length != 2 ? "expected length 2" : null);
        var word = GetField<string>(comp, "_souvenirQuestionWordPlaying").Get(str => str.Length < 4 ? "expected length ≥ 4" : null);
        var words = GetArrayField<string>(comp, "_souvenirQuestionWordList").Get(expectedLength: 36);

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.MorseAMazeStartingCoordinate, module, correctAnswers: new[] { start }),
            makeQuestion(Question.MorseAMazeEndingCoordinate, module, correctAnswers: new[] { end }),
            makeQuestion(Question.MorseAMazeMorseCodeWord, module, correctAnswers: new[] { word }, preferredWrongAnswers: words));
    }
}