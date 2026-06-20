using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SScavengerHunt
{
    [Question("Which of these tiles gave a relevant clue in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, AnswerType = InfoType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    ClueTiles
}

public partial class SouvenirModule
{
    [Handler("scavengerHunt", "Scavenger Hunt", typeof(SScavengerHunt), "Timwi")]
    [ManualQuestion("Which tiles gave relevant clues in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessScavengerHunt(ModuleData module)
    {
        var comp = GetComponent(module, "scavengerHunt");

        // Coordinates of the color that the user needed
        var relTiles = GetArrayField<int>(comp, "relTiles").Get(expectedLength: 2, validator: v => v is < 0 or > 15 ? "expected range 0–15" : null);

        // Coordinates of the other colors
        var decoyTiles = GetArrayField<int>(comp, "decoyTiles").Get(expectedLength: 4, validator: v => v is < 0 or > 15 ? "expected range 0–15" : null);

        yield return WaitForSolve;

        yield return question(SScavengerHunt.ClueTiles, args: [Ordinal(1)]).Answers(relTiles.Select(c => new Coord(4, 4, c)).ToArray());
        yield return question(SScavengerHunt.ClueTiles, args: [Ordinal(2)]).Answers(decoyTiles.Select(c => new Coord(4, 4, c)).ToArray());
    }
}
