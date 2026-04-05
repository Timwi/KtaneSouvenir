using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SNotDoubleOh
{
    [Question("What was the {1} displayed position in the second stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(2, 'A', 'H')]
    QPosition,

    [Discriminator("the Not Double-Oh where the {0} displayed position was {1}", Arguments = [QandA.Ordinal, "AA"], ArgumentGroupSize = 2)]
    DPosition
}

public partial class SouvenirModule
{
    [Handler("NotDoubleOhModule", "Not Double-Oh", typeof(SNotDoubleOh), "Anonymous")]
    [ManualQuestion("What were the displayed positions in the second stage?")]
    private IEnumerator<SouvenirInstruction> ProcessNotDoubleOh(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "NotDoubleOhScript");
        var positions = GetArrayField<int>(comp, "_goalPos").Get(expectedLength: 8, validator: v => v is < 0 or > 63 ? "Out of range 0–63" : null);
        var grid = GetStaticField<string[]>(comp.GetType(), "_grid").Get(arr => arr.Length != 64 ? "Expected 64 elements" : arr.Any(s => !Regex.IsMatch(s, "^[A-H]{2}$")) ? $"Expected all strings to match /^[A-H]{{2}}$/, got: {arr.JoinString(", ")}" : null);
        var displays = positions.Select(p => grid[p]).ToArray();

        var leftSegs = GetArrayField<GameObject>(comp, "LeftSegObjs", true).Get(expectedLength: 7);
        var rightSegs = GetArrayField<GameObject>(comp, "RightSegObjs", true).Get(expectedLength: 7);
        foreach (var seg in leftSegs.Concat(rightSegs))
            seg.SetActive(false);

        for (var i = 0; i < 8; i++)
        {
            yield return new Discriminator(SNotDoubleOh.DPosition, $"pos-{i}", displays[i], args: [Ordinal(i + 1), displays[i]]);
            yield return question(SNotDoubleOh.QPosition, args: [Ordinal(i + 1)])
                .AvoidDiscriminators($"pos-{i}")
                .Answers(displays[i]);
        }
    }
}