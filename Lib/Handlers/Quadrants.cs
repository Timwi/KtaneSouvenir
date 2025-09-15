using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SQuadrants
{
    [SouvenirQuestion("What was on the {1} button of the {2} stage in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "+", "-", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Buttons
}

public partial class SouvenirModule
{
    [SouvenirHandler("Quadrants", "Quadrants", typeof(SQuadrants), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessQuadrants(ModuleData module)
    {
        var comp = GetComponent(module, "Quadrants");

        var chosenSet = GetIntField(comp, "ChosenSet");
        var ixOfSymbolsComp = GetArrayField<int>(comp, "SymbolsIndex");
        var parityOfSymbolsComp = GetArrayField<bool>(comp, "PositiveCoordinate");
        var stageComp = GetIntField(comp, "Stage");

        var stages = new string[5];
        string[] sets = ["1243", "1324", "1432", "2134", "2341", "2413", "3142", "3214", "3421", "4123", "4231", "3214"];

        var btnsAtAllStages = new string[5];
        while (module.Unsolved)
        {
            var stage = stageComp.Get() - 1;
            if (stage == -1)
            {
                yield return null;
                continue;
            }
            var set = chosenSet.Get();
            var ixOfSymbols = ixOfSymbolsComp.Get();
            var parityOfSymbols = parityOfSymbolsComp.Get();
            var btns = new string[4];
            for (var i = 0; i < 4; i++)
            {
                btns[i] = ixOfSymbols.Contains(i)
                    ? ixOfSymbols.First() == i ? parityOfSymbols.First() ? "+" : "-" : parityOfSymbols.Last() ? "+" : "-"
                    : sets[set][i].ToString();
            }
            btnsAtAllStages[stage] = btns.JoinString();
            yield return null;
        }

        for (var s = 0; s < 5; s++)
            for (var b = 0; b < 4; b++)
                yield return question(SQuadrants.Buttons, args: [Ordinal(b + 1), Ordinal(s + 1)]).Answers(btnsAtAllStages[s][b].ToString());
    }
}
