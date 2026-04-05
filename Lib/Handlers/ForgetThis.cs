using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SForgetThis
{
    [Question("What color was the LED in the {1} stage of {0}?", ThreeColumns6Answers, "Cyan", "Magenta", "Yellow", "Black", "White", "Green", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QColors,

    [Question("What was the digit displayed in the {1} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.AsciiMazeFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-9A-Z")]
    QDigits,

    [Discriminator("the Forget This whose LED was {0} in the {1} stage", Arguments = ["cyan", QandA.Ordinal, "magenta", QandA.Ordinal, "yellow", QandA.Ordinal, "black", QandA.Ordinal, "white", QandA.Ordinal, "green", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DColors,

    [Discriminator("the Forget This which displayed {0} in the {1} stage", Arguments = ["A", QandA.Ordinal, "B", QandA.Ordinal, "C", QandA.Ordinal, "D", QandA.Ordinal, "E", QandA.Ordinal, "F", QandA.Ordinal], ArgumentGroupSize = 2)]
    DDigits
}

public partial class SouvenirModule
{
    [Handler("forgetThis", "Forget This", typeof(SForgetThis), "Kuro", IsBossModule = true)]
    [ManualQuestion("What was the displayed character in each stage?")]
    [ManualQuestion("What was the LED color in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessForgetThis(ModuleData module)
    {
        var comp = GetComponent(module, "ForgetThis");

        if (GetField<bool>(comp, "autoSolved").Get())
            yield return legitimatelyNoQuestion(module, "It solved itself due to a lack of stages.");

        var myColors = GetListField<int>(comp, "stageColors").Get();
        var myDigits = GetListField<int>(comp, "stageNumbers").Get();

        if (myColors.Count != myDigits.Count)
            throw new AbandonModuleException($"The number of colors ({myColors.Count}) did not match the number of digits ({myDigits.Count})");

        yield return WaitForUnignoredModules;

        var displayedStagesCount = GetIntField(comp, "curStageNum").Get(min: 0, max: myColors.Count);

        var allColors = new[] { "Cyan", "Magenta", "Yellow", "Black", "White", "Green" };
        var base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (var stage = 0; stage < displayedStagesCount; stage++)
        {
            yield return question(SForgetThis.QColors, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"colors{stage}").Answers(allColors[myColors[stage]]);
            yield return question(SForgetThis.QDigits, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"digits{stage}").Answers(base36[myDigits[stage]].ToString());
            yield return new Discriminator(SForgetThis.DColors, $"colors{stage}", myColors[stage], args: [allColors[myColors[stage]], Ordinal(stage + 1)]);
            yield return new Discriminator(SForgetThis.DDigits, $"digits{stage}", myDigits[stage], args: [base36[myDigits[stage]].ToString(), Ordinal(stage + 1)]);
        }
    }
}
