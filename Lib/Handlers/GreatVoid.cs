using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGreatVoid
{
    [SouvenirQuestion("What was the {1} digit in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Digit,

    [SouvenirQuestion("What was the {1} color in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("greatVoid", "Great Void", typeof(SGreatVoid), "Marksam", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessGreatVoid(ModuleData module)
    {
        var comp = GetComponent(module, "TheGreatVoid");
        var fldDigits = GetArrayField<int>(comp, "Displays");
        var fldColors = GetArrayField<int>(comp, "ColorNums");

        yield return WaitForSolve;

        var colorNames = new[] { "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White" };
        for (var i = 0; i < 6; i++)
        {
            yield return question(SGreatVoid.Digit, args: [Ordinal(i + 1)]).Answers(fldDigits.Get()[i].ToString());
            yield return question(SGreatVoid.Color, args: [Ordinal(i + 1)]).Answers(colorNames[fldColors.Get()[i]]);
        }
    }
}