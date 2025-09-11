using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SEnigmaCycle
{
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", ThreeColumns3Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleThreeSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirectionsThree,
    
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleTwelveSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirectionsTwelve,
    
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DialDirectionsEight,
    
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    DialLabels
}

public partial class SouvenirModule
{
    [SouvenirHandler("enigmaCycle", "Enigma Cycle", typeof(SEnigmaCycle), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessEnigmaCycle(ModuleData module)
    {
        var comp = GetComponent(module, "EnigmaCycleScript");
        yield return WaitForSolve;

        var qs = new List<QandA>();
        var rotComp = GetArrayField<int>(comp, "assignedDialRotations").Get();
        var dialLabels = GetField<string>(comp, "encryptedDisplay").Get();

        for (var dial = 0; dial < 8; dial++)
        {
            switch (dial)
            {
                case 0:
                    qs.Add(makeQuestion(Question.EnigmaCycleDialDirectionsThree, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleThreeSprites[rotComp[dial]] }, preferredWrongAnswers: CycleModuleThreeSprites));
                    break;
                case 4:
                case 5:
                case 6:
                    qs.Add(makeQuestion(Question.EnigmaCycleDialDirectionsTwelve, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleTwelveSprites[rotComp[dial]] }, preferredWrongAnswers: CycleModuleTwelveSprites));
                    break;
                default:
                    qs.Add(makeQuestion(Question.EnigmaCycleDialDirectionsEight, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleEightSprites[rotComp[dial]] }, preferredWrongAnswers: CycleModuleEightSprites));
                    break;
            }
            qs.Add(makeQuestion(Question.EnigmaCycleDialLabels, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { dialLabels[dial].ToString() }));
        }

        addQuestions(module, qs);
    }
}