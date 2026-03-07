using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STrickyTetrisPieces
{
    [SouvenirQuestion("What was the shape of the first piece you pressed in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Polyominoes(minCellCount: 4, excludeEqualRotations: true)]
    FirstShape,

    [SouvenirQuestion("What was the second color palette the pieces converted to in {0}?", OneColumn4Answers, "Internship at Marie Curie's Lab", "Mexico according to Hollywood", "Christmas but Green with Envy", "Blueberry Factory After Hours")]
    SecondPalette
}

public partial class SouvenirModule
{
    [SouvenirHandler("TrickyTetrisPieces", "Tricky Tetris Pieces", typeof(STrickyTetrisPieces), "Espik")]
    private IEnumerator<SouvenirInstruction> ProcessTrickyTetrisPieces(ModuleData module)
    {
        var comp = GetComponent(module, "TrickyTetrisPieces");

        var allPalettes = new[] { "Level 0", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9",
            "Watermelon", "Neon Night", "Glowing Spaghetti", "Lychee", "Lychee Milk Tea", "Red Velvet Cake", "Dusk", "Bubble Gum", "Charcoal", "Week-Old Bubble Gum",
            "Salmon", "Australian Outback", "Stardew Valley Sunset", "Pastel", "Squid", "Lime Factory", "Pomegranate", "Radium", "Nuclear Christmas", "Glass Blowing Studio",
            "Regular Christmas", "Saloon", "Lava Pools", "Sewage", "Lime Factory After Hours", "Blackpink", "Non-Glowing Spaghetti", "Willy Wonka", "Greeeeeeen", "Cherry Limeade",
            "Arctic Forest", "Peach Smoothie", "Los Angeles Smog", "Internship at Marie Curie's Lab", "Burnt Spaghetti", "Burnt Key Lime Pie", "Aquamarine", "Vaporwave",
            "Sour Patch Watermelon", "Mexico according to Hollywood", "Christmas but Green with Envy", "Prairie", "Sea Kelp", "Mossy Cobblestone", "Blueberry Factory After Hours",
            "50 Shades of Blue", "Quarantine Hair Dye", "RED" };

        yield return WaitForSolve;

        var firstShape = GetField<char>(comp, "firstPieceShape").Get();
        var secondPalette = GetIntField(comp, "secondPalette").Get();

        yield return question(STrickyTetrisPieces.FirstShape).Answers([Sprites.GeneratePolyominoSprite(firstShape.ToString())]);
        yield return question(STrickyTetrisPieces.SecondPalette).Answers(allPalettes[secondPalette], all: allPalettes);
    }
}
