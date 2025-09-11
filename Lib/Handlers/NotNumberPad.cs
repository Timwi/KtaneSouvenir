using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNotNumberPad
{
    [SouvenirQuestion("Which of these numbers {1} at the {2} stage of {0}?", TwoColumns4Answers, TranslateFormatArgs = [true, false], Arguments = ["flashed", QandA.Ordinal, "did not flash", QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    Flashes
}

public partial class SouvenirModule
{
    [SouvenirHandler("notNumberPad", "Not Number Pad", typeof(SNotNumberPad), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotNumberPad(ModuleData module)
    {
        var comp = GetComponent(module, "NotNumberPadScript");
        yield return WaitForSolve;

        var flashes = GetField<IList>(comp, "flashes").Get();
        var mthGetNumbers = GetMethod<int[]>(flashes[0], "GetNumbers", 0, isPublic: true);
        var numbers = Enumerable.Range(0, 3).Select(stage => mthGetNumbers.InvokeOn(flashes[stage]).Select(i => i.ToString()).ToArray()).ToArray();

        var qs = new List<QandA>();
        var numStrs = Enumerable.Range(0, 10).Select(i => i.ToString()).ToArray();
        for (var stage = 0; stage < 3; stage++)
        {
            if (numbers[stage].Length >= 3)
                qs.Add(makeQuestion(Question.NotNumberPadFlashes, module, formatArgs: new[] { "did not flash", Ordinal(stage + 1) }, correctAnswers: numStrs.Except(numbers[stage]).ToArray()));
            qs.Add(makeQuestion(Question.NotNumberPadFlashes, module, formatArgs: new[] { "flashed", Ordinal(stage + 1) }, correctAnswers: numbers[stage]));
        }

        addQuestions(module, qs);
    }
}