using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHillCycle
{
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleFiveSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,
    
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    DialLabels
}

public partial class SouvenirModule
{
    [SouvenirHandler("hillCycle", "Hill Cycle", typeof(SHillCycle), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessHillCycle(ModuleData module) => processSpeakingEvilCycle(module, "HillCycleScript", Question.HillCycleDialDirections, Question.HillCycleDialLabels);
}