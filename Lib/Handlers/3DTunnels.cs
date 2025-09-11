using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum S3DTunnels
{
    [SouvenirQuestion("What was the {1} goal node in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings("a-z.")]
    TargetNode
}

public partial class SouvenirModule
{
    [SouvenirHandler("3dTunnels", "3D Tunnels", typeof(S3DTunnels), "Timwi")]
    private IEnumerator<SouvenirInstruction> Process3DTunnels(ModuleData module)
    {
        var comp = GetComponent(module, "ThreeDTunnels");
        yield return WaitForSolve;

        var symbols = GetStaticField<string>(comp.GetType(), "_symbols").Get();
        var targetNodeNames = GetListField<int>(comp, "_targetNodes")
            .Get(tns => tns.Any(tn => tn < 0 || tn >= symbols.Length) ? "invalid symbols" : null)
            .Select(tn => symbols[tn].ToString())
            .ToArray();
        addQuestions(module, targetNodeNames.Select((tn, ix) => makeQuestion(Question._3DTunnelsTargetNode, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { tn }, preferredWrongAnswers: targetNodeNames)));
    }
}