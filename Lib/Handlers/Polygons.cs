using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPolygons
{
    [SouvenirQuestion("Which polygon was present on {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "PolygonsSprites")]
    Polygon
}

public partial class SouvenirModule
{
    [SouvenirHandler("polygons", "Polygons", typeof(SPolygons), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessPolygons(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "polygons");

        var incompatibleGroups = Ut.NewArray(
            new[] { 26, 30 }, // short trapezoid, tall trapezoid
            new[] { 16, 29 }, // long rectangle, tall rectangle
            new[] { 3, 12, 21, 31 }, // arrows (dlru)
            new[] { 4, 13, 22, 32 }, // arrowheads (dlru)
            new[] { 5, 14, 23, 33 }, // crosses (dlru)
            new[] { 6, 15, 24, 34 } // kites (dlru)
        );

        // [0, 34] except incompatible
        var compatibleShapes = new[] { 0, 1, 2, 7, 8, 9, 10, 11, 17, 18, 19, 20, 25, 27, 28 };

        var correctShape = GetIntField(comp, "trueshape").Get(0, 34);
        var incorrectShapes = GetArrayField<int>(comp, "falseshapes").Get(3, 3, validator: v => v is < 0 or > 34 ? "Out of range 0-34" : null);
        var allShapes = new[] { correctShape, incorrectShapes[0], incorrectShapes[1], incorrectShapes[2] };

        // We remove questionably ambiguous answers, but add one back chosen randomly from each group.
        // If a group has at least one correct answer, don't add another, but multiple correct answers can't show up anyways.
        var use = incompatibleGroups
            .Where(g => !g.Any(v => allShapes.Contains(v)))
            .Select(a => a.PickRandom())
            .ToArray();

        var valid = compatibleShapes
            .Concat(use)
            .Concat(allShapes)
            .Select(v => PolygonsSprites[v])
            .ToArray();
        var correct = allShapes.Select(v => PolygonsSprites[v]).ToArray();

        addQuestion(module, Question.PolygonsPolygon, correctAnswers: correct, allAnswers: valid);
    }
}