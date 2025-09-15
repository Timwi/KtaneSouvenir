using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SClockCounter
{
    [SouvenirQuestion("Which arrow was shown in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "ClockCounterSprites")]
    Arrows
}

public partial class SouvenirModule
{
    [SouvenirHandler("clockCounter", "↻↺", typeof(SClockCounter), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessClockCounter(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "ClockCounter");
        var ans = GetArrayField<int>(comp, "answer").Get(10, false, false, i => i is > 26 or < 1 ? "Out of range 1-26" : null);
        addQuestion(module, Question.ClockCounterArrows, correctAnswers: ans.Select(i => ClockCounterSprites[i - 1]).ToArray());
    }
}