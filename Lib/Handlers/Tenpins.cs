using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STenpins
{
    [SouvenirQuestion("What was the {1} split in {0}?", OneColumn4Answers, "Goal Posts", "Cincinnati", "Woolworth Store", "Lily", "3-7 Split", "Cocked Hat", "4-7-10 Split", "Big Four", "Greek Church", "Big Five", "Big Six", "HOW", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    Splits
}

public partial class SouvenirModule
{
    [SouvenirHandler("tenpins", "Tenpins", typeof(STenpins), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessTenpins(ModuleData module)
    {
        var comp = GetComponent(module, "tenpins");
        yield return WaitForSolve;

        var splitNames = new[] { "Goal Posts", "Cincinnati", "Woolworth Store", "Lily", "3-7 Split", "Cocked Hat", "4-7-10 Split", "Big Four", "Greek Church", "Big Five", "Big Six", "HOW" };
        var splits = GetArrayField<int>(comp, "splits").Get(validator: ar => ar.Length != 3 ? "expected length 3" : ar.Any(v => v < 0 || v >= splitNames.Length) ? $"out of range for splitNames (0–{splitNames.Length - 1})" : null);
        var colorNames = new[] { "red", "green", "blue" };
        for (var i = 0; i < 3; i++)
            yield return question(STenpins.Splits, args: [colorNames[i]]).Answers(splitNames[splits[i]], preferredWrong: splits.Select(x => splitNames[x]).ToArray());
    }
}