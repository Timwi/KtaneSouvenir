using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRetirement
{
    [SouvenirQuestion("Which one of these houses was on offer, but not chosen by Bob in {0}?", TwoColumns4Answers, ExampleAnswers = ["Hotham Place", "Homestead", "Riverwell", "Lodge Park"])]
    Houses
}

public partial class SouvenirModule
{
    [SouvenirHandler("retirement", "Retirement", typeof(SRetirement), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessRetirement(ModuleData module)
    {
        var comp = GetComponent(module, "retirementScript");
        yield return WaitForSolve;

        var homes = GetArrayField<string>(comp, "retirementHomeOptions", isPublic: true).Get();
        var available = GetArrayField<string>(comp, "selectedHomes").Get();
        var correct = GetField<string>(comp, "correctHome").Get(str => str == "" ? "empty" : null);
        yield return question(SRetirement.Houses).Answers(available.Where(x => x != correct).ToArray(), preferredWrong: homes);
    }
}