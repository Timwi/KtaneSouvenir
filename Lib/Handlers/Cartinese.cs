using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCartinese
{
    [SouvenirQuestion("What color was the {1} button in {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", Arguments = ["up", "right", "down", "left"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateArguments = [true])]
    ButtonColors,

    [SouvenirQuestion("What lyric was played by the {1} button in {0}?", TwoColumns4Answers, "Aingobodirou", "Dongifubounan", "Ayofumylu", "Dimycamilayw", "Dogosemiu", "Bitgosemiu", "Iwittyluyu", "Herolideca", "Anseweke", "Likwoveke", "Omeygah", "Dediamnatifney", Arguments = ["up", "right", "down", "left"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Lyrics
}

public partial class SouvenirModule
{
    [SouvenirHandler("cartinese", "Cartinese", typeof(SCartinese), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCartinese(ModuleData module)
    {
        var comp = GetComponent(module, "cartinese");
        yield return WaitForSolve;

        var buttonColors = GetArrayField<int>(comp, "buttonColors").Get(expectedLength: 4);
        var buttonLyrics = GetArrayField<string>(comp, "buttonLyrics").Get(expectedLength: 4);

        var buttonNames = new[] { "up", "right", "down", "left" };

        addQuestions(module,
            Enumerable.Range(0, 4).Select(btn => makeQuestion(SCartinese.ButtonColors, module, formatArgs: new[] { buttonNames[btn] }, correctAnswers: new[] { SCartinese.ButtonColors.GetAnswers()[buttonColors[btn]] }))
            .Concat(Enumerable.Range(0, 4).Select(btn => makeQuestion(SCartinese.Lyrics, module, formatArgs: new[] { buttonNames[btn] }, correctAnswers: new[] { buttonLyrics[btn] }))));
    }
}