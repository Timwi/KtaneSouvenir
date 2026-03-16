using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGridlock
{
    [SouvenirQuestion("What was the starting color in {0}?", TwoColumns4Answers, "Green", "Yellow", "Red", "Blue", TranslateAnswers = true)]
    StartingColor,

    [SouvenirQuestion("What was the starting location in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    StartingLocation
}

public partial class SouvenirModule
{
    [SouvenirHandler("GridlockModule", "Gridlock", typeof(SGridlock), "CaitSith2")]
    [SouvenirManualQuestion("What was the starting color?")]
    [SouvenirManualQuestion("What was the starting position?")]
    private IEnumerator<SouvenirInstruction> ProcessGridlock(ModuleData module)
    {
        var comp = GetComponent(module, "GridlockModule");

        var colors = SGridlock.StartingColor.GetAnswers();

        yield return WaitForActivate;

        var pages = GetArrayField<int[]>(comp, "_pages").Get(minLength: 5, maxLength: 10, validator: p => p.Length != 16 ? "expected length 16" : p.Any(q => q < 0 || (q & 15) > 12 || (q & (15 << 4)) > (4 << 4)) ? "unexpected value" : null);
        var start = pages[0].IndexOf(i => (i & 15) == 4);

        yield return WaitForSolve;
        yield return question(SGridlock.StartingLocation).Answers(new Coord(4, 4, start));
        yield return question(SGridlock.StartingColor).Answers(colors[(pages[0][start] >> 4) - 1]);
    }
}
