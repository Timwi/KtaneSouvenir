using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUltimateCycle
{
    [Question("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,

    [Question("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    DialLabels,

    [Discriminator("the Ultimate Cycle that had the letter {0} on a dial", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"], ArgumentGroupSize = 1)]
    LabelDiscriminator
}

public partial class SouvenirModule
{
    [Handler("ultimateCycle", "Ultimate Cycle", typeof(SUltimateCycle), "Timwi")]
    [ManualQuestion("Which direction were the dials pointing?")]
    [ManualQuestion("What was written on each dial?")]
    private IEnumerator<SouvenirInstruction> ProcessUltimateCycle(ModuleData module) => processSpeakingEvilCycle(
        module, "UltimateCycleScript", SUltimateCycle.DialDirections, SUltimateCycle.DialLabels, SUltimateCycle.LabelDiscriminator,
        getDialLabels: comp => GetArrayField<string[]>(comp, "ciphertext").Get()[0][8]);
}
