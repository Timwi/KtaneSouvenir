using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum S3LEDs
{
    [Question("What was the initial state of the LEDs in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "ThreeLEDsSprites")]
    InitialState
}

public partial class SouvenirModule
{
    [Handler("threeLEDsModule", "3 LEDs", typeof(S3LEDs), "Quinn Wuest")]
    [ManualQuestion("What was the initial state of the LEDs?")]
    private IEnumerator<SouvenirInstruction> Process3LEDs(ModuleData module)
    {
        var comp = GetComponent(module, "ThreeLEDsScript");
        yield return WaitForSolve;

        var lights = GetArrayField<Light>(comp, "lights", isPublic: true).Get(expectedLength: 3);

        int value = GetArrayField<bool>(comp, "initialStates").Get(expectedLength: 3).Aggregate(0, (a, b) => (a << 1) | (b ? 1 : 0));
        yield return question(S3LEDs.InitialState).Answers(ThreeLEDsSprites[value], preferredWrong: ThreeLEDsSprites);

        // Turn off the LEDs
        foreach (var light in lights)
            light.enabled = false;
    }
}
