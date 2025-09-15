using System.Collections.Generic;
using Souvenir;

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
        var rotComp = GetArrayField<int>(comp, "assignedDialRotations").Get();
        var dialLabels = GetField<string>(comp, "encryptedDisplay").Get();

        for (var dial = 0; dial < 8; dial++)
        {
            switch (dial)
            {
                case 0:
                    yield return question(SEnigmaCycle.DialDirectionsThree, args: [Ordinal(dial + 1)]).Answers(CycleModuleThreeSprites[rotComp[dial]], preferredWrong: CycleModuleThreeSprites);
                    break;
                case 4:
                case 5:
                case 6:
                    yield return question(SEnigmaCycle.DialDirectionsTwelve, args: [Ordinal(dial + 1)]).Answers(CycleModuleTwelveSprites[rotComp[dial]], preferredWrong: CycleModuleTwelveSprites);
                    break;
                default:
                    yield return question(SEnigmaCycle.DialDirectionsEight, args: [Ordinal(dial + 1)]).Answers(CycleModuleEightSprites[rotComp[dial]], preferredWrong: CycleModuleEightSprites);
                    break;
            }
            yield return question(SEnigmaCycle.DialLabels, args: [Ordinal(dial + 1)]).Answers(dialLabels[dial].ToString());
        }
    }
}