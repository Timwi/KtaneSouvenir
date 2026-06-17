using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S4DTunnels
{
    [Question("What was the {1} goal node in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, Type = AnswerType.FourDTunnelsFont)]
    [AnswerGenerator.Strings("\uE900\uE901\uE902\uE903\uE904\uE905\uE906\uE907\uE908\uE909\uE90A\uE90B\uE90C\uE90D\uE90E\uE90F\uE910\uE911\uE912\uE913\uE914\uE915\uE916\uE917\uE918\uE919\uE91A\uE91B\uE91C\uE91D\uE91E\uE91F\uE920\uE921\uE922\uE923\uE924\uE925\uE926\uE927\uE928\uE929\uE92A\uE92B\uE92C\uE92D\uE92E\uE92F\uE930\uE931\uE932\uE933\uE934\uE935\uE936\uE937\uE938\uE939\uE93A\uE93B\uE93C\uE93D\uE93E\uE93F\uE940\uE941\uE942\uE943\uE944\uE945\uE946\uE947\uE948\uE949\uE94A\uE94B\uE94C\uE94D\uE94E\uE94F\uE950")]
    TargetNode
}

public partial class SouvenirModule
{
    [Handler("4dTunnels", "4D Tunnels", typeof(S4DTunnels), "Quinn Wuest")]
    [ManualQuestion("What were the goal symbols?")]
    [NoDiscriminator]   // unless we want to turn the symbols from the font into sprites
    private IEnumerator<SouvenirInstruction> Process4DTunnels(ModuleData module)
    {
        var comp = GetComponent(module, "FourDTunnels");
        yield return WaitForSolve;

        var symbols = GetStaticField<string>(comp.GetType(), "_symbols").Get();
        var targetNodeNames = GetListField<int>(comp, "_targetNodes")
            .Get(tns => tns.Any(tn => tn < 0 || tn >= symbols.Length) ? "invalid symbols" : null)
            .Select(tn => symbols[tn].ToString())
            .ToArray();
        for (var ix = 0; ix < targetNodeNames.Length; ix++)
            yield return question(S4DTunnels.TargetNode, args: [Ordinal(ix + 1)]).Answers(targetNodeNames[ix], preferredWrong: targetNodeNames);
    }
}
