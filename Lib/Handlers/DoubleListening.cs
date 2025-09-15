using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDoubleListening
{
    [SouvenirQuestion("What clip was played in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "ListeningAudio")]
    Sounds
}

public partial class SouvenirModule
{
    [SouvenirHandler("doubleListening", "Double Listening", typeof(SDoubleListening), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleListening(ModuleData module)
    {
        var comp = GetComponent(module, "doubleListeningScript");
        yield return WaitForSolve;

        // Sounds could be gotten directly from the module, however,
        // they can't be for Listening so there's no point.

        var indices = new[] {
            0,  2,  3,  4,  5,  //"Arcade","Beach","Book Page Turning","Car Engine","Casino",
            6,  7,  8,  9,  10, //"Censorship Bleep","Chainsaw","Compressed Air","Cow","Dialup Internet",
            11, 12, 13, 14, 15, //"Door Closing","Extractor Fan","Firework Exploding","Glass Shattering","Helicopter",
            16, 17, 18, 19, 20, //"Marimba","Medieval Weapons","Oboe","Phone Ringing","Police Radio Scanner",
            21, 22, 23, 24, 25, //"Rattling Iron Chain","Reloading Glock 19","Saxophone","Servo Motor","Sewing Machine",
            26, 27, 28, 29, 30, //"Soccer Match","Squeaky Toy","Supermarket","Table Tennis","Tawny Owl",
            31, 33, 34, 35, 36, //"Taxi Dispatch","Throat Singing","Thrush Nightingale","Tibetan Nuns","Train Station",
            37, 38, 39          //"Tuba","Vacuum Cleaner","Waterfall"
        };

        var used = GetArrayField<int>(comp, "soundPositions").Get(expectedLength: 2, validator: i => i < 0 || i >= indices.Length ? $"Index {i} out of range [0,{indices.Length})" : null);
        addQuestion(module, Question.DoubleListeningSounds,
            correctAnswers: used.Select(i => ListeningAudio[indices[i]]).ToArray(),
            allAnswers: indices.Select(i => ListeningAudio[i]).ToArray()
        );
    }
}