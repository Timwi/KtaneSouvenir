using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPigpenCycle
{
    [Question("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,

    [Question("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    DialLabels,

    [Discriminator("the Pigpen Cycle that had the letter {0} on a dial", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"], ArgumentGroupSize = 1)]
    LabelDiscriminator
}

public partial class SouvenirModule
{
    [Handler("pigpenCycle", "Pigpen Cycle", typeof(SPigpenCycle), "Timwi")]
    [ManualQuestion("Which direction were the dials pointing?")]
    [ManualQuestion("What was written on each dial?")]
    private IEnumerator<SouvenirInstruction> ProcessPigpenCycle(ModuleData module) => processSpeakingEvilCycle(
        module, "PigpenCycleScript", SPigpenCycle.DialDirections, SPigpenCycle.DialLabels, SPigpenCycle.LabelDiscriminator);
}
