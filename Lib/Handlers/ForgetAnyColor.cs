using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SForgetAnyColor
{
    [SouvenirQuestion("What colors were the cylinders during the {1} stage of {0}?", OneColumn4Answers, ExampleAnswers = ["Orange, Yellow, Green", "Yellow, Cyan, Purple", "Green, Purple, Orange", "Green, Blue, Purple"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["{0}, {1}, {2}", "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White", "L", "M", "R"])]
    Cylinder,

    [SouvenirQuestion("Which figure was used during the {1} stage of {0}?", ThreeColumns6Answers, ExampleAnswers = ["LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Sequence
}

public partial class SouvenirModule
{
    [SouvenirHandler("ForgetAnyColor", "Forget Any Color", typeof(SForgetAnyColor), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessForgetAnyColor(ModuleData module)
    {
        var comp = GetComponent(module, "FACScript");
        const string moduleId = "ForgetAnyColor";

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
        _facCylinders.Add(myCylinders);
        _facFigures.Add(myFigures);

        yield return WaitForUnignoredModules;

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White" }
            .Select(str => translateString(SForgetAnyColor.Cylinder, str)).ToArray();
        var figureNames = new[] { "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR" }
            .Select(str => str.Select(ch => translateString(SForgetAnyColor.Cylinder, ch.ToString())).JoinString()).ToArray();

        string getCylinders(Array cylinders, int stage) => string.Format(
            translateString(SForgetAnyColor.Cylinder, "{0}, {1}, {2}"),
            Enumerable.Range(0, 3).Select(ix => colorNames[(int) cylinders.GetValue(stage, ix)]).ToArray());

        var randomStage = Rnd.Range(0, fldCurrentStage.Get(min: 0, max: maxStage));
        string formattedName = null;
        if (_moduleCounts[moduleId] > 1)
        {
            for (var stage = 0; stage < maxStage; stage++)
            {
                if (stage == randomStage)
                    continue;
                var cylindersThisStage = getCylinders(myCylinders, stage);
                var formatCandidates = new List<string>();
                if (_facFigures.Count(f => f[stage] == myFigures[stage]) == 1)
                    formatCandidates.Add(string.Format(
                        translateString(SForgetAnyColor.Cylinder, "the Forget Any Color which used figure {0} in the {1} stage"),
                        figureNames[myFigures[stage]],
                        Ordinal(stage + 1)));
                if (_facCylinders.Count(c => getCylinders(c, stage) == cylindersThisStage) == 1)
                    formatCandidates.Add(string.Format(
                        translateString(SForgetAnyColor.Cylinder, "the Forget Any Color whose cylinders in the {1} stage were {0}"),
                        cylindersThisStage,
                        Ordinal(stage + 1)));
                if (formatCandidates.Count > 0)
                {
                    formattedName = formatCandidates.PickRandom();
                    break;
                }
            }
            if (formattedName == null)
                yield return legitimatelyNoQuestion(module, $"There were not enough stages where this one (#{GetIntField(init, "moduleId").Get()}) was unique.");
        }

        formattedName ??= _translation?.Translate(SForgetAnyColor.Cylinder).ModuleName ?? "Forget Any Color";
        var correctCylinders = getCylinders(myCylinders, randomStage);
        var preferredCylinders = new HashSet<string> { correctCylinders };
        while (preferredCylinders.Count < 6)
            preferredCylinders.Add(string.Format(translateString(SForgetAnyColor.Cylinder, "{0}, {1}, {2}"),
                Enumerable.Range(0, 3).Select(i => colorNames.PickRandom()).ToArray()));

        yield return question(SForgetAnyColor.Cylinder, args: [Ordinal(randomStage + 1)]).Answers(correctCylinders, preferredWrong: preferredCylinders.ToArray());
        yield return question(SForgetAnyColor.Sequence, args: [Ordinal(randomStage + 1)]).Answers(figureNames[myFigures[randomStage]], all: figureNames);
    }
}
