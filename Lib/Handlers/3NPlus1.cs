using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum S3NPlus1
{
    [SouvenirQuestion("What number was initially displayed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 100)]
    Question
}

public partial class SouvenirModule
{
    [SouvenirHandler("threeNPlusOne", "3N+1", typeof(S3NPlus1), "Hawker")]
    private IEnumerator<SouvenirInstruction> Process3NPlus1(ModuleData module)
    {
        var comp = GetComponent(module, "ThreeNPlusOneScript");
        var fldDisplayText = GetField<TextMesh>(comp, "DisplayText", isPublic: true);
        var fldStage = GetField<int>(comp, "Stage");

        var text = fldDisplayText.Get().text;
        yield return !int.TryParse(text, out var answer)
            ? throw new AbandonModuleException($"“{text}” does not parse as an integer.")
            : (YieldInstruction) WaitForSolve;
        addQuestion(module, Question._3NPlus1, correctAnswers: new[] { answer.ToString() });
    }
}