using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SUSACycle
{
    [SouvenirQuestion("Which state was displayed in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites)]
    Displayed
}

public partial class SouvenirModule
{
    private static readonly Dictionary<string, float> _usaCycle_StateSizes = new()
    {
        ["Alabama"] = 3600,
        ["Alaska"] = 3600,
        ["Arizona"] = 3600,
        ["Arkansas"] = 3600,
        ["California"] = 8000,
        ["Colorado"] = 3600,
        ["Connecticut"] = 3600,
        ["Delaware"] = 5000,
        ["Florida"] = 4000,
        ["Georgia"] = 3600,
        ["Hawaii"] = 3600,
        ["Idaho"] = 5000,
        ["Illinois"] = 3600,
        ["Indiana"] = 3600,
        ["Iowa"] = 3600,
        ["Kansas"] = 3800,
        ["Kentucky"] = 3600,
        ["Louisiana"] = 3600,
        ["Maine"] = 5000,
        ["Maryland"] = 5000,
        ["Massachuesetts"] = 5000,
        ["Michigan"] = 4000,
        ["Minnesota"] = 5000,
        ["Mississippi"] = 3600,
        ["Missouri"] = 3600,
        ["Montana"] = 5000,
        ["Nebraska"] = 4000,
        ["Nevada"] = 6000,
        ["New Hampshire"] = 6000,
        ["New Jersey"] = 5000,
        ["New Mexico"] = 4000,
        ["New York"] = 3600,
        ["North Carolina"] = 4000,
        ["North Dakota"] = 3600,
        ["Ohio"] = 3600,
        ["Oklahoma"] = 4000,
        ["Oregon"] = 4500,
        ["Pennsylvania"] = 3600,
        ["Rhode Island"] = 1250,
        ["South Carolina"] = 3600,
        ["South Dakota"] = 4000,
        ["Tennessee"] = 4000,
        ["Texas"] = 7000,
        ["Utah"] = 3600,
        ["Vermont"] = 2000,
        ["Virginia"] = 3600,
        ["Washington"] = 3600,
        ["West Virginia"] = 3600,
        ["Wisconsin"] = 9000,
        ["Wyoming"] = 5000
    };

    [SouvenirHandler("USACycle", "USA Cycle", typeof(SUSACycle), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessUSACycle(ModuleData module)
    {
        var comp = GetComponent(module, "USACycle");
        var fldStateIndices = GetListField<int>(comp, "StateIndexes");
        var sprites = GetArrayField<Sprite>(comp, "States", isPublic: true)
            .Get(expectedLength: 50, validator: spr => _usaCycle_StateSizes.ContainsKey(spr.name) ? null : $"Unknown US state: {spr.name}")
            .Select(spr => spr.TranslateSprite(_usaCycle_StateSizes[spr.name]))
            .ToArray();
        yield return WaitForSolve;

        var stateIndices = fldStateIndices.Get(minLength: 4).Where(ix => ix is not 5 and not 49).ToArray();

        yield return question(SUSACycle.Displayed).Answers(stateIndices.Select(ix => sprites[ix]).ToArray(),
            // Exclude Colorado and Wyoming because they are practically indistinguishable
            all: sprites.Where((_, pos) => pos is not 5 and not 49).ToArray());
    }
}
