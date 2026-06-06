using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SForgetTheColors
{
    // QUESTIONS

    [Question("What number was on the gear during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QGearNumber,

    [Question("What number was on the large display during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 990)]
    QLargeDisplay,

    [Question("What number was on the {2} nixie during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "left", "0", "right", "1", "left", "1", "right", "2", "left", "2", "right", "3", "left", "3", "right", "4", "left", "4", "right", "5", "left", "5", "right", "6", "left", "6", "right", "7", "left", "7", "right", "8", "left", "8", "right", "9", "left", "9", "right"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    [AnswerGenerator.Integers(0, 9)]
    QNixieNumber,

    [Question("What color was on a cylinder during stage {1} of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", TranslateAnswers = true, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    QCylinderColor,

    [Question("What color was the gear during stage {1} of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", TranslateAnswers = true, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    QGearColor,

    // DISCRIMINATORS

    [Discriminator("the Forget The Colors whose gear number was {0} in stage {1}", Arguments = ["1", "1", "2", "5", "4", "7"], ArgumentGroupSize = 2)]
    DGearNumber,

    [Discriminator("the Forget The Colors which had {0} on its large display in stage {1}", Arguments = ["426", "1", "271", "5", "925", "7"], ArgumentGroupSize = 2)]
    DLargeDisplay,

    [Discriminator("the Forget The Colors which had {0} on its {2} nixie in stage {1}", Arguments = ["0", "0", "left", "1", "1", "right"], ArgumentGroupSize = 3, TranslateArguments = [false, false, true])]
    DNixieNumber,

    [Discriminator("the Forget The Colors which had a(n) {0} cylinder in stage {1}", Arguments = ["Red", "1", "Orange", "1", "Yellow", "1", "Green", "1", "Cyan", "1", "Blue", "1", "Purple", "1", "Pink", "1", "Maroon", "1", "White", "1"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DCylinderColor,

    [Discriminator("the Forget The Colors whose gear color was {0} in stage {1}", Arguments = ["Red", "1", "Orange", "1", "Yellow", "1", "Green", "1", "Cyan", "1", "Blue", "1", "Purple", "1", "Pink", "1", "Maroon", "1", "White", "1"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DGearColor
}

public partial class SouvenirModule
{
    [Handler("ForgetTheColors", "Forget The Colors", typeof(SForgetTheColors), "Kuro", IsBossModule = true)]
    [ManualQuestion("What were the large display's, gear's, and nixies' numbers in each stage?")]
    [ManualQuestion("What were the cylinders' and gear's colors in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessForgetTheColors(ModuleData module)
    {
        var comp = GetComponent(module, "FTCScript");

        yield return WaitForUnignoredModules;

        var colors = SForgetTheColors.QGearColor.GetAnswers();
        var myGearNumbers = GetListField<byte>(comp, "gear").Get(minLength: 0, validator: v => v is < 0 or > 9 ? "expected 0–9" : null);
        var myLargeDisplays = GetListField<short>(comp, "largeDisplay").Get(minLength: 0, validator: v => v is < 0 or > 990 ? "expected 0–990" : null);
        var myNixieNumbers = GetListField<byte>(comp, "_nixies").Get(minLength: 0, validator: v => v is < 0 or > 9 ? "expected 0–9" : null);
        var myCylinderColors = GetListField<byte>(comp, "_cylinder").Get(minLength: 0, validator: v => v is < 0 or > 9 ? "expected 0–9" : null);
        var myGearColors = GetListField<string>(comp, "gearColor").Get(minLength: 0, validator: v => !colors.Contains(v) ? $"expected {colors.JoinString(", ")}" : null);

        if (myGearColors.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        for (var stage = 0; stage < myGearNumbers.Count; stage++)
        {
            yield return new Discriminator(SForgetTheColors.DGearNumber, $"gearnumber{stage}", myGearNumbers[stage], args: [myGearNumbers[stage].ToString(), stage.ToString()]);
            yield return new Discriminator(SForgetTheColors.DLargeDisplay, $"largedisplay{stage}", myLargeDisplays[stage], args: [myLargeDisplays[stage].ToString(), stage.ToString()]);
            yield return new Discriminator(SForgetTheColors.DNixieNumber, $"leftnixie{stage}", myNixieNumbers[2 * stage], args: [myNixieNumbers[2 * stage].ToString(), stage.ToString(), "left"]);
            yield return new Discriminator(SForgetTheColors.DNixieNumber, $"rightnixie{stage}", myNixieNumbers[2 * stage + 1], args: [myNixieNumbers[2 * stage + 1].ToString(), stage.ToString(), "right"]);
            yield return new Discriminator(SForgetTheColors.DCylinderColor, $"leftcylinder{stage}", myCylinderColors[4 * stage], args: [colors[myCylinderColors[4 * stage]], stage.ToString()]);
            yield return new Discriminator(SForgetTheColors.DCylinderColor, $"middlecylinder{stage}", myCylinderColors[4 * stage + 1], args: [colors[myCylinderColors[4 * stage + 1]], stage.ToString()]);
            yield return new Discriminator(SForgetTheColors.DCylinderColor, $"rightcylinder{stage}", myCylinderColors[4 * stage + 2], args: [colors[myCylinderColors[4 * stage + 2]], stage.ToString()]);
            yield return new Discriminator(SForgetTheColors.DGearColor, $"gearcolor{stage}", myGearColors[stage], args: [myGearColors[stage], stage.ToString()]);

            yield return question(SForgetTheColors.QGearNumber, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DGearNumber).Answers(myGearNumbers[stage].ToString());
            yield return question(SForgetTheColors.QLargeDisplay, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DLargeDisplay).Answers(myLargeDisplays[stage].ToString());
            yield return question(SForgetTheColors.QNixieNumber, args: [stage.ToString(), "left"]).AvoidDiscriminators(SForgetTheColors.DNixieNumber).Answers(myNixieNumbers[2 * stage].ToString());
            yield return question(SForgetTheColors.QNixieNumber, args: [stage.ToString(), "right"]).AvoidDiscriminators(SForgetTheColors.DNixieNumber).Answers(myNixieNumbers[2 * stage + 1].ToString());
            yield return question(SForgetTheColors.QCylinderColor, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DCylinderColor).Answers([colors[myCylinderColors[4 * stage]], colors[myCylinderColors[4 * stage + 1]], colors[myCylinderColors[4 * stage + 2]]]);
            yield return question(SForgetTheColors.QGearColor, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DGearColor).Answers(myGearColors[stage].ToString());
        }
    }
}
