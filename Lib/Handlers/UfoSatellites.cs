using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUfoSatellites
{
    [SouvenirQuestion("Which number was not present on {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(0, 9)]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("UfoSatellites", "UFO Satellites", typeof(SUfoSatellites), "thunder725")]
    private IEnumerator<SouvenirInstruction> ProcessUfoSatellites(ModuleData module)
    {
        var comp = GetComponent(module, "UfoSatellitesScript");

        // The array is supposed to contain at least 3 distinct numbers
        var allNumbers = GetField<int[]>(comp, "satelliteNumbers").Get(
            v => v.Length != 6 ? "expected length 6" :
            v.Distinct().Count() < 3 ? "expected at least 3 distinct values" : null);

        yield return WaitForSolve;

        yield return question(SUfoSatellites.Numbers).Answers(Enumerable.Range(0, 10).Except(allNumbers).Select(n => n.ToString()).ToArray(),
            preferredWrong: allNumbers.Select(n => n.ToString()).ToArray());
    }
}
