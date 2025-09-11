using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCrypticCycle
{
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleCrypticSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,
    
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Type = AnswerType.CrypticCycleBoozleglyphFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"£$%^&*()[]{}<>")]
    DialLabels
}

public partial class SouvenirModule
{
    [SouvenirHandler("crypticCycle", "Cryptic Cycle", typeof(SCrypticCycle), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessCrypticCycle(ModuleData module) => processSpeakingEvilCycle(module, "CrypticCycleScript", Question.CrypticCycleDialDirections, Question.CrypticCycleDialLabels)
}