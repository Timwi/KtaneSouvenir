using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCRule
{
    [Question("Which cell was pre-filled at the start of {0}?", TwoColumns4Answers, AnswerType = InfoType.Sprites)]
    Prefilled
}

public partial class SouvenirModule
{
    [Handler("the_cRule", "cRule", typeof(SCRule), "Timwi", AddThe = true)]
    [ManualQuestion("Which cells were prefilled at the start?")]
    private IEnumerator<SouvenirInstruction> ProcessCRule(ModuleData module)
    {
        var comp = GetComponent(module, "TheCRuleScript");

        yield return WaitForSolve;

        var rowButtons = GetArrayField<KMSelectable>(comp, "rowButtons", isPublic: true).Get();
        var colorButtons = GetArrayField<KMSelectable>(comp, "colorButtons", isPublic: true).Get();
        var submitButton = GetField<KMSelectable>(comp, "buttonS", isPublic: true).Get();
        var resetButton = GetField<KMSelectable>(comp, "buttonR", isPublic: true).Get();

        foreach (var b in rowButtons)
            b.OnInteract = delegate () { return false; };
        foreach (var b in colorButtons)
            b.OnInteract = delegate () { return false; };
        submitButton.OnInteract = delegate () { submitButton.AddInteractionPunch(); return false; };
        resetButton.OnInteract = delegate () { submitButton.AddInteractionPunch(); return false; };

        // This contains the indexes of the pre-filled squares, but counting from 1
        var initOn = GetArrayField<int>(comp, "initOn").Get(expectedLength: 10);

        var cells = Enumerable.Range(0, 8).Select(x => (x: 4 * x, y: 0))
            .Concat(Enumerable.Range(0, 7).Select(x => (x: 4 * x + 2, y: 4)))
            .Concat(Enumerable.Range(0, 6).Select(x => (x: 4 * x + 4, y: 8)))
            .Concat(Enumerable.Range(0, 5).Select(x => (x: 4 * x + 6, y: 12)))
            .ToArray();
        var cellSprites = Enumerable.Range(0, 26).Select(cell => Sprites.GenerateGridSprite("cRule", 4 * 8 + 1, 4 * 4 + 1, cells, cell, $"cRule row {cells[cell].y / 4 + 1} cell {(cells[cell].x - cells[cell].y / 2) / 4 + 1}", 80)).ToArray();

        // "Which cell was pre-filled at the start of {0}?"
        yield return question(SCRule.Prefilled).Answers(initOn.Select(cellOffBy1 => cellSprites[cellOffBy1 - 1]).ToArray(), all: cellSprites);
    }
}
