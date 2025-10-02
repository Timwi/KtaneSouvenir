using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAffineCycle
{
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,

    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    DialLabels,

    [SouvenirDiscriminator("the Affine Cycle that had the letter {0} on a dial", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"], ArgumentGroupSize = 1)]
    LabelDiscriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("affineCycle", "Affine Cycle", typeof(SAffineCycle), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessAffineCycle(ModuleData module) => processSpeakingEvilCycle(
        module, "AffineCycleScript", SAffineCycle.DialDirections, SAffineCycle.DialLabels, SAffineCycle.LabelDiscriminator,
        all: Enumerable.Range(0, 8).Except([6]).Select(x => CycleModuleEightSprites[x]).ToArray());
}
