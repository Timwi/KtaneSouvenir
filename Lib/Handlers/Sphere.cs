using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSphere
{
    [SouvenirQuestion("What was the {1} flashed color in {0}?", ThreeColumns6Answers, "red", "blue", "green", "orange", "pink", "purple", "grey", "white", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("sphere", "Sphere", typeof(SSphere), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessSphere(ModuleData module)
    {
        var comp = GetComponent(module, "theSphereScript");
        var colorNames = GetArrayField<string>(comp, "colourNames", isPublic: true).Get();
        var colors = GetArrayField<int>(comp, "selectedColourIndices", isPublic: true).Get(expectedLength: 5, validator: c => c < 0 || c >= colorNames.Length ? $"expected range 0–{colorNames.Length - 1}" : null);

        yield return WaitForSolve;

        for (var i = 0; i < 5; i++)
            yield return question(SSphere.Colors, args: [Ordinal(i + 1)]).Answers(colorNames[colors[i]]);
    }
}
