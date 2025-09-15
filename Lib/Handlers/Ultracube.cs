using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUltracube
{
    [SouvenirQuestion("What was the {1} rotation in {0}?", ThreeColumns6Answers, "XY", "YX", "XZ", "ZX", "XW", "WX", "XV", "VX", "YZ", "ZY", "YW", "WY", "YV", "VY", "ZW", "WZ", "ZV", "VZ", "WV", "VW", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Rotations
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheUltracubeModule", "Ultracube", typeof(SUltracube), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessUltracube(ModuleData module) => processHypercubeUltracube(module, "TheUltracubeModule", Question.UltracubeRotations);
}