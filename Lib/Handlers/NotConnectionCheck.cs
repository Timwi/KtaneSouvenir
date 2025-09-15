using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNotConnectionCheck
{
    [SouvenirQuestion("What symbol flashed on the {1} button in {0}?", ThreeColumns6Answers, "+", "-", ".", ":", "/", "_", "=", ",", Arguments = ["top left", "top right", "bottom left", "bottom right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Flashes,
    
    [SouvenirQuestion("What was the value of the {1} button in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", Arguments = ["top left", "top right", "bottom left", "bottom right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Values
}

public partial class SouvenirModule
{
    [SouvenirHandler("notConnectionCheck", "Not Connection Check", typeof(SNotConnectionCheck), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotConnectionCheck(ModuleData module)
    {
        var comp = GetComponent(module, "NCCScript");
        yield return WaitForSolve;
        var qs = new List<QandA>();
        var positions = new[] { "top left", "top right", "bottom left", "bottom right" };

        // Flashes
        var ops = GetArrayField<int>(comp, "ops").Get();
        var puncMarkNames = new[] { "+", "-", ".", ":", "/", "_", "=", "," };
        var puncMarks = Enumerable.Range(0, ops.Length).Select(i => puncMarkNames[ops[i]]).ToArray();
        for (var p = 0; p < 4; p++)
            qs.Add(makeQuestion(Question.NotConnectionCheckFlashes, module, formatArgs: new[] { positions[p] }, correctAnswers: new[] { puncMarks[p] }));

        // Values
        var outputs = GetArrayField<int>(comp, "outputs").Get();
        var vals = Enumerable.Range(0, outputs.Length).Select(i => outputs[i].ToString()).ToArray();
        for (var p = 0; p < 4; p++)
            qs.Add(makeQuestion(Question.NotConnectionCheckValues, module, formatArgs: new[] { positions[p] }, correctAnswers: new[] { vals[p] }, preferredWrongAnswers: Enumerable.Range(1, 9).Select(i => i.ToString()).ToArray()));

        addQuestions(module, qs);
    }
}