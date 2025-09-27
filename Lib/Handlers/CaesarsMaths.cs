using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SCaesarsMaths
{
    [SouvenirQuestion("What color was the {1} LED in {0}?", TwoColumns4Answers, "Yellow", "Blue", "Red", "Green", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    LED
}

public partial class SouvenirModule
{
    [SouvenirHandler("caesarsMaths", "Caesar's Maths", typeof(SCaesarsMaths), "KiloBites")]
    private IEnumerator<SouvenirInstruction> ProcessCaesarsMaths(ModuleData module)
    {
        var comp = GetComponent(module, "caesarsMathsScript");
        yield return WaitForSolve;

        var usedLEDS = GetArrayField<int>(comp, "ledIndices").Get(expectedLength: 3);

        var colorNames = new[] { "Yellow", "Blue", "Red", "Green" };

        for (int i = 0; i < 3; i++)
            yield return question(SCaesarsMaths.LED, args: [Ordinal(i + 1)]).Answers(colorNames[usedLEDS[i]]);
    }
}
