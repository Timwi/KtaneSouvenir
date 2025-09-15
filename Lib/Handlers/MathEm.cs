using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMathEm
{
    [SouvenirQuestion("What was the color of this tile before the shuffle on {0}?", TwoColumns4Answers, "White", "Bronze", "Silver", "Gold", TranslateAnswers = true, UsesQuestionSprite = true)]
    Color,

    [SouvenirQuestion("What was the design on this tile before the shuffle on {0}?", ThreeColumns6Answers, UsesQuestionSprite = true, Type = AnswerType.Sprites, SpriteFieldName = "MathEmSprites")]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("mathem", "Math ’em", typeof(SMathEm), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessMathEm(ModuleData module)
    {
        var comp = GetComponent(module, "MathemScript");

        var fldArrangements = GetField<int[][]>(comp, "tarrange");
        var fldProps = GetField<int[,]>(comp, "tprops");
        var fldMats = GetArrayField<Material>(comp, "tpatterns", isPublic: true);

        yield return WaitForSolve;

        var initialArrangement = fldArrangements.Get().First();
        var props = fldProps.Get();

        var qs = new List<QandA>();
        string[] colorNames = { "White", "Bronze", "Silver", "Gold" };
        var displayedMarkings = Enumerable.Range(0, 16).Select(ix => MathEmSprites[(props[initialArrangement[ix], 0] * 10) + props[initialArrangement[ix], 2]]).ToArray();

        for (var tileIx = 0; tileIx < 16; tileIx++)
        {
            qs.Add(makeQuestion(Question.MathEmColor, module,
                questionSprite: Sprites.GenerateGridSprite(new Coord(4, 4, tileIx)),
                correctAnswers: new[] { colorNames[props[initialArrangement[tileIx], 1]] }));
            qs.Add(makeQuestion(Question.MathEmLabel, module,
                questionSprite: Sprites.GenerateGridSprite(new Coord(4, 4, tileIx)),
                correctAnswers: new[] { displayedMarkings[tileIx] },
                preferredWrongAnswers: displayedMarkings));
        }
        addQuestions(module, qs);

    }
}