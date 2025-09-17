using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SForgetAnyColor
{
    [SouvenirQuestion("What colors were the cylinders during the {1} stage of {0}?", OneColumn4Answers, ExampleAnswers = ["Orange, Yellow, Green", "Yellow, Cyan, Purple", "Green, Purple, Orange", "Green, Blue, Purple"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["{0}, {1}, {2}", "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White", "L", "M", "R"])]
    QCylinder,

    [SouvenirQuestion("Which figure was used during the {1} stage of {0}?", ThreeColumns6Answers, ExampleAnswers = ["LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QFigure,

    [SouvenirDiscriminator("the Forget Any Color whose cylinders in the {1} stage were {0}", Arguments = ["Orange, Yellow, Green", QandA.Ordinal, "Yellow, Cyan, Purple", QandA.Ordinal], ArgumentGroupSize = 2)]
    DCylinder,

    [SouvenirDiscriminator("the Forget Any Color which used figure {0} in the {1} stage", Arguments = ["LLLMR", QandA.Ordinal, "LMMMR", QandA.Ordinal, "LMRRR", QandA.Ordinal, "LMMRR", QandA.Ordinal, "LLMRR", QandA.Ordinal, "LLMMR", QandA.Ordinal], ArgumentGroupSize = 2)]
    DFigure
}

public partial class SouvenirModule
{
    [SouvenirHandler("ForgetAnyColor", "Forget Any Color", typeof(SForgetAnyColor), "Kuro", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessForgetAnyColor(ModuleData module)
    {
        var comp = GetComponent(module, "FACScript");

        var init = GetField<object>(comp, "init").Get();
        var fldCurrentStage = GetIntField(init, "stage");
        var fldCylinders = GetField<Array>(init, "cylinders");
        var calculate = GetField<object>(init, "calculate").Get();
        var fldFigures = GetListField<int>(calculate, "figureSequences");

        var activated = false;
        module.Module.OnActivate += () => activated = true;
        while (!activated)
            yield return null;
        yield return null; // Wait one extra frame to ensure that maxStage has been set.

        var maxStage = GetIntField(init, "maxStage").Get(min: 0);
        if (maxStage == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        var myCylinders = fldCylinders.Get(v => v.Rank != 2 || v.GetLength(0) != maxStage + 1 || v.GetLength(1) != 3 ? $"expected a {maxStage + 1}×3 2D array" : null);
        var myFigures = fldFigures.Get();

        yield return WaitForUnignoredModules;

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White" }
            .Select(str => TranslateQuestionString(SForgetAnyColor.QCylinder, str)).ToArray();
        var figureNames = new[] { "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR" }
            .Select(str => str.Select(ch => TranslateQuestionString(SForgetAnyColor.QCylinder, ch.ToString())).JoinString()).ToArray();
        var cylinderFormatter = TranslateQuestionString(SForgetAnyColor.QCylinder, "{0}, {1}, {2}");

        string getCylinders(Array cylinders, int stage) => string.Format(cylinderFormatter,
            Enumerable.Range(0, 3).Select(ix => colorNames[(int) cylinders.GetValue(stage, ix)]).ToArray());

        var preferredCylinders = new HashSet<string>();
        while (preferredCylinders.Count < 7)
            preferredCylinders.Add(string.Format(cylinderFormatter, colorNames.PickRandom(), colorNames.PickRandom(), colorNames.PickRandom()));

        for (var stage = 0; stage < myCylinders.Length; stage++)
        {
            var correctCylinders = getCylinders(myCylinders, stage);
            var correctFigure = figureNames[myFigures[stage]];

            yield return question(SForgetAnyColor.QCylinder, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"cylinder-{stage}").Answers(correctCylinders, preferredWrong: preferredCylinders.ToArray());
            yield return question(SForgetAnyColor.QFigure, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"figure-{stage}").Answers(correctFigure, all: figureNames);
            yield return new Discriminator(SForgetAnyColor.DCylinder, $"cylinder-{stage}", correctCylinders, args: [correctCylinders, Ordinal(stage + 1)]);
            yield return new Discriminator(SForgetAnyColor.DFigure, $"figure-{stage}", correctFigure, args: [correctFigure, Ordinal(stage + 1)]);
        }
    }
}
