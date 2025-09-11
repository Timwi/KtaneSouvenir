using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAffineCycle
{
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirections,
    
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    DialLabels
}

public partial class SouvenirModule
{
    [SouvenirHandler("affineCycle", "Affine Cycle", typeof(SAffineCycle), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessAffineCycle(ModuleData module) => processSpeakingEvilCycle(module, "AffineCycleScript", Question.AffineCycleDialDirections, Question.AffineCycleDialLabels,
            overrideAnswers: Enumerable.Range(0, 8).Except(new[] { 6 }).Select(x => CycleModuleEightSprites[x]).ToArray())
}