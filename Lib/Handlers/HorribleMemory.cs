using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHorribleMemory
{
    [SouvenirQuestion("In what position was the button pressed on the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    Positions,

    [SouvenirQuestion("What was the label of the button pressed on the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    Labels,

    [SouvenirQuestion("What color was the button pressed on the {1} stage of {0}?", ThreeColumns6Answers, "blue", "green", "red", "orange", "purple", "pink", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("horribleMemory", "Horrible Memory", typeof(SHorribleMemory), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessHorribleMemory(ModuleData module)
    {
        var comp = GetComponent(module, "cruelMemoryScript");
        yield return WaitForSolve;

        var pos = GetListField<int>(comp, "correctStagePositions", isPublic: true).Get(expectedLength: 5);
        var lbl = GetListField<int>(comp, "correctStageLabels", isPublic: true).Get(expectedLength: 5);
        var colors = GetListField<string>(comp, "correctStageColours", isPublic: true).Get(expectedLength: 5);

        yield return question(SHorribleMemory.Positions, args: [Ordinal(1)]).Answers(pos[0].ToString());
        yield return question(SHorribleMemory.Positions, args: [Ordinal(2)]).Answers(pos[1].ToString());
        yield return question(SHorribleMemory.Positions, args: [Ordinal(3)]).Answers(pos[2].ToString());
        yield return question(SHorribleMemory.Positions, args: [Ordinal(4)]).Answers(pos[3].ToString());
        yield return question(SHorribleMemory.Labels, args: [Ordinal(1)]).Answers(lbl[0].ToString());
        yield return question(SHorribleMemory.Labels, args: [Ordinal(2)]).Answers(lbl[1].ToString());
        yield return question(SHorribleMemory.Labels, args: [Ordinal(3)]).Answers(lbl[2].ToString());
        yield return question(SHorribleMemory.Labels, args: [Ordinal(4)]).Answers(lbl[3].ToString());
        yield return question(SHorribleMemory.Colors, args: [Ordinal(1)]).Answers(colors[0]);
        yield return question(SHorribleMemory.Colors, args: [Ordinal(2)]).Answers(colors[1]);
        yield return question(SHorribleMemory.Colors, args: [Ordinal(3)]).Answers(colors[2]);
        yield return question(SHorribleMemory.Colors, args: [Ordinal(4)]).Answers(colors[3]);
    }
}
