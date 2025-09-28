using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SNim
{
    [SouvenirQuestion("How many matches were in the {1} row in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(5, 12)]
    MatchCountFirstRow,

    [SouvenirQuestion("How many matches were in the {1} row in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(5, 15)]
    MatchCountOtherRows
}

public partial class SouvenirModule
{
    [SouvenirHandler("nim", "Nim", typeof(SNim), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNim(ModuleData module)
    {
        var comp = GetComponent(module, "NimModule");
        var rows = GetArrayField<int>(comp, "_rows").Get().ToArray();
        yield return WaitForSolve;

        for (int i = 0; i < rows.Length; i++)
            yield return question(i == 0 ? SNim.MatchCountFirstRow : SNim.MatchCountOtherRows, args: [Ordinal(i + 1)]).Answers(rows[i].ToString());
    }
}
