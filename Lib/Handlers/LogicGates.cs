using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLogicGates
{
    [SouvenirQuestion("What was {1} in {0}?", ThreeColumns6Answers, "AND", "OR", "XOR", "NAND", "NOR", "XNOR", TranslateArguments = [true], Arguments = ["gate A", "gate B", "gate C", "gate D", "gate E", "gate F", "gate G", "the duplicated gate"], ArgumentGroupSize = 1)]
    Gates
}

public partial class SouvenirModule
{
    [SouvenirHandler("logicGates", "Logic Gates", typeof(SLogicGates), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessLogicGates(ModuleData module)
    {
        var comp = GetComponent(module, "LogicGates");
        var gates = GetField<IList>(comp, "_gates").Get(lst => lst.Count == 0 ? "empty" : null);
        var btnNext = GetField<KMSelectable>(comp, "ButtonNext", isPublic: true).Get();
        var btnPrevious = GetField<KMSelectable>(comp, "ButtonPrevious", isPublic: true).Get();
        var tmpGateType = GetField<object>(gates[0], "GateType", isPublic: true).Get();
        var fldGateTypeName = GetField<string>(tmpGateType, "Name", isPublic: true);

        var gateTypeNames = gates.Cast<object>().Select(obj => fldGateTypeName.GetFrom(GetField<object>(gates[0], "GateType", isPublic: true).GetFrom(obj)).ToString()).ToArray();
        string duplicate = null;
        var isDuplicateInvalid = false;
        for (var i = 0; i < gateTypeNames.Length; i++)
            for (var j = i + 1; j < gateTypeNames.Length; j++)
                if (gateTypeNames[i] == gateTypeNames[j])
                {
                    if (duplicate != null)
                        isDuplicateInvalid = true;
                    else
                        duplicate = gateTypeNames[i];
                }

        yield return WaitForSolve;

        btnNext.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btnNext.transform);
            btnNext.AddInteractionPunch(0.2f);
            return false;
        };
        btnPrevious.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btnPrevious.transform);
            btnPrevious.AddInteractionPunch(0.2f);
            return false;
        };

        var qs = new List<QandA>();
        for (var i = 0; i < gateTypeNames.Length; i++)
            qs.Add(makeQuestion(Question.LogicGatesGates, module, formatArgs: new[] { "gate " + (char) ('A' + i) }, correctAnswers: new[] { gateTypeNames[i] }));
        if (!isDuplicateInvalid)
            qs.Add(makeQuestion(Question.LogicGatesGates, module, formatArgs: new[] { "the duplicated gate" }, correctAnswers: new[] { duplicate }));
        addQuestions(module, qs);
    }
}