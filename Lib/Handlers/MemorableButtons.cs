using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMemorableButtons
{
    [SouvenirQuestion("What was the {1} correct symbol pressed in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "J", "K", "L", "P", "Q", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, Type = AnswerType.DynamicFont)]
    Symbols
}

public partial class SouvenirModule
{
    [SouvenirHandler("memorableButtons", "Memorable Buttons", typeof(SMemorableButtons), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMemorableButtons(ModuleData module)
    {
        var comp = GetComponent(module, "MemorableButtons");
        var buttonLabels = GetArrayField<TextMesh>(comp, "buttonLabels", isPublic: true).Get(ar => ar.Length == 0 ? "empty" : null);

        yield return WaitForSolve;

        var combinedCode = GetField<string>(comp, "combinedCode", isPublic: true).Get(str => str.Length is < 10 or > 15 ? "expected length 10–15" : null);
        addQuestions(module, combinedCode.Select((ch, ix) => makeQuestion(Question.MemorableButtonsSymbols, module, buttonLabels[0].font, buttonLabels[0].GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { ch.ToString() })));
    }
}