using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDecolourFlash
{
    [SouvenirQuestion("What was the {1} of the {2} goal in {0}?", ThreeColumns6Answers, "Blue", "Green", "Red", "Magenta", "Yellow", "White", Arguments = ["colour", QandA.Ordinal, "word", QandA.Ordinal], ArgumentGroupSize = 2, TranslateAnswers = true, TranslateArguments = [true, false])]
    Goal
}

public partial class SouvenirModule
{
    [SouvenirHandler("DecolourFlashModule", "Decolour Flash", typeof(SDecolourFlash), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessDecolourFlash(ModuleData module)
    {
        var comp = GetComponent(module, "DecolourFlashScript");
        yield return WaitForSolve;

        var names = new[] { "Blue", "Green", "Red", "Magenta", "Yellow", "White" };
        var goals = GetField<IList>(comp, "_goals").Get(validator: l => l.Count != 3 ? "expected length 3" : null);
        var hexGrid = GetField<IDictionary>(comp, "_hexes").Get(validator: d => !goals.Cast<object>().All(g => d.Contains(g)) ? "key missing in dictionary" : null);
        var infos = goals.Cast<object>().Select(goal => hexGrid[goal]).ToArray();
        var fldColour = GetField<object>(infos[0], "ColourIx");
        var fldWord = GetField<object>(infos[0], "Word");
        var colours = infos.Select(inf => (int) fldColour.GetFrom(inf)).ToArray();
        var words = infos.Select(inf => (int) fldWord.GetFrom(inf)).ToArray();
        if (colours.Any(c => c is < 0 or >= 6) || words.Any(w => w is < 0 or >= 6))
            throw new AbandonModuleException($"colours/words are: [{colours.JoinString(", ")}], [{words.JoinString(", ")}]; expected values 0–5");
        for (var i = 0; i < 3; i++)
        {
            yield return question(SDecolourFlash.Goal, args: ["colour", Ordinal(i + 1)]).Answers(names[colours[i]]);
            yield return question(SDecolourFlash.Goal, args: ["word", Ordinal(i + 1)]).Answers(names[words[i]]);
        }
    }
}