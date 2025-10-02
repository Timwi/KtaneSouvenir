using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SJumbleCycle
{
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,

    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    DialLabels,

    [SouvenirDiscriminator("the Jumble Cycle that had the letter {0} on a dial", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"], ArgumentGroupSize = 1)]
    LabelDiscriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("jumbleCycle", "Jumble Cycle", typeof(SJumbleCycle), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessJumbleCycle(ModuleData module) => processSpeakingEvilCycle(
        module, "JumbleCycleScript", SJumbleCycle.DialDirections, SJumbleCycle.DialLabels, SJumbleCycle.LabelDiscriminator,
        getDialLabels: comp => GetArrayField<string[]>(comp, "ciphertext").Get()[0][4]);
}
