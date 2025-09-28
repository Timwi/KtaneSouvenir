using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using UnityEngine.UI;
using static Souvenir.AnswerLayout;

public enum STwodoku
{
    [SouvenirQuestion("Which of these squares in {0} was {1}?", ThreeColumns6Answers, Arguments = ["a given digit", "a given shape", "highlighted"], ArgumentGroupSize = 1, TranslateArguments = [true], Type = AnswerType.Sprites)]
    Givens,

    [SouvenirQuestion("What was in this grid position in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, UsesQuestionSprite = true)]
    GridPositions,
}

public partial class SouvenirModule
{
    [SouvenirHandler("TwodokuModule", "Twodoku", typeof(STwodoku), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessTwodoku(ModuleData module)
    {
        var comp = GetComponent(module, "TwodokuModule");

        yield return WaitForSolve;

        var symTemplate = GetField<Image>(comp, "SymbolTemplate", isPublic: true).Get().gameObject.transform.parent;
        var symbolSprites = GetArrayField<Sprite>(comp, "Symbols", isPublic: true).Get(expectedLength: 15).Select(spr => spr.TranslateSprite(1750f)).ToArray();
        var numberSprites = GetArrayField<Sprite>(comp, "Numbers", isPublic: true).Get(expectedLength: 6).Select(spr => spr.TranslateSprite(1750f)).ToArray();
        var highlightSprite = GetField<Sprite>(comp, "HighlightSprite", isPublic: true).Get().TranslateSprite(1750f);

        var shapesOnModule = new List<(int location, int symIx)>();
        var numbersOnModule = new List<(int location, int number)>();
        var highlightsOnModule = new List<int>();

        for (var i = 0; i < symTemplate.childCount; i++)
        {
            var child = symTemplate.GetChild(i).name;
            if (child.RegexMatch(@"^Number-(\d+)-(\d+)$", out var match))
                numbersOnModule.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
            else if (child.RegexMatch(@"^Symbol-(\d+)-(\d+)$", out match))
                shapesOnModule.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
            else if (child.RegexMatch(@"^Highlight-(\d+)$", out match))
                highlightsOnModule.Add(int.Parse(match.Groups[1].Value));
        }

        if (shapesOnModule.Count < 6)
            throw new AbandonModuleException($"The module has only {shapesOnModule.Count} shapes, at least 6 are required.");
        if (numbersOnModule.Count < 5)
            throw new AbandonModuleException($"The module has only {numbersOnModule.Count} numbers, at least 5 are required.");
        if (highlightsOnModule.Count != 6)
            throw new AbandonModuleException($"The module has {highlightsOnModule.Count} highlights, exactly 6 are required.");

        Sprite[] allSprites = [.. shapesOnModule.Select(tup => symbolSprites[tup.symIx]), .. numberSprites, highlightSprite];

        yield return question(STwodoku.Givens, args: ["a given digit"]).Answers(numbersOnModule.Select(tup => new Coord(6, 6, tup.location)).ToArray());
        yield return question(STwodoku.Givens, args: ["a given shape"]).Answers(shapesOnModule.Select(tup => new Coord(6, 6, tup.location)).ToArray());
        yield return question(STwodoku.Givens, args: ["highlighted"]).Answers(highlightsOnModule.Select(cell => new Coord(6, 6, cell)).ToArray());

        foreach (var (location, symIx) in shapesOnModule)
            yield return question(STwodoku.GridPositions, questionSprite: Sprites.GenerateGridSprite(new Coord(6, 6, location)))
                .Answers(symbolSprites[symIx], all: allSprites);
        foreach (var (location, number) in numbersOnModule)
            yield return question(STwodoku.GridPositions, questionSprite: Sprites.GenerateGridSprite(new Coord(6, 6, location)))
                .Answers(numberSprites[number], all: allSprites);
        foreach (var cell in highlightsOnModule)
            yield return question(STwodoku.GridPositions, questionSprite: Sprites.GenerateGridSprite(new Coord(6, 6, cell)))
                .Answers(highlightSprite, all: allSprites);
    }
}
