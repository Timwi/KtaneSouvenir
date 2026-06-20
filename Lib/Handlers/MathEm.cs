using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMathEm
{
    [Question("What was the color of this tile before the shuffle on {0}?", TwoColumns4Answers, "White", "Bronze", "Silver", "Gold", TranslateAnswers = true, QuestionExtraType = InfoType.Sprites)]
    Color,

    [Question("What was the design on this tile before the shuffle on {0}?", ThreeColumns6Answers, QuestionExtraType = InfoType.Sprites, AnswerType = InfoType.Sprites)]
    Label
}

public partial class SouvenirModule
{
    [Handler("mathem", "Math ’em", typeof(SMathEm), "tandyCake")]
    [ManualQuestion("What were the color and design of each tile before the shuffle?")]
    private IEnumerator<SouvenirInstruction> ProcessMathEm(ModuleData module)
    {
        var comp = GetComponent(module, "MathemScript");

        var fldArrangements = GetField<int[][]>(comp, "tarrange");
        var fldProps = GetField<int[,]>(comp, "tprops");
        var sprites = GetArrayField<Material>(comp, "tpatterns", isPublic: true).Get(expectedLength: 24)
            .Select(mat => ((Texture2D) mat.mainTexture).ToSprite())
            .ToArray();

        yield return WaitForSolve;

        var initialArrangement = fldArrangements.Get().First();
        var props = fldProps.Get();
        var colorNames = new[] { "White", "Bronze", "Silver", "Gold" };
        var displayedMarkings = Enumerable.Range(0, 16).Select(ix => sprites[(props[initialArrangement[ix], 0] * 10) + props[initialArrangement[ix], 2]]).ToArray();

        for (var tileIx = 0; tileIx < 16; tileIx++)
        {
            yield return question(SMathEm.Color, questionExtra: Sprites.GenerateGridSprite(new Coord(4, 4, tileIx)))
                .Answers(colorNames[props[initialArrangement[tileIx], 1]]);
            yield return question(SMathEm.Label, questionExtra: Sprites.GenerateGridSprite(new Coord(4, 4, tileIx)))
                .Answers(displayedMarkings[tileIx], preferredWrong: displayedMarkings, all: sprites, xStretch: 1.5f);
        }

    }
}
