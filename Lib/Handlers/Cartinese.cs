using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCartinese
{
    [Question("What color was the {1} button in {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", Arguments = ["up", "right", "down", "left"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateArguments = [true])]
    ButtonColors,

    [Question("What lyric was played by the {1} button in {0}?", ThreeColumns6Answers, ForeignAudioID = "cartinese", AnswerType = InfoType.Audio, Arguments = ["up", "right", "down", "left"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Lyrics
}

public partial class SouvenirModule
{
    [Handler("cartinese", "Cartinese", typeof(SCartinese), "Timwi")]
    [ManualQuestion("What lyrics were played by each button?")]
    [ManualQuestion("What color was each button?")]
    private IEnumerator<SouvenirInstruction> ProcessCartinese(ModuleData module)
    {
        var comp = GetComponent(module, "cartinese");
        yield return WaitForSolve;

        var buttonColors = GetArrayField<int>(comp, "buttonColors").Get(expectedLength: 4);
        var buttonLyrics = GetArrayField<string>(comp, "buttonLyrics").Get(expectedLength: 4);
        
        var allLyrics = GetStaticField<string[]>(comp.GetType(), "lyrics").Get(v => v.Length != 12 ? "expected length 12" : null);
        var allSounds = allLyrics.Select(lyric => Sounds.GetForeignClip("cartinese", lyric)).ToArray();

        var buttonNames = new[] { "up", "right", "down", "left" };

        for (var btn = 0; btn < 4; btn++)
        {
            yield return question(SCartinese.ButtonColors, args: [buttonNames[btn]]).Answers(SCartinese.ButtonColors.GetAnswers()[buttonColors[btn]]);
            yield return question(SCartinese.Lyrics, args: [buttonNames[btn]]).Answers(allSounds.First(x => x.name == buttonLyrics[btn]), all: allSounds);
        }
    }
}
