using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHypercube
{
    [SouvenirQuestion("What was the {1} rotation in {0}?", ThreeColumns6Answers, "XY", "YX", "XZ", "ZX", "XW", "WX", "YZ", "ZY", "YW", "WY", "ZW", "WZ", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Rotations
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheHypercubeModule", "Hypercube", typeof(SHypercube), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessHypercube(ModuleData module) => processHypercubeUltracube(module, "TheHypercubeModule", Question.HypercubeRotations);
}