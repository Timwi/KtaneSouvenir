using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SConnectionCheck
{
    [SouvenirQuestion("What pair of numbers was present in {0}?", ThreeColumns6Answers, "2 1", "3 1", "4 1", "5 1", "6 1", "7 1", "8 1", "1 2", "3 2", "4 2", "5 2", "6 2", "7 2", "8 2", "1 3", "2 3", "4 3", "5 3", "6 3", "7 3", "8 3", "1 4", "2 4", "3 4", "5 4", "6 4", "7 4", "8 4", "1 5", "2 5", "3 5", "4 5", "6 5", "7 5", "8 5", "1 6", "2 6", "3 6", "4 6", "5 6", "7 6", "8 6", "1 7", "2 7", "3 7", "4 7", "5 7", "6 7", "8 7", "1 8", "2 8", "3 8", "4 8", "5 8", "6 8", "7 8")]
    Numbers,

    [SouvenirDiscriminator("the Connection Check with no {0}s", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1)]
    NoNs,

    [SouvenirDiscriminator("the Connection Check with one {0}", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1)]
    OneN,

    [SouvenirDiscriminator("the Connection Check with two {0}s", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1)]
    TwoNs,

    [SouvenirDiscriminator("the Connection Check with three {0}s", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1)]
    ThreeNs,

    [SouvenirDiscriminator("the Connection Check with four {0}s", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1)]
    FourNs
}

public partial class SouvenirModule
{
    [SouvenirHandler("graphModule", "Connection Check", typeof(SConnectionCheck), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessConnectionCheck(ModuleData module)
    {
        var comp = GetComponent(module, "GraphModule");

        var valid = new float[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var numbersOnModule = GetArrayField<Vector2>(comp, "Queries")
            .Get(expectedLength: 4, validator: v =>
                !valid.Contains(v.x) ? $"x out of bounds (got: {v.x})" :
                !valid.Contains(v.y) ? $"y out of bounds (got: {v.y})" :
                v.y <= v.x ? $"y less than or equal to x (got: {v.x} {v.y})" : null)
            .Select(v => (l: (int) v.x, r: (int) v.y))
            .ToArray();

        var digitCounts = new Dictionary<int, int>();
        foreach (var (l, r) in numbersOnModule)
        {
            digitCounts.IncSafe(l);
            digitCounts.IncSafe(r);
        }

        yield return WaitForSolve;

        string stringifyPair((int l, int r) pair) => $"{pair.l} {pair.r}";
        string stringifyReverse((int l, int r) pair) => $"{pair.r} {pair.l}";
        foreach (var pair in numbersOnModule)
            yield return question(SConnectionCheck.Numbers).Answers([stringifyPair(pair), stringifyReverse(pair)], preferredWrong: numbersOnModule.Select(stringifyPair).ToArray());

        var discrs = new[] { SConnectionCheck.NoNs, SConnectionCheck.OneN, SConnectionCheck.TwoNs, SConnectionCheck.ThreeNs, SConnectionCheck.FourNs };
        for (var n = 1; n <= 8; n++)
            if (digitCounts.Get(n, 0) is int count)
                yield return new Discriminator(discrs[count], $"n-{n}", count);

        var leftDisplays = GetArrayField<GameObject>(comp, "L", true).Get(expectedLength: 4);
        var rightDisplays = GetArrayField<GameObject>(comp, "R", true).Get(expectedLength: 4);

        IEnumerator removeDisplays()
        {
            foreach (var num in Enumerable.Range(0, 4).SelectMany(i => new[] { leftDisplays[i], rightDisplays[i] }))
            {
                num.GetComponentInChildren<TextMesh>().text = "!";
                yield return new WaitForSeconds(.1f);
            }
        }
        StartCoroutine(removeDisplays());
    }
}
