using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SForgetAnyColor
{
    // QUESTIONS

    [Question("What colors were the cylinders during the {1} stage of {0}?", OneColumn4Answers, ExampleAnswers = ["Orange, Yellow, Green", "Yellow, Cyan, Purple", "Green, Purple, Orange", "Green, Blue, Purple"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["{0}, {1}, {2}", "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White", "L", "M", "R"])]
    QCylinder,

    [Question("What color was the gear during the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QGearColor,

    [Question("What number was on the gear during the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QGearNumber,

    [Question("What number was on the large display during the {1} stage of {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 999999, "000000")]
    QLargeDisplay,

    [Question("What number was on the {2} nixie during the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, "left", QandA.Ordinal, "right"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    [AnswerGenerator.Integers(0, 9)]
    QNixieNumber,

    // DISCRIMINATORS

    [Discriminator("the Forget Any Color whose cylinders in the {1} stage were {0}", Arguments = ["Orange, Yellow, Green", QandA.Ordinal, "Yellow, Cyan, Purple", QandA.Ordinal], ArgumentGroupSize = 2)]
    DCylinder,

    [Discriminator("the Forget Any Color whose gear color in the {1} stage was {0}", Arguments = ["red", QandA.Ordinal, "orange", QandA.Ordinal, "yellow", QandA.Ordinal, "green", QandA.Ordinal, "cyan", QandA.Ordinal, "blue", QandA.Ordinal, "purple", QandA.Ordinal, "white", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DGearColor,

    [Discriminator("the Forget Any Color whose gear number in the {1} stage was {0}", Arguments = ["0", QandA.Ordinal, "1", QandA.Ordinal, "2", QandA.Ordinal, "3", QandA.Ordinal, "4", QandA.Ordinal, "5", QandA.Ordinal, "6", QandA.Ordinal, "7", QandA.Ordinal, "8", QandA.Ordinal, "9", QandA.Ordinal], ArgumentGroupSize = 2)]
    DGearNumber,

    [Discriminator("the Forget Any Color which had {0} on its large display in the {1} stage", Arguments = ["1", QandA.Ordinal, "22", QandA.Ordinal, "333", QandA.Ordinal, "4444", QandA.Ordinal, "55555", QandA.Ordinal, "666666", QandA.Ordinal], ArgumentGroupSize = 2)]
    DLargeDisplay,

    [Discriminator("the Forget Any Color which had {0} on its {2} nixie the {1} stage", Arguments = ["0", QandA.Ordinal, "left", "1", QandA.Ordinal, "right"], ArgumentGroupSize = 3, TranslateArguments = [false, false, true])]
    DNixieNumber,
}

public partial class SouvenirModule
{
    [Handler("ForgetAnyColor", "Forget Any Color", typeof(SForgetAnyColor), "Espik", IsBossModule = true)]
    [ManualQuestion("What were the large display’s, gear’s, and nixies’ numbers in each stage?")]
    [ManualQuestion("What were the cylinders’ and gear’s colors in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessForgetAnyColor(ModuleData module)
    {
        var comp = GetComponent(module, "FACScript");
        var init = GetField<object>(comp, "init").Get();
        var coroutine = GetField<object>(init, "coroutine").Get();

        var fldStage = GetIntField(init, "stage");
        var fldAnimatingStage = GetField<bool>(coroutine, "animating");
        var foundStage = -1;

        var fldCurrentStage = GetIntField(init, "currentStage");
        var fldModulesPerStage = GetStaticField<int>(init.GetType(), "modulesPerStage");
        var fldFinalStage = GetIntField(init, "finalStage");
        
        var fldCylinders = GetField<Array>(init, "cylinders");
        var fldDisplayText = GetField<TextMesh>(comp, "DisplayText", isPublic: true);
        var fldGearText = GetField<TextMesh>(comp, "GearText", isPublic: true);
        var fldNixieTexts = GetArrayField<TextMesh>(comp, "NixieTexts", isPublic: true);

        yield return WaitForActivate;
        yield return null; // Wait one extra frame to ensure that maxStage has been set.

        if (GetIntField(init, "maxStage").Get() == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        var myDisplayTexts = new List<string>();
        var myGearTexts = new List<string>();
        var myGearColors = new List<string>();
        var myLeftNixieTexts = new List<string>();
        var myRightNixieTexts = new List<string>();

        // Acts as both WaitForUnignoredModules and WaitForSolve. Forget Any Color has a chance to solve before all its unignored modules.
        while (!_noUnignoredModulesLeft && module.Unsolved)
        {
            if (foundStage != fldStage.Get())
            {
                foundStage = fldStage.Get();
                if (!(fldCurrentStage.Get() / fldModulesPerStage.Get() == fldFinalStage.Get() / fldModulesPerStage.Get()))
                {
                    while (fldAnimatingStage.Get())
                        yield return null;

                    var foundNixieTexts = fldNixieTexts.Get(expectedLength: 2);

                    myDisplayTexts.Add(fldDisplayText.Get().text);
                    myGearTexts.Add(fldGearText.Get().text.Last().ToString());
                    myGearColors.Add(fldGearText.Get().text.First().ToString());
                    myLeftNixieTexts.Add(foundNixieTexts[0].text);
                    myRightNixieTexts.Add(foundNixieTexts[1].text);
                }
            }
            
            yield return null;
        }

        var totalStages = myDisplayTexts.Count();

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White" }
            .Select(str => TranslateQuestionString(SForgetAnyColor.QCylinder, str)).ToArray();
        var cylinderFormatter = TranslateQuestionString(SForgetAnyColor.QCylinder, "{0}, {1}, {2}");

        var myCylinders = fldCylinders.Get(v =>
        {
            if (v.Rank != 2 || v.GetLength(1) != 3)
                return $"expected an N×3 2D array";
            for (var i = 0; i < v.GetLength(0); i++)
                for (var j = 0; j < v.GetLength(1); j++)
                    if ((int) v.GetValue(i, j) is int w && (w < 0 || w >= colorNames.Length))
                        return $"index [{i}, {j}] is {w}, expected 0–{colorNames.Length - 1}";
            return null;
        });

        string getCylinders(Array cylinders, int stage) => string.Format(cylinderFormatter,
            Enumerable.Range(0, 3).Select(ix => colorNames[(int) cylinders.GetValue(stage, ix)]).ToArray());

        var preferredCylinders = new HashSet<string>();
        while (preferredCylinders.Count < 7)
            preferredCylinders.Add(string.Format(cylinderFormatter, colorNames.PickRandom(), colorNames.PickRandom(), colorNames.PickRandom()));

        string getColorName(string letter)
        {
            return letter switch
            {
                "R" => "Red",
                "O" => "Orange",
                "Y" => "Yellow",
                "G" => "Green",
                "C" => "Cyan",
                "B" => "Blue",
                "P" => "Purple",
                "W" => "White",
                _ => "Gray",
            };
        }

        for (var stage = 0; stage < totalStages; stage++)
        {
            //Debug.Log($"Stage={stage}, myCylinders={myCylinders.GetLength(0)}, {myCylinders.GetLength(1)}");
            var correctCylinders = getCylinders(myCylinders, stage);

            yield return question(SForgetAnyColor.QCylinder, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"cylinder-{stage}").Answers(correctCylinders, preferredWrong: preferredCylinders.ToArray());
            yield return question(SForgetAnyColor.QGearColor, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"gearcolor-{stage}").Answers(getColorName(myGearColors[stage]));
            yield return question(SForgetAnyColor.QGearNumber, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"gearnumber-{stage}").Answers(myGearTexts[stage]);
            yield return question(SForgetAnyColor.QLargeDisplay, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"largedisplay-{stage}").Answers(myDisplayTexts[stage]);
            yield return question(SForgetAnyColor.QNixieNumber, args: [Ordinal(stage + 1), "left"]).AvoidDiscriminators($"leftnixie-{stage}").Answers(myLeftNixieTexts[stage]);
            yield return question(SForgetAnyColor.QNixieNumber, args: [Ordinal(stage + 1), "right"]).AvoidDiscriminators($"rightnixie-{stage}").Answers(myRightNixieTexts[stage]);

            yield return new Discriminator(SForgetAnyColor.DCylinder, $"cylinder-{stage}", correctCylinders, args: [correctCylinders, Ordinal(stage + 1)]);
            yield return new Discriminator(SForgetAnyColor.DGearColor, $"gearcolor-{stage}", myGearColors[stage], args: [getColorName(myGearColors[stage]).ToLowerInvariant(), Ordinal(stage + 1)]);
            yield return new Discriminator(SForgetAnyColor.DGearNumber, $"gearnumber-{stage}", myGearTexts[stage], args: [myGearTexts[stage], Ordinal(stage + 1)]);
            yield return new Discriminator(SForgetAnyColor.DLargeDisplay, $"largedisplay-{stage}", myDisplayTexts[stage], args: [myDisplayTexts[stage], Ordinal(stage + 1)]);
            yield return new Discriminator(SForgetAnyColor.DNixieNumber, $"leftnixie-{stage}", myLeftNixieTexts[stage], args: [myLeftNixieTexts[stage], Ordinal(stage + 1), "left"]);
            yield return new Discriminator(SForgetAnyColor.DNixieNumber, $"rightnixie-{stage}", myRightNixieTexts[stage], args: [myRightNixieTexts[stage], Ordinal(stage + 1), "right"]);
        }
    }
}
