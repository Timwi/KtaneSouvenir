using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCRule
{
    [SouvenirQuestion("Which symbol pair was here in {0}?", ThreeColumns6Answers, "♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡", UsesQuestionSprite = true)]
    SymbolPair,

    [SouvenirQuestion("Where was {1} in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = ["♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡"], ArgumentGroupSize = 1)]
    SymbolPairCell,

    [SouvenirQuestion("Which symbol pair was present on {0}?", ThreeColumns6Answers, "♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡")]
    SymbolPairPresent,

    [SouvenirQuestion("Which cell was pre-filled at the start of {0}?", TwoColumns4Answers, Type = AnswerType.Sprites)]
    Prefilled
}

public partial class SouvenirModule
{
    [SouvenirHandler("the_cRule", "cRule", typeof(SCRule), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessCRule(ModuleData module)
    {
        var comp = GetComponent(module, "TheCRuleScript");

        var symbolTextMeshes = GetArrayField<TextMesh>(comp, "symbols", isPublic: true).Get(expectedLength: 26);
        var symbols = symbolTextMeshes.Select((tm, ix) => (symPair: tm.text, cell: ix)).Where(tup => !string.IsNullOrEmpty(tup.symPair)).ToArray();

        yield return WaitForSolve;

        // This contains the indexes of the pre-filled squares, but counting from 1
        var initOn = GetArrayField<int>(comp, "initOn").Get(expectedLength: 10);

        var cells = Enumerable.Range(0, 8).Select(x => (x: 4 * x, y: 0))
            .Concat(Enumerable.Range(0, 7).Select(x => (x: 4 * x + 2, y: 4)))
            .Concat(Enumerable.Range(0, 6).Select(x => (x: 4 * x + 4, y: 8)))
            .Concat(Enumerable.Range(0, 5).Select(x => (x: 4 * x + 6, y: 12)))
            .ToArray();
        var cellSprites = Enumerable.Range(0, 26).Select(cell => Sprites.GenerateGridSprite("cRule", 4 * 8 + 1, 4 * 4 + 1, cells, cell, $"cRule row {cells[cell].y / 4 + 1} cell {(cells[cell].x - cells[cell].y / 2) / 4 + 1}", 80)).ToArray();

        var displayedSymbols = symbols.Select(tup => tup.symPair).ToArray();
        var displayedCells = symbols.Select(tup => cellSprites[tup.cell]).ToArray();
        foreach (var (symPair, cell) in symbols)
        {
            // "Which symbol pair was here in {0}?"
            yield return question(SCRule.SymbolPair, questionSprite: cellSprites[cell]).Answers(symPair, preferredWrong: displayedSymbols);

            // "Where was {1} in {0}?"
            yield return question(SCRule.SymbolPairCell, args: [symPair]).Answers(cellSprites[cell], all: cellSprites, preferredWrong: displayedCells);
        }

        // "Which symbol pair was present on {0}?"
        yield return question(SCRule.SymbolPairPresent).Answers(displayedSymbols);

        // "Which cell was pre-filled at the start of {0}?"
        yield return question(SCRule.Prefilled).Answers(initOn.Select(cellOffBy1 => cellSprites[cellOffBy1 - 1]).ToArray(), all: cellSprites);
    }
}