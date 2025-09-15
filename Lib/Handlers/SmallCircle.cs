using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSmallCircle
{
    [SouvenirQuestion("How much did the sequence shift by in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8")]
    Shift,

    [SouvenirQuestion("Which wedge made the different noise in the beginning of {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black", TranslateAnswers = true)]
    Wedge,

    [SouvenirQuestion("Which color was {1} in the solution to {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Solution
}

public partial class SouvenirModule
{
    [SouvenirHandler("smallCircle", "Small Circle", typeof(SSmallCircle), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessSmallCircle(ModuleData module)
    {
        var comp = GetComponent(module, "smallCircle");
        yield return WaitForSolve;

        var shift = GetField<int>(comp, "shift").Get();
        var tableColor = GetField<int>(comp, "tableColor").Get();
        var solution = GetArrayField<int>(comp, "solution").Get();
        var colorNames = GetStaticField<string[]>(comp.GetType(), "colorNames").Get().Select(x => x[0].ToString().ToUpperInvariant() + x.Substring(1)).ToArray();
        yield return question(SSmallCircle.Shift).Answers(shift.ToString());
        yield return question(SSmallCircle.Wedge).Answers(colorNames[tableColor]);
        for (var i = 0; i < 3; i++)
            yield return question(SSmallCircle.Solution, args: [Ordinal(i + 1)]).Answers(colorNames[solution[i]]);
    }
}