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

        addQuestions(module,
            makeQuestion(Question.HorribleMemoryPositions, module, formatArgs: new[] { "first" }, correctAnswers: new[] { pos[0].ToString() }),
            makeQuestion(Question.HorribleMemoryPositions, module, formatArgs: new[] { "second" }, correctAnswers: new[] { pos[1].ToString() }),
            makeQuestion(Question.HorribleMemoryPositions, module, formatArgs: new[] { "third" }, correctAnswers: new[] { pos[2].ToString() }),
            makeQuestion(Question.HorribleMemoryPositions, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { pos[3].ToString() }),
            makeQuestion(Question.HorribleMemoryLabels, module, formatArgs: new[] { "first" }, correctAnswers: new[] { lbl[0].ToString() }),
            makeQuestion(Question.HorribleMemoryLabels, module, formatArgs: new[] { "second" }, correctAnswers: new[] { lbl[1].ToString() }),
            makeQuestion(Question.HorribleMemoryLabels, module, formatArgs: new[] { "third" }, correctAnswers: new[] { lbl[2].ToString() }),
            makeQuestion(Question.HorribleMemoryLabels, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { lbl[3].ToString() }),
            makeQuestion(Question.HorribleMemoryColors, module, formatArgs: new[] { "first" }, correctAnswers: new[] { colors[0] }),
            makeQuestion(Question.HorribleMemoryColors, module, formatArgs: new[] { "second" }, correctAnswers: new[] { colors[1] }),
            makeQuestion(Question.HorribleMemoryColors, module, formatArgs: new[] { "third" }, correctAnswers: new[] { colors[2] }),
            makeQuestion(Question.HorribleMemoryColors, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colors[3] }));
    }
}