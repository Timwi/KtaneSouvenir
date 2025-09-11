using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSubdivides
{
    [SouvenirQuestion("What color was the button at this position in {0}?", TwoColumns4Answers, ["Blue", "Green", "Red", "Violet"], TranslateAnswers = true, UsesQuestionSprite = true)]
    Button
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSubdivides", "Simon Subdivides", typeof(SSimonSubdivides), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSubdivides(ModuleData module)
    {
        var comp = GetComponent(module, "SSubScript");
        yield return WaitForSolve;
        //URDL
        //RBVG
        var split = GetArrayField<bool[]>(comp, "split").Get(arr => arr.Length != 5 ? "Wrong outer array size" : arr.All(a => a.Length == 4) ? null : "Wrong inner array size");
        var arrange = GetField<int[,]>(comp, "arrange").Get(arr => arr.Length == 84 ? null : "Bad arrange size");
        var qs = new List<QandA>(12);
        var dirs = new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 1) };
        var colors = new[] { "Red", "Blue", "Violet", "Green" };
        for (var a = 0; a < 4; a++)
        {
            if (split[0][a])
            {
                qs.Add(makeQuestion(Question.SimonSubdividesButton, module, questionSprite: Sprites.GenerateGridSprite(new Coord(2, 2, dirs[a].x, dirs[a].y)), correctAnswers: new[] { colors[arrange[0, a]] }, questionSpriteRotation: 45f));
                for (var b = 0; b < 4; b++)
                    if (split[a + 1][b])
                        qs.Add(makeQuestion(Question.SimonSubdividesButton, module, questionSprite: Sprites.GenerateGridSprite(new Coord(4, 4, dirs[a].x * 2 + dirs[b].x, dirs[a].y * 2 + dirs[b].y)), correctAnswers: new[] { colors[arrange[a + 1, b]] }, questionSpriteRotation: 45f));
            }
        }

        addQuestions(module, qs);
    }
}