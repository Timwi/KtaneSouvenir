using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBinaryShift
{
    [Question("What was the {1} initial number in {0}?", ThreeColumns6Answers, ExampleAnswers = ["13", "14", "34", "46", "53", "64", "67", "77", "82", "96"], Arguments = ["top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    InitialNumber
}

public partial class SouvenirModule
{
    [Handler("binary_shift", "Binary Shift", typeof(SBinaryShift), "NickLatkovich")]
    [ManualQuestion("What were the initial numbers?")]
    private IEnumerator<SouvenirInstruction> ProcessBinaryShift(ModuleData module)
    {
        var comp = GetComponent(module, "BinaryShiftModule");
        yield return WaitForSolve;

        var allPositions = new[] { "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right" };
        for (var position = 0; position < 9; position++)
        {
            var initialNumber = GetMethod<int>(comp, "GetInitialNumber", 1, true).Invoke([position]);
            var possibleInitialNumbers = GetProperty<HashSet<int>>(comp, "possibleInitialNumbers", true).Get().Select(n => n.ToString()).ToArray();
            yield return question(SBinaryShift.InitialNumber, args: [allPositions[position]]).Answers(initialNumber.ToString(), preferredWrong: possibleInitialNumbers);
        }
    }
}
