using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSmashMarryKill
{
    [SouvenirQuestion("In what category was {1} for {0}?", TwoColumns4Answers, "SMASH", "MARRY", "KILL", Arguments = ["The Button", "Maze", "Memory", "Morse Code", "Password", "Simon Says", "Who’s on First", "Wires", "Wire Sequence"], ArgumentGroupSize = 1)]
    Category,

    [SouvenirQuestion("Which module was in the {1} category for {0}?", OneColumn4Answers, ExampleAnswers = ["The Button", "Maze", "Memory", "Morse Code", "Password", "Simon Says", "Who’s on First", "Wires", "Wire Sequence"], Arguments = ["SMASH", "MARRY", "KILL"], ArgumentGroupSize = 1)]
    Module,

    [SouvenirDiscriminator("Smash, Marry, Kill")]
    NullDiscriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("smashmarrykill", "Smash, Marry, Kill", typeof(SSmashMarryKill), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSmashMarryKill(ModuleData module)
    {
        var comp = GetComponent(module, "SmashMarryKill");
        yield return WaitForUnignoredModules;

        // All SMyK modules on a bomb share information,
        // so we don't need to keep track of solve order at all,
        // nor even disambiguate the modules.
        yield return new Discriminator(SSmashMarryKill.NullDiscriminator, "", module.Module);

        var assignments = GetStaticField<IDictionary>(comp.GetType(), "allModules").Get();
        if (assignments.Count == 0)
            yield return legitimatelyNoQuestion(module, "No modules were categorized.");
        var smash = new List<string>();
        var marry = new List<string>();
        var kill = new List<string>();
        foreach (DictionaryEntry de in assignments)
        {
            if (de.Value.ToString() == "SMASH")
                smash.Add((string) de.Key);
            if (de.Value.ToString() == "MARRY")
                marry.Add((string) de.Key);
            if (de.Value.ToString() == "KILL")
                kill.Add((string) de.Key);
            yield return question(SSmashMarryKill.Category, args: [(string) de.Key]).Answers(de.Value.ToString());
        }
        var allMods = smash.Concat(marry).Concat(kill).ToArray();
        if (allMods.Length < 4)
            allMods = Bomb.GetSolvableModuleNames().Distinct().ToArray();
        if (smash.Count > 0)
            yield return question(SSmashMarryKill.Module, args: ["SMASH"]).Answers(smash.ToArray(), all: allMods);
        if (marry.Count > 0)
            yield return question(SSmashMarryKill.Module, args: ["MARRY"]).Answers(marry.ToArray(), all: allMods);
        if (kill.Count > 0)
            yield return question(SSmashMarryKill.Module, args: ["KILL"]).Answers(kill.ToArray(), all: allMods);
    }
}
