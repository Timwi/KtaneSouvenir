using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSuitsAndColours
{
    [SouvenirQuestion("What was the colour of this cell in {0}?", TwoColumns4Answers, "yellow", "green", "orange", "red", UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colour,

    [SouvenirQuestion("What was the suit of this cell in {0}?", TwoColumns4Answers, "spades", "diamonds", "clubs", "hearts", UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Suit
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSSuitsAndColours", "Suits and Colours", typeof(SSuitsAndColours), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessSuitsAndColours(ModuleData module)
    {
        var comp = GetComponent(module, "SuitsAndColoursScript");
        yield return WaitForSolve;

        var suits = new[] { "spades", "hearts", "clubs", "diamonds" };
        var colours = new[] { "red", "orange", "yellow", "green" };
        var correctSuitIndices = GetListField<int>(comp, "ChosenSuits").Get(li => li.Count != 9 ? "expected length 9" : null);
        var correctColourIndices = GetListField<int>(comp, "ChosenColours").Get(li => li.Count != 9 ? "expected length 9" : null);

        var questions = new List<QandA>();
        for (var i = 0; i < 9; i++)
        {
            var coordinate = new Coord(3, 3, i);
            questions.Add(makeQuestion(Question.SuitsAndColoursColour, module, questionSprite: Sprites.GenerateGridSprite(coordinate), correctAnswers: new[] { colours[correctColourIndices[i]] }));
            questions.Add(makeQuestion(Question.SuitsAndColoursSuit, module, questionSprite: Sprites.GenerateGridSprite(coordinate), correctAnswers: new[] { colours[correctSuitIndices[i]] }));
        }

        addQuestions(module, questions);
    }
}