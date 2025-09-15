using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSubblyJubbly
{
    [SouvenirQuestion("What was a substitution word in {0}?", TwoColumns4Answers, ExampleAnswers = ["AMOGUS", "BOINKY", "CRINGE", "DUMPY", "EUPHEMISM", "FORTNITE"])]
    Substitutions
}

public partial class SouvenirModule
{
    [SouvenirHandler("subblyJubbly", "Subbly Jubbly", typeof(SSubblyJubbly), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSubblyJubbly(ModuleData module)
    {
        var comp = GetComponent(module, "JubblyScript");
        yield return WaitForSolve;

        // Phrases can be customized in mod settings
        var all = GetField<Dictionary<char, List<string>>>(comp, "subblies").Get(v => v.Count == 26 ? "Subblies dict too big" : null).Values.SelectMany(x => x).ToArray();

        var used = GetArrayField<string>(comp, "subselect").Get(expectedLength: 9, validator: v => all.Contains(v) ? null : $"Unknown word {v}");
        yield return question(SSubblyJubbly.Substitutions).Answers(used, all: all);
    }
}