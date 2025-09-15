using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SYellowButton
{
    [SouvenirQuestion("What was the {1} color in {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Cyan", "Blue", "Magenta", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("YellowButtonModule", "Yellow Button", typeof(SYellowButton), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessYellowButton(ModuleData module)
    {
        var comp = GetComponent(module, "YellowButtonScript");
        yield return WaitForSolve;

        var sqs = GetArrayField<MeshRenderer>(comp, "ColorSquares", isPublic: true).Get(expectedLength: 8);
        var colorNames = SYellowButton.Colors.GetAnswers();
        for (var ix = 0; ix < sqs.Length; ix++)
            yield return sqs[ix].sharedMaterial.name.RegexMatch(@"^Color([0-5])$", out var m)
                ? question(SYellowButton.Colors, args: [Ordinal(ix + 1)]).Answers(colorNames[int.Parse(m.Groups[1].Value)])
                : throw new AbandonModuleException($"Expected material name “Color0–5”, got: “{sqs[ix].sharedMaterial.name}”");
    }
}
