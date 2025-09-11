using System.Collections.Generic;
using System.Linq;
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
        var colorNames = Question.YellowButtonColors.GetAttribute().AllAnswers;
        addQuestions(module, sqs.Select((sq, ix) =>
        {
            var m = Regex.Match(sq.sharedMaterial.name, @"^Color([0-5])$");
            return !m.Success
                ? throw new AbandonModuleException($"Expected material name “Color0–5”, got: “{sq.sharedMaterial.name}”")
                : makeQuestion(Question.YellowButtonColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colorNames[int.Parse(m.Groups[1].Value)] });
        }));
    }
}