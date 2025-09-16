using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SNotNumberPad
{
    [SouvenirQuestion("Which of these numbers {1} at the {2} stage of {0}?", TwoColumns4Answers, TranslateArguments = [true, false], Arguments = ["flashed", QandA.Ordinal, "did not flash", QandA.Ordinal], ArgumentGroupSize = 2)]
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
        var numStrs = Enumerable.Range(0, 10).Select(i => i.ToString()).ToArray();
        for (var stage = 0; stage < 3; stage++)
        {
            if (numbers[stage].Length >= 3)
                yield return question(SNotNumberPad.Flashes, args: ["did not flash", Ordinal(stage + 1)]).Answers(numStrs.Except(numbers[stage]).ToArray());
            yield return question(SNotNumberPad.Flashes, args: ["flashed", Ordinal(stage + 1)]).Answers(numbers[stage]);
        }
    }
}