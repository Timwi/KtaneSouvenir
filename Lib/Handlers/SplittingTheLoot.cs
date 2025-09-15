using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSplittingTheLoot
{
    [SouvenirQuestion("What bag was initially colored in {0}?", ThreeColumns6Answers, ExampleAnswers = ["A5", "E6", "19", "82"])]
    ColoredBag
}

public partial class SouvenirModule
{
    [SouvenirHandler("SplittingTheLootModule", "Splitting The Loot", typeof(SSplittingTheLoot), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessSplittingTheLoot(ModuleData module)
    {
        yield return WaitForActivate;

        var comp = GetComponent(module, "SplittingTheLootScript");
        var bags = (IList) GetField<object>(comp, "bags").Get(lst => lst is not IList list ? "expected an IList" : list.Count != 7 ? "expected length 7" : null);
        var fldBagColor = GetField<object>(bags[0], "Color");
        var fldBagLabel = GetField<string>(bags[0], "Label");
        var bagColors = bags.Cast<object>().Select(obj => fldBagColor.GetFrom(obj)).ToArray();
        var bagLabels = bags.Cast<object>().Select(obj => fldBagLabel.GetFrom(obj)).ToArray();
        var paintedBag = bagColors.IndexOf(bc => bc.ToString() != "Normal");
        yield return paintedBag == -1
            ? throw new AbandonModuleException($"No colored bag was found: [{bagColors.JoinString(", ")}]")
            : (YieldInstruction) WaitForSolve;
        yield return question(SSplittingTheLoot.ColoredBag).Answers(bagLabels[paintedBag], preferredWrong: bagLabels);
    }
}