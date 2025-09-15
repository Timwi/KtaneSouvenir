using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWhatsOnSecond
{
    [SouvenirQuestion("What was the display text in the {1} stage of {0}?", ThreeColumns6Answers, "got it", "says", "display", "leed", "their", "blank", "right", "reed", "hold", "they are", "louder", "lead", "repeat", "ready", "none", "led", "ur", "you’re", "no", "you", "nothing", "middle", "done", "empty", "your", "hold on", "like", "read", "wait", "left", "press", "what?", "uh uh", "they’re", "uhhh", "c", "error", "you are", "next", "yes", "u", "sure", "okay", "what", "cee", "first", "see", "uh huh", "there", "red", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayText,

    [SouvenirQuestion("What was the display text color in the {1} stage of {0}?", ThreeColumns6Answers, "Blue", "Cyan", "Green", "Magenta", "Red", "Yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("WhatsOnSecond", "What’s on Second", typeof(SWhatsOnSecond), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessWhatsOnSecond(ModuleData module)
    {
        var comp = GetComponent(module, "WhatsonSecondScript");
        yield return WaitForSolve;

        var labels = GetArrayField<string>(comp, "Answers").Get(expectedLength: 2);
        var labelColors = GetArrayField<string>(comp, "AnswerColors").Get(expectedLength: 2);

        addQuestions(module,
           makeQuestion(Question.WhatsOnSecondDisplayText, module, formatArgs: new[] { "first" }, correctAnswers: new[] { labels[0].ToLowerInvariant() }),
           makeQuestion(Question.WhatsOnSecondDisplayText, module, formatArgs: new[] { "second" }, correctAnswers: new[] { labels[1].ToLowerInvariant() }),
           makeQuestion(Question.WhatsOnSecondDisplayColor, module, formatArgs: new[] { "first" }, correctAnswers: new[] { labelColors[0] }),
           makeQuestion(Question.WhatsOnSecondDisplayColor, module, formatArgs: new[] { "second" }, correctAnswers: new[] { labelColors[1] }));
    }
}