using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWatchingPaintDry
{
    [Question("How many brush strokes were heard in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(3, 8)]
    StrokeCount
}

public partial class SouvenirModule
{
    [Handler("watchingPaintDry", "Watching Paint Dry", typeof(SWatchingPaintDry), "Anonymous")]
    [ManualQuestion("How many brush strokes were there?")]
    private IEnumerator<SouvenirInstruction> ProcessWatchingPaintDry(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "watchingPaintDry");
        var strokes = GetIntField(comp, "strokeCount").Get(min: 3, max: 8);

        yield return question(SWatchingPaintDry.StrokeCount).Answers(strokes.ToString());
    }
}