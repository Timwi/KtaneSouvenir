using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SLogicalButtons
{
    [SouvenirQuestion("What was the color of the {1} button in the {2} stage of {0}?", TwoColumns4Answers, "Red", "Blue", "Green", "Yellow", "Purple", "White", "Orange", "Cyan", "Grey", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["top", QandA.Ordinal, "bottom-left", QandA.Ordinal, "bottom-right", QandA.Ordinal], ArgumentGroupSize = 2)]
    Color,

    [SouvenirQuestion("What was the label on the {1} button in the {2} stage of {0}?", TwoColumns4Answers, "Logic", "Color", "Label", "Button", "Wrong", "Boom", "No", "Wait", "Hmmm", TranslateArguments = [true, false], Arguments = ["top", QandA.Ordinal, "bottom-left", QandA.Ordinal, "bottom-right", QandA.Ordinal], ArgumentGroupSize = 2)]
    Label,

    [SouvenirQuestion("What was the final operator in the {1} stage of {0}?", ThreeColumns6Answers, "AND", "OR", "XOR", "NAND", "NOR", "XNOR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Operator
}

public partial class SouvenirModule
{
    [SouvenirHandler("logicalButtonsModule", "Logical Buttons", typeof(SLogicalButtons), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessLogicalButtons(ModuleData module)
    {
        var comp = GetComponent(module, "LogicalButtonsScript");
        var fldStage = GetIntField(comp, "stage");
        var fldButtons = GetField<Array>(comp, "buttons");
        var fldGateOperator = GetField<object>(comp, "gateOperator");

        yield return WaitForActivate;

        var curStage = 0;
        var colors = new string[3][];
        var labels = new string[3][];
        var initialOperators = new string[3];

        FieldInfo<string> fldLabel = null;
        FieldInfo<object> fldColor = null;
        FieldInfo<int> fldIndex = null;
        MethodInfo<string> mthGetName = null;

        while (module.Unsolved)
        {
            var buttons = fldButtons.Get(ar => ar.Length != 3 ? "expected length 3" : null);
            var infs = buttons.Cast<object>().Select(obj =>
            {
                fldLabel ??= GetField<string>(obj, "<Label>k__BackingField");
                fldColor ??= GetField<object>(obj, "<Color>k__BackingField");
                fldIndex ??= GetIntField(obj, "<Index>k__BackingField");
                return fldLabel == null || fldColor == null || fldIndex == null
                    ? null
                    : new { Label = fldLabel.GetFrom(obj), Color = fldColor.GetFrom(obj), Index = fldIndex.GetFrom(obj) };
            }).ToArray();
            if (infs.Length != 3 || infs.Any(inf => inf == null || inf.Label == null || inf.Color == null) || infs[0].Index != 0 || infs[1].Index != 1 || infs[2].Index != 2)
                throw new AbandonModuleException($"I got an unexpected value ([{infs.Select(inf => inf == null ? "<null>" : inf.ToString()).JoinString(", ")}]).");

            var gateOperator = fldGateOperator.Get();
            if (mthGetName == null)
            {
                var interfaceType = gateOperator.GetType().Assembly.GetType("ILogicalGateOperator") ?? throw new AbandonModuleException("Interface type ILogicalGateOperator not found.");
                var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                var mths = interfaceType.GetMethods(bindingFlags).Where(m => m.Name == "get_Name" && m.GetParameters().Length == 0 && typeof(string).IsAssignableFrom(m.ReturnType)).Take(2).ToArray();
                if (mths.Length == 0)
                    throw new AbandonModuleException($"Type {interfaceType} does not contain public method {name} with return type string and 0 parameters.");
                if (mths.Length > 1)
                    throw new AbandonModuleException($"Type {interfaceType} contains multiple public methods {name} with return type string and 0 parameters.");
                mthGetName = new MethodInfo<string>(null, mths[0]);
            }

            var clrs = infs.Select(inf => inf.Color.ToString()).ToArray();
            var lbls = infs.Select(inf => inf.Label).ToArray();
            var iOp = mthGetName.InvokeOn(gateOperator);

            var stage = fldStage.Get();
            if (stage != curStage || !clrs.SequenceEqual(colors[stage - 1]) || !lbls.SequenceEqual(labels[stage - 1]) || iOp != initialOperators[stage - 1])
            {
                if (stage != curStage && stage != curStage + 1)
                    throw new AbandonModuleException($"I must have missed a stage (it went from {curStage} to {stage}).");
                if (stage is < 1 or > 3)
                    throw new AbandonModuleException($"‘stage’ has unexpected value {stage} (expected 1–3).");

                colors[stage - 1] = clrs;
                labels[stage - 1] = lbls;
                initialOperators[stage - 1] = iOp;
                curStage = stage;
            }

            yield return new WaitForSeconds(.1f);
        }

        if (initialOperators.Any(io => io == null))
            throw new AbandonModuleException($"There is a null initial operator ([{initialOperators.Select(io => io == null ? "<null>" : $"“{io}”").JoinString(", ")}]).");

        var _logicalButtonsButtonNames = new[] { "top", "bottom-left", "bottom-right" };
        for (var stage = 0; stage < colors.Length; stage++)
            for (var btnIx = 0; btnIx < colors[stage].Length; btnIx++)
                yield return question(SLogicalButtons.Color, args: [_logicalButtonsButtonNames[btnIx], Ordinal(stage + 1)]).Answers(colors[stage][btnIx]);
        for (var stage = 0; stage < labels.Length; stage++)
            for (var btnIx = 0; btnIx < labels[stage].Length; btnIx++)
                yield return question(SLogicalButtons.Label, args: [_logicalButtonsButtonNames[btnIx], Ordinal(stage + 1)]).Answers(labels[stage][btnIx]);
        for (var stage = 0; stage < initialOperators.Length; stage++)
            yield return question(SLogicalButtons.Operator, args: [Ordinal(stage + 1)]).Answers(initialOperators[stage]);
    }
}
