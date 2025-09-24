using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPigpenCycle
{
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,

    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    DialLabels
}

public partial class SouvenirModule
{
    [SouvenirHandler("pigpenCycle", "Pigpen Cycle", typeof(SPigpenCycle), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessPigpenCycle(ModuleData module) => processSpeakingEvilCycle(module, "PigpenCycleScript", SPigpenCycle.DialDirections, SPigpenCycle.DialLabels);
}