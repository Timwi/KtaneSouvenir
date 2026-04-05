using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSuitsAndColours
{
    [Question("What was the colour of this cell in {0}?", TwoColumns4Answers, "yellow", "green", "orange", "red", UsesQuestionSprite = true, TranslateAnswers = true)]
    Colour,

    [Question("What was the suit of this cell in {0}?", TwoColumns4Answers, "spades", "hearts", "clubs", "diamonds", UsesQuestionSprite = true, TranslateAnswers = true)]
    Suit
}

public partial class SouvenirModule
{
    [Handler("GSSuitsAndColours", "Suits and Colours", typeof(SSuitsAndColours), "Hawker")]
    [ManualQuestion("What were the colours and suits of each cell?")]
    private IEnumerator<SouvenirInstruction> ProcessSuitsAndColours(ModuleData module)
    {
        var comp = GetComponent(module, "SuitsAndColoursScript");
        yield return WaitForSolve;

        var suits = new[] { "spades", "hearts", "clubs", "diamonds" };
        var colours = new[] { "red", "orange", "yellow", "green" };
        var correctSuitIndices = GetListField<int>(comp, "ChosenSuits").Get(li => li.Count != 9 ? "expected length 9" : null);
        var correctColourIndices = GetListField<int>(comp, "ChosenColours").Get(li => li.Count != 9 ? "expected length 9" : null);
        for (var i = 0; i < 9; i++)
        {
            var coordinate = new Coord(3, 3, i);
            yield return question(SSuitsAndColours.Colour, questionSprite: Sprites.GenerateGridSprite(coordinate)).Answers(colours[correctColourIndices[i]]);
            yield return question(SSuitsAndColours.Suit, questionSprite: Sprites.GenerateGridSprite(coordinate)).Answers(suits[correctSuitIndices[i]]);
        }
    }
}
