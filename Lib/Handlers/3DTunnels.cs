using System.Collections.Generic;
using System.Linq;
using Souvenir;

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

        var symbols = GetField<string>(comp, "_symbols").Get(v => v.Length != 27 ? "expected length 27" : !v.All(c => (c >= 'a' && c <= 'z') || c == '.') ? "expected a-z and . (period)" : null);
        var targetNodeNames = GetArrayField<int>(comp, "_targetNodes")
            .Get(tns => tns.Any(tn => tn < 0 || tn >= symbols.Length) ? "invalid symbols" : null)
            .Select(tn => symbols[tn].ToString())
            .ToArray();
        for (var ix = 0; ix < targetNodeNames.Length; ix++)
            yield return question(S3DTunnels.TargetNode, args: [Ordinal(ix + 1)]).Answers(targetNodeNames[ix], preferredWrong: targetNodeNames);
    }
}
