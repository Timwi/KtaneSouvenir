using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SScavengerHunt
{
    [SouvenirQuestion("Which tile was correctly submitted in the first stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    KeySquare,

    [SouvenirQuestion("Which of these tiles was {1} in the first stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    ColoredTiles
}

public partial class SouvenirModule
{
    [SouvenirHandler("scavengerHunt", "Scavenger Hunt", typeof(SScavengerHunt), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessScavengerHunt(ModuleData module)
    {
        var comp = GetComponent(module, "scavengerHunt");
        var keySquare = GetIntField(comp, "keySquare").Get(min: 0, max: 15);

        // Coordinates of the color that the user needed
        var relTiles = GetArrayField<int>(comp, "relTiles").Get(expectedLength: 2, validator: v => v is < 0 or > 15 ? "expected range 0–15" : null);

        // Coordinates of the other colors
        var decoyTiles = GetArrayField<int>(comp, "decoyTiles").Get(expectedLength: 4, validator: v => v is < 0 or > 15 ? "expected range 0–15" : null);

        // Which color is the ‘relTiles’ color
        var colorIndex = GetIntField(comp, "colorIndex").Get(min: 0, max: 2);

        // 0 = red, 1 = green, 2 = blue
        var redTiles = colorIndex == 0 ? relTiles : decoyTiles.Take(2).ToArray();
        var greenTiles = colorIndex == 1 ? relTiles : colorIndex == 0 ? decoyTiles.Take(2).ToArray() : decoyTiles.Skip(2).ToArray();
        var blueTiles = colorIndex == 2 ? relTiles : decoyTiles.Skip(2).ToArray();

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.ScavengerHuntKeySquare, module, correctAnswers: new[] { new Coord(4, 4, keySquare) }),
            makeQuestion(Question.ScavengerHuntColoredTiles, module, formatArgs: new[] { "red" }, correctAnswers: redTiles.Select(c => new Coord(4, 4, c)).ToArray()),
            makeQuestion(Question.ScavengerHuntColoredTiles, module, formatArgs: new[] { "green" }, correctAnswers: greenTiles.Select(c => new Coord(4, 4, c)).ToArray()),
            makeQuestion(Question.ScavengerHuntColoredTiles, module, formatArgs: new[] { "blue" }, correctAnswers: blueTiles.Select(c => new Coord(4, 4, c)).ToArray()));
    }
}