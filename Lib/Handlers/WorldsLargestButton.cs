using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SWorldsLargestButton
{
    [Question("What label was on {0}?", TwoColumns4Answers, "Hold", "Press", "Tap", "Release", "Abort", "Detonate", "Run", "Large", "Button", "Big", "Listen", "Thick", "Literally Blank", "Explode", "Strike", "Solve", "\u00a0", "Blank", "Something", "World")]
    QLabel,

    [Question("What color was {0} before it was held for step 1?", ThreeColumns6Answers, "Blue", "Yellow", "Magenta", "Purple", "Cyan", "White", "Gray", "Brown", TranslateAnswers = true)]
    QColorBefore,

    [Question("What color was {0} while it was held for step 1?", ThreeColumns6Answers, "Blue", "Yellow", "Magenta", "Purple", "Cyan", "White", "Gray", "Brown", TranslateAnswers = true)]
    QColorWhile,

    [Question("Which color was among the colors {0} flashed while it was held for step 1?", ThreeColumns6Answers, "Blue", "Yellow", "Magenta", "Purple", "Cyan", "White", "Gray", "Brown", TranslateAnswers = true)]
    QColorsWhile,

    [Discriminator("the World’s Largest Button that said “{0}”", Arguments = ["Hold", "Press", "Tap", "Release", "Abort", "Detonate", "Run", "Large", "Button", "Big", "Listen", "Thick", "Literally Blank", "Explode", "Strike", "Solve", " ", "Blank", "Something", "World"], ArgumentGroupSize = 1)]
    DLabel,

    [Discriminator("the World’s Largest Button whose color was {0} before it was held for step 1", Arguments = ["Blue", "Yellow", "Magenta", "Purple", "Cyan", "White", "Gray", "Brown"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DColorBefore,

    [Discriminator("the World’s Largest Button that was {0} while it was held for step 1", Arguments = ["blue", "yellow", "magenta", "purple", "cyan", "white", "gray", "brown"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DColorWhile,

    [Discriminator("the World’s Largest Button that flashed {0} while it was held for step 1", Arguments = ["blue", "yellow", "magenta", "purple", "cyan", "white", "gray", "brown"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DColorsWhile
}

public partial class SouvenirModule
{
    [Handler("WorldsLargestButton", "World’s Largest Button", typeof(SWorldsLargestButton), "Timwi", AddThe = true)]
    [ManualQuestion("What color(s) was the button before and while it was held for step 1?")]
    [ManualQuestion("What label was on the button?")]
    private IEnumerator<SouvenirInstruction> ProcessWorldsLargestButton(ModuleData module)
    {
        var comp = GetComponent(module, "WorldsLargestButton");

        var button = GetField<KMSelectable>(comp, "Button", isPublic: true).Get();
        var fldCanHoldButton = GetField<bool>(comp, "canHoldButton");
        var fldIsUnicorn = GetField<bool>(comp, "isUnicorn");
        var fldStagesCompleted = GetField<int>(comp, "stagesCompleted");
        var fldColorIndex = GetField<int>(comp, "colorIndex");
        var fldNewColorIndex = GetField<int>(comp, "newColorIndex");
        var fldTwoColorsFlash = GetField<bool>(comp, "twoColorsFlash");
        var fldNewColor2Index = GetField<int>(comp, "newColor2Index");
        var fldWasAlertMode = GetField<bool>(comp, "wasAlertMode");

        int colorIndex = 0, newColorIndex = 0, newColor2Index = 0;
        bool twoColorsFlash = false, wasAlertMode = false;

        var oldInteract = button.OnInteract;
        button.OnInteract = delegate
        {
            var valid = fldCanHoldButton.Get() && fldStagesCompleted.Get() == 0 && !fldIsUnicorn.Get();

            // Get the button’s color _before_ holding it
            if (valid)
                colorIndex = fldColorIndex.Get();

            var ret = oldInteract();

            // Get the flashing color(s) _while_ holding it
            if (valid)
            {
                newColorIndex = fldNewColorIndex.Get();
                newColor2Index = fldNewColor2Index.Get();
                twoColorsFlash = fldTwoColorsFlash.Get();
                wasAlertMode = fldWasAlertMode.Get();
            }

            return ret;
        };

        var label = GetField<string>(comp, "buttonText").Get();
        if (label == " ")
            label = "\u00a0";   // non-breaking space: makes sure that the blank label doesn’t word-wrap
        var buttonTexts = GetArrayField<TextMesh>(comp, "ButtonText", isPublic: true).Get(expectedLength: 2);

        yield return WaitForSolve;

        foreach (var buttonText in buttonTexts)
            buttonText.text = "";

        if (fldIsUnicorn.Get())
            yield return legitimatelyNoQuestion(module, "the “unicorn rule” (first rule in the manual) applied.");

        var colorNames = SWorldsLargestButton.QColorBefore.GetAnswers();
        yield return new Discriminator(SWorldsLargestButton.DLabel, "label", label, args: [label]);
        yield return question(SWorldsLargestButton.QLabel).AvoidDiscriminators(SWorldsLargestButton.DLabel).Answers(label);
        yield return new Discriminator(SWorldsLargestButton.DColorBefore, "cb", colorIndex, args: [colorNames[colorIndex]]);
        yield return question(SWorldsLargestButton.QColorBefore).AvoidDiscriminators(SWorldsLargestButton.DColorBefore).Answers(colorNames[colorIndex]);
        if (!wasAlertMode && !twoColorsFlash)
        {
            yield return question(SWorldsLargestButton.QColorWhile).AvoidDiscriminators(SWorldsLargestButton.DColorWhile).Answers(colorNames[newColorIndex]);
            yield return new Discriminator(SWorldsLargestButton.DColorWhile, $"ca-{newColorIndex}", args: [colorNames[newColorIndex]]);
        }
        else if (!wasAlertMode && twoColorsFlash)
        {
            yield return question(SWorldsLargestButton.QColorsWhile).AvoidDiscriminators(SWorldsLargestButton.DColorsWhile).Answers([colorNames[newColorIndex], colorNames[newColor2Index]]);
            yield return new Discriminator(SWorldsLargestButton.DColorsWhile, $"ca2-{newColorIndex}", args: [colorNames[newColorIndex].ToLowerInvariant()]);
            yield return new Discriminator(SWorldsLargestButton.DColorsWhile, $"ca2-{newColor2Index}", args: [colorNames[newColor2Index].ToLowerInvariant()]);
        }
    }
}
