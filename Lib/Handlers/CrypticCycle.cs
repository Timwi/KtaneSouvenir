using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCrypticCycle
{
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleCrypticSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,

    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Type = AnswerType.CrypticCycleBoozleglyphFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"£$%^&*()[]{}<>")]
    DialLabels,

    [SouvenirDiscriminator("the Cryptic Cycle that had the letter {0} on a dial", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"], ArgumentGroupSize = 1)]
    LabelDiscriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("crypticCycle", "Cryptic Cycle", typeof(SCrypticCycle), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCrypticCycle(ModuleData module) => processSpeakingEvilCycle(
        module, "CrypticCycleScript", SCrypticCycle.DialDirections, SCrypticCycle.DialLabels, SCrypticCycle.LabelDiscriminator,
        answerSprites: CycleModuleCrypticSprites, all: CycleModuleCrypticSprites);
}
