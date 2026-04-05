using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCrypticCycle
{
    [Question("Which direction was the {1} dial pointing in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleCrypticSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,

    [Question("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Type = AnswerType.CrypticCycleBoozleglyphFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"£$%^&*()[]{}<>")]
    DialLabels
}

public partial class SouvenirModule
{
    [Handler("crypticCycle", "Cryptic Cycle", typeof(SCrypticCycle), "Timwi")]
    [ManualQuestion("Which direction were the dials pointing?")]
    [ManualQuestion("What was written on each dial?")]
    private IEnumerator<SouvenirInstruction> ProcessCrypticCycle(ModuleData module) => processSpeakingEvilCycle(
        module, "CrypticCycleScript", SCrypticCycle.DialDirections, SCrypticCycle.DialLabels, null,
        answerSprites: CycleModuleCrypticSprites, all: CycleModuleCrypticSprites);
}
