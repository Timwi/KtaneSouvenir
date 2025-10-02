using System;
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
    DialLabels,

    [SouvenirDiscriminator("the Enigma Cycle that had the letter {0} on a dial", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"], ArgumentGroupSize = 1)]
    LabelDiscriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("enigmaCycle", "Enigma Cycle", typeof(SEnigmaCycle), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessEnigmaCycle(ModuleData module)
    {
        var comp = GetComponent(module, "EnigmaCycleScript");
        yield return WaitForSolve;
        var rotComp = GetArrayField<int>(comp, "assignedDialRotations").Get(expectedLength: 8);
        var dialLabels = GetField<string>(comp, "encryptedDisplay").Get(v => v.Length != 8 ? "expected length 8" : null);

        // Each dial has a different number of possible rotations
        var info = Ut.NewArray<(Enum question, Sprite[] sprites)>(
            /* 0: 3 rotations */ (SEnigmaCycle.DialDirectionsThree, CycleModuleThreeSprites),
            /* 1: 8 rotations */ (SEnigmaCycle.DialDirectionsEight, CycleModuleEightSprites),
            /* 2: 8 rotations */ (SEnigmaCycle.DialDirectionsEight, CycleModuleEightSprites),
            /* 3: 8 rotations */ (SEnigmaCycle.DialDirectionsEight, CycleModuleEightSprites),
            /* 4: 12 rotations */ (SEnigmaCycle.DialDirectionsTwelve, CycleModuleTwelveSprites),
            /* 5: 12 rotations */ (SEnigmaCycle.DialDirectionsTwelve, CycleModuleTwelveSprites),
            /* 6: 12 rotations */ (SEnigmaCycle.DialDirectionsTwelve, CycleModuleTwelveSprites),
            /* 7: 8 rotations */ (SEnigmaCycle.DialDirectionsEight, CycleModuleEightSprites));

        for (var dial = 0; dial < 8; dial++)
        {
            yield return question(info[dial].question, args: [Ordinal(dial + 1)]).Answers(info[dial].sprites[rotComp[dial]], all: info[dial].sprites);
            yield return question(SEnigmaCycle.DialLabels, args: [Ordinal(dial + 1)]).AvoidDiscriminators(SEnigmaCycle.LabelDiscriminator).Answers(dialLabels[dial].ToString());
        }

        foreach (var ltr in dialLabels.Distinct())
            yield return new Discriminator(SEnigmaCycle.LabelDiscriminator, $"ltr-{ltr}", args: [ltr.ToString()]);
    }
}
