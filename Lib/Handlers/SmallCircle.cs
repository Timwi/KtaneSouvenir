using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSmallCircle
{
    [Question("How much did the sequence shift by in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8")]
    Shift,

    [Question("Which wedge made the different noise in the beginning of {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black", TranslateAnswers = true)]
    Wedge
}

public partial class SouvenirModule
{
    [Handler("smallCircle", "Small Circle", typeof(SSmallCircle), "TasThiluna")]
    [ManualQuestion("How much did the sequence shift by?")]
    [ManualQuestion("Which wedge made the different noise in the beginning?")]
    private IEnumerator<SouvenirInstruction> ProcessSmallCircle(ModuleData module)
    {
        var comp = GetComponent(module, "smallCircle");
        yield return WaitForSolve;

        var shift = GetField<int>(comp, "shift").Get();
        var tableColor = GetField<int>(comp, "tableColor").Get();
        var colorNames = GetStaticField<string[]>(comp.GetType(), "colorNames").Get().Select(x => x[0].ToString().ToUpperInvariant() + x.Substring(1)).ToArray();
        yield return question(SSmallCircle.Shift).Answers(shift.ToString());
        yield return question(SSmallCircle.Wedge).Answers(colorNames[tableColor]);
    }
}
