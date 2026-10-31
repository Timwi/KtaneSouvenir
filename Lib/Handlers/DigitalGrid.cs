using System.Collections.Generic;
using System.Linq;
using Souvenir;

public enum SDigitalGrid
{
    [Question("What digit was on this square in {0}?", AnswerLayout.ThreeColumns6Answers, QuestionExtraType = InfoType.Sprites)]
    [AnswerGenerator.Integers(0, 9)]
    QDigit,

    [Question("What color was this square in {0}?", AnswerLayout.TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Yellow", "Magenta", "White", QuestionExtraType = InfoType.Sprites, TranslateAnswers = true)]
    QColor,

    [Question("Which square was {1} and had {2} on it in {0}?", AnswerLayout.ThreeColumns6Answers, AnswerType = InfoType.Sprites, Arguments = ["red", "a 0", "green", "a 1", "blue", "a 2", "cyan", "a 3", "yellow", "a 4", "magenta", "a 5", "white", "a 6", "white", "a 7", "white", "an 8", "white", "a 9"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    QSpecific,

    [Discriminator("the Digital Grid where this square had {0} on it", Arguments = ["a 0", "a 1", "a 2", "a 3", "a 4", "a 5", "a 6", "a 7", "an 8", "a 9"], ArgumentGroupSize = 1, QuestionExtraType = InfoType.Sprites, TranslateArguments = [true])]
    DDigit,

    [Discriminator("the Digital Grid where this square was {0}", Arguments = ["red", "green", "blue", "cyan", "yellow", "magenta", "white"], ArgumentGroupSize = 1, TranslateArguments = [true], QuestionExtraType = InfoType.Sprites)]
    DColor
}

public partial class SouvenirModule
{
    [Handler("digitalGrid", "Digital Grid", typeof(SDigitalGrid), "Timwi")]
    [ManualQuestion("What were the colors and digits in each square?")]
    private IEnumerator<SouvenirInstruction> ProcessDigitalGrid(ModuleData module)
    {
        var comp = GetComponent(module, "DigitalGridHandler");
        yield return WaitForSolve;

        var colors = GetArrayField<int>(comp, "idxColors").Get(expectedLength: 25, validator: v => v is < 0 or > 5 ? "expected range 0–5" : null);
        var digits = GetArrayField<int>(comp, "digitsGrid").Get(expectedLength: 25, validator: v => v is < 0 or > 9 ? "expected range 0–9" : null);

        var colorNames = new[] { "Red", "Green", "Blue", "Cyan", "Yellow", "Magenta", "White" };
        var digitNames = new[] { "a 0", "a 1", "a 2", "a 3", "a 4", "a 5", "a 6", "a 7", "an 8", "a 9" };
        for (var sq = 0; sq < 25; sq++)
        {
            yield return question(SDigitalGrid.QColor, questionExtra: Sprites.GenerateGridSprite(5, 5, sq)).AvoidDiscriminators(SDigitalGrid.DColor).Answers(colorNames[colors[sq]]);
            yield return new Discriminator(SDigitalGrid.DColor, $"col-{sq}", colors[sq], args: [colorNames[colors[sq]].ToLowerInvariant()], questionExtra: Sprites.GenerateGridSprite(5, 5, sq));
            yield return question(SDigitalGrid.QDigit, questionExtra: Sprites.GenerateGridSprite(5, 5, sq)).AvoidDiscriminators(SDigitalGrid.DDigit).Answers(digits[sq].ToString());
            yield return new Discriminator(SDigitalGrid.DDigit, $"dgt-{sq}", colors[sq], args: [digitNames[digits[sq]]], questionExtra: Sprites.GenerateGridSprite(5, 5, sq));
        }
        for (var color = 0; color < 7; color++)
            for (var digit = 0; digit <= 9; digit++)
            {
                var candidates = Enumerable.Range(0, 25).Where(sq => colors[sq] == color && digits[sq] == digit).ToArray();
                if (candidates.Length > 0)
                    yield return question(SDigitalGrid.QSpecific, args: [colorNames[color].ToLowerInvariant(), digitNames[digit]])
                        .AvoidDiscriminators(candidates.SelectMany(sq => new[] { $"col-{sq}", $"dgt-{sq}" }))
                        .Answers(candidates.Select(sq => new Coord(5, 5, sq)).ToArray());
            }
    }
}
