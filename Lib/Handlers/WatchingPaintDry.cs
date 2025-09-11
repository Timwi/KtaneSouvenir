using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWatchingPaintDry
{
    [SouvenirQuestion("How many brush strokes were heard in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(3, 8)]
    StrokeCount
}

public partial class SouvenirModule
{
    [SouvenirHandler("watchingPaintDry", "Watching Paint Dry", typeof(SWatchingPaintDry), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessWatchingPaintDry(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "watchingPaintDry");
        var strokes = GetIntField(comp, "strokeCount").Get(min: 3, max: 8);

        addQuestion(module, Question.WatchingPaintDryStrokeCount, correctAnswers: new[] { strokes.ToString() });
    }
}