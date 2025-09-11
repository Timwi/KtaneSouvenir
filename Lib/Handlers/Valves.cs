using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SValves
{
    [SouvenirQuestion("What was the initial state of {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "ValvesSprites")]
    InitialState
}

public partial class SouvenirModule
{
    [SouvenirHandler("valves", "Valves", typeof(SValves), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessValves(ModuleData module)
    {
        var comp = GetComponent(module, "Valves");
        yield return WaitForSolve;

        if (ValvesSprites.Length != 8)
            throw new AbandonModuleException($"Valves should have 8 sprites. Counted {ValvesSprites.Length}");

        var valvesColorNums = GetArrayField<int>(comp, "valvesColorNum").Get(expectedLength: 3, validator: val => val is not 0 and not 1 ? "expected 0 or 1" : null);
        var spriteIx = valvesColorNums.Aggregate(0, (p, n) => (p << 1) | (n ^ 1));
        addQuestion(module, Question.ValvesInitialState, correctAnswers: new[] { ValvesSprites[spriteIx] });
    }
}