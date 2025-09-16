using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPointlessMachines
{
    [SouvenirQuestion("What color flashed {1} in {0}?", TwoColumns4Answers, "White", "Purple", "Red", "Blue", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Flashes
}

public partial class SouvenirModule
{
    [SouvenirHandler("PointlessMachines", "Pointless Machines", typeof(SPointlessMachines), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessPointlessMachines(ModuleData module)
    {
        var comp = GetComponent(module, "PointlessMachinesScript");
        yield return WaitForSolve;

        var flashes = GetField<Array>(comp, "_souvenirFlashes")
            .Get(v =>
                v.Length != 6 ? "Expected array length 6" :
                v.Cast<int>().Any(i => i is < 0 or >= 5) ? "Expected color 0–4" : null)
            .Cast<object>()
            .Select(v => v.ToString())
            .ToArray();

        // All 5 colors always appear (with one duplicate), so no need to add preferredWrongAnswers
        for (var i = 0; i < flashes.Length; i++)
            yield return question(SPointlessMachines.Flashes, args: [Ordinal(i + 1)]).Answers(flashes[i]);
    }
}