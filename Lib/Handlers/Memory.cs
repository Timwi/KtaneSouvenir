using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMemory
{
    [SouvenirQuestion("What was the displayed number in the {1} stage of {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    Display,

    [SouvenirQuestion("In what position was the button that you pressed in the {1} stage of {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "MemorySprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Position,

    [SouvenirQuestion("What was the label of the button that you pressed in the {1} stage of {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("Memory", "Memory", typeof(SMemory), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessMemory(ModuleData module)
    {
        var comp = GetComponent(module, "MemoryComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        module.SolveIndex = _modulesSolved.IncSafe("Memory");

        var displaySequence = GetProperty<string>(comp, "DisplaySequence", true).Get();
        var indices = GetListField<int>(comp, "buttonIndicesPressed", false).Get();
        var labels = GetListField<string>(comp, "buttonLabelsPressed", false).Get();
        var qs = new List<QandA>();
        for (var stage = 0; stage < 4; stage++)
        {
            qs.Add(makeQuestion(Question.MemoryDisplay, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { displaySequence[stage].ToString() }));
            qs.Add(makeQuestion(Question.MemoryPosition, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { MemorySprites[indices[stage]] }, preferredWrongAnswers: MemorySprites));
            qs.Add(makeQuestion(Question.MemoryLabel, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { labels[stage][labels[stage].Length - 1].ToString() }));
        }
        addQuestions(module, qs);
    }
}