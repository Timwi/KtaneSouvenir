using System;
using System.Collections;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SLightBulbs
{
    [Question("What was the color of the {1} bulb in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Cyan", "Magenta", TranslateAnswers = true, Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    OuterBulb,

    [Question("What was the color of the center bulb in {0}?", TwoColumns2Answers, "Gray", "White", TranslateAnswers = true)]
    CenterBulb
}

public partial class SouvenirModule
{
    [Handler("LightBulbs", "Light Bulbs", typeof(SLightBulbs), "Kuro")]
    [ManualQuestion("What were the colors of the bulbs?")]
    private IEnumerator<SouvenirInstruction> ProcessLightBulbs(ModuleData module)
    {
        var comp = GetComponent(module, "LightBulbsScript");

        yield return WaitForSolve;

        var bulbs = GetField<IList>(comp, "Bulbs").Get(lst => lst.Count != 3 ? "expected length 3" : null);

        yield return question(SLightBulbs.OuterBulb, args: ["left"]).Answers(GetField<Enum>(bulbs[0], "Color", isPublic: true).Get().ToString());
        yield return question(SLightBulbs.OuterBulb, args: ["right"]).Answers(GetField<Enum>(bulbs[2], "Color", isPublic: true).Get().ToString());
        yield return question(SLightBulbs.CenterBulb).Answers(GetField<Enum>(bulbs[1], "Color", isPublic: true).Get().ToString());
    }
}
