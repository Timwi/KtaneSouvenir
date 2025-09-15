using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotWhosOnFirst
{
    [SouvenirQuestion("In which position was the button you pressed in the {1} stage on {0}?", TwoColumns4Answers, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    PressedPosition,

    [SouvenirQuestion("What was the label on the button you pressed in the {1} stage on {0}?", ThreeColumns6Answers, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    PressedLabel,

    [SouvenirQuestion("In which position was the reference button in the {1} stage on {0}?", TwoColumns4Answers, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ReferencePosition,

    [SouvenirQuestion("What was the label on the reference button in the {1} stage on {0}?", ThreeColumns6Answers, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ReferenceLabel,

    [SouvenirQuestion("What was the calculated number in the second stage on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 60)]
    Sum
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotWhosOnFirst", "Not Who’s on First", typeof(SNotWhosOnFirst), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessNotWhosOnFirst(ModuleData module)
    {
        var comp = GetComponent(module, "NotWhosOnFirst");
        var fldPositions = GetArrayField<int>(comp, "rememberedPositions");
        var fldLabels = GetArrayField<string>(comp, "rememberedLabels");
        var fldSum = GetIntField(comp, "stage2Sum");

        yield return WaitForSolve;

        var positions = SNotWhosOnFirst.PressedPosition.GetAnswers();
        var sumCorrectAnswers = new[] { fldSum.Get().ToString() };
        for (var i = 0; i < 4; i++)
        {
            yield return question(SNotWhosOnFirst.PressedPosition, args: [Ordinal(i + 1)]).Answers(positions[fldPositions.Get()[i]]);
            yield return question(SNotWhosOnFirst.PressedLabel, args: [Ordinal(i + 1)]).Answers(fldLabels.Get()[i]);
        }
        for (var i = 4; i < 6; i++)
        {
            yield return question(SNotWhosOnFirst.ReferencePosition, args: [Ordinal(i - 1)]).Answers(positions[fldPositions.Get()[i]]);
            yield return question(SNotWhosOnFirst.ReferenceLabel, args: [Ordinal(i - 1)]).Answers(fldLabels.Get()[i]);
        }
        yield return question(SNotWhosOnFirst.Sum).Answers(sumCorrectAnswers);
    }
}