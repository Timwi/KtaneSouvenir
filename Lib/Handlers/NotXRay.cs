using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SNotXRay
{
    [Question("What was the scanner color in {0}?", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", TranslateAnswers = true)]
    ScannerColor,

    [Question("What table were we in in {0} (numbered 1–8 in reading order in the manual)?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8")]
    Table,

    [Question("What direction was button {1} in {0}?", TwoColumns4Answers, "Up", "Right", "Down", "Left", TranslateAnswers = true, Arguments = ["1", "2", "3", "4"], ArgumentGroupSize = 1)]
    Directions,

    [Question("Which button went {1} in {0}?", TwoColumns4Answers, "1", "2", "3", "4", TranslateArguments = [true], Arguments = ["up", "right", "down", "left"], ArgumentGroupSize = 1)]
    Buttons
}

public partial class SouvenirModule
{
    [Handler("NotXRayModule", "Not X-Ray", typeof(SNotXRay), "Timwi")]
    [ManualQuestion("What table were we in?")]
    [ManualQuestion("Which button went which direction?")]
    [ManualQuestion("What was the scanner color?")]
    private IEnumerator<SouvenirInstruction> ProcessNotXRay(ModuleData module)
    {
        var comp = GetComponent(module, "NotXRayModule");
        yield return WaitForSolve;

        var table = GetIntField(comp, "_table").Get(0, 7);
        var directions = GetField<Array>(comp, "_directions").Get(validator: arr => arr.Length != 4 ? "expected length 4" : null);
        var allColors = SNotXRay.ScannerColor.GetAnswers();
        var scannerColor = GetField<object>(comp, "_scannerColor").Get(v => v == null ? "did not expected null" : !allColors.Contains(v.ToString()) ? "expected " + allColors.JoinString(", ") : null);

        yield return question(SNotXRay.Table).Answers((table + 1).ToString());
        yield return question(SNotXRay.ScannerColor).Answers(scannerColor.ToString());
        for (var i = 0; i < 4; i++)
        {
            yield return question(SNotXRay.Directions, args: [(i + 1).ToString()]).Answers(directions.GetValue(i).ToString());
            yield return question(SNotXRay.Buttons, args: [directions.GetValue(i).ToString().ToLowerInvariant()]).Answers((i + 1).ToString());
        }
    }
}
