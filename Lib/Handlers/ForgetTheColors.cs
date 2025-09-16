using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SForgetTheColors
{
    // QUESTIONS

    [SouvenirQuestion("What number was on the gear during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QGearNumber,

    [SouvenirQuestion("What number was on the large display during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 990)]
    QLargeDisplay,

    [SouvenirQuestion("What was the last decimal in the sine number received during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QSineNumber,

    [SouvenirQuestion("What color was the gear during stage {1} of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray", TranslateAnswers = true, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    QGearColor,

    [SouvenirQuestion("Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray", TranslateAnswers = true, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    QRuleColor,

    // DISCRIMINATORS

    [SouvenirDiscriminator("the Forget The Colors whose gear number was {0} in stage {1}", Arguments = ["1", "1", "2", "5", "4", "7"], ArgumentGroupSize = 2)]
    DGearNumber,

    [SouvenirDiscriminator("the Forget The Colors which had {0} on its large display in stage {1}", Arguments = ["426", "1", "271", "5", "925", "7"], ArgumentGroupSize = 2)]
    DLargeDisplay,

    [SouvenirDiscriminator("the Forget The Colors whose received sine number in stage {1} ended with a {0}", Arguments = ["0", "1", "1", "5", "2", "7"], ArgumentGroupSize = 2)]
    DSineNumber,

    [SouvenirDiscriminator("the Forget The Colors whose {2} was {0} in stage {1}", Arguments = ["Red", "1", "gear color", "Orange", "1", "gear color", "Yellow", "1", "gear color", "Green", "1", "gear color", "Cyan", "1", "rule color", "Blue", "1", "rule color", "Purple", "1", "rule color", "Pink", "1", "rule color", "Maroon", "1", "rule color", "White", "1", "rule color", "Gray", "rule color"], ArgumentGroupSize = 3, TranslateArguments = [true, false, true])]
    DColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("ForgetTheColors", "Forget The Colors", typeof(SForgetTheColors), "Kuro", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessForgetTheColors(ModuleData module)
    {
        var comp = GetComponent(module, "FTCScript");

        yield return WaitForUnignoredModules;

        var colors = SForgetTheColors.QGearColor.GetAnswers();
        var myGearNumbers = GetListField<byte>(comp, "gear").Get(minLength: 0, validator: v => v is < 0 or > 9 ? "expected 0–9" : null);
        var myLargeDisplays = GetListField<short>(comp, "largeDisplay").Get(minLength: 0, validator: v => v is < 0 or > 990 ? "expected 0–990" : null);
        var mySineNumbers = GetListField<int>(comp, "sineNumber").Get(minLength: 0, validator: v => v is < -99999 or > 99999 ? "expected (-99999)–99999" : null);
        var myGearColors = GetListField<string>(comp, "gearColor").Get(minLength: 0, validator: v => !colors.Contains(v) ? $"expected {colors.JoinString(", ")}" : null);
        var myRuleColors = GetListField<string>(comp, "ruleColor").Get(minLength: 0, validator: v => !colors.Contains(v) ? $"expected {colors.JoinString(", ")}" : null);

        if (myGearColors.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        for (var stage = 0; stage < myGearNumbers.Count; stage++)
        {
            yield return new Discriminator(SForgetTheColors.DGearNumber, $"gearnumber{stage}", myGearNumbers[stage], args: [myGearNumbers[stage].ToString(), stage.ToString()]);
            yield return new Discriminator(SForgetTheColors.DLargeDisplay, $"largedisplay{stage}", myLargeDisplays[stage], args: [myLargeDisplays[stage].ToString(), stage.ToString()]);
            yield return new Discriminator(SForgetTheColors.DSineNumber, $"sinenumber{stage}", Mathf.Abs(mySineNumbers[stage]) % 10, args: [(Mathf.Abs(mySineNumbers[stage]) % 10).ToString(), stage.ToString()]);
            yield return new Discriminator(SForgetTheColors.DColor, $"gearcolor{stage}", myGearColors[stage], args: [myGearColors[stage], stage.ToString(), "gear color"]);
            yield return new Discriminator(SForgetTheColors.DColor, $"rulecolor{stage}", myRuleColors[stage], args: [myRuleColors[stage], stage.ToString(), "rule color"]);

            yield return question(SForgetTheColors.QGearNumber, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DGearNumber).Answers(myGearNumbers[stage].ToString());
            yield return question(SForgetTheColors.QLargeDisplay, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DLargeDisplay).Answers(myLargeDisplays[stage].ToString());
            yield return question(SForgetTheColors.QSineNumber, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DSineNumber).Answers((Mathf.Abs(mySineNumbers[stage]) % 10).ToString());
            yield return question(SForgetTheColors.QGearColor, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DColor).Answers(myGearColors[stage].ToString());
            yield return question(SForgetTheColors.QRuleColor, args: [stage.ToString()]).AvoidDiscriminators(SForgetTheColors.DColor).Answers(myRuleColors[stage].ToString());
        }
    }
}
