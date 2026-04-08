using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S3LEDs
{
    [Question("What was the initial state of the LEDs in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "ThreeLEDsSprites")]
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

        int value = GetArrayField<bool>(comp, "initialStates").Get(expectedLength: 3).Aggregate(0, (a, b) => (a << 1) | (b ? 1 : 0));
        yield return question(S3LEDs.InitialState).Answers(ThreeLEDsSprites[value], preferredWrong: ThreeLEDsSprites);
    }
}