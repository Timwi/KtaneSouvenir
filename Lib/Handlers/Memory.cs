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
        module.SolveIndex = module.Info.NumSolved++;

        var displaySequence = GetProperty<string>(comp, "DisplaySequence", true).Get();
        var indices = GetListField<int>(comp, "buttonIndicesPressed", false).Get();
        var labels = GetListField<string>(comp, "buttonLabelsPressed", false).Get();
        for (var stage = 0; stage < 4; stage++)
        {
            yield return question(SMemory.Display, args: [Ordinal(stage + 1)]).Answers(displaySequence[stage].ToString());
            yield return question(SMemory.Position, args: [Ordinal(stage + 1)]).Answers(MemorySprites[indices[stage]], preferredWrong: MemorySprites);
            yield return question(SMemory.Label, args: [Ordinal(stage + 1)]).Answers(labels[stage][labels[stage].Length - 1].ToString());
        }
    }
}
