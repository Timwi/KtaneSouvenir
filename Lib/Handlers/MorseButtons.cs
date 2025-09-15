using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMorseButtons
{
    [SouvenirQuestion("What was the character flashed by the {1} button in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-Z0-9")]
    ButtonLabel,

    [SouvenirQuestion("What was the color flashed by the {1} button in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "orange", "purple", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ButtonColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("morseButtons", "Morse Buttons", typeof(SMorseButtons), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessMorseButtons(ModuleData module)
    {
        var comp = GetComponent(module, "morseButtonsScript");
        yield return WaitForSolve;

        var alphabet = GetField<string>(comp, "alphabet").Get();
        var colorNames = new[] { "red", "blue", "green", "yellow", "orange", "purple" };
        var letters = GetArrayField<int>(comp, "letters").Get(expectedLength: 6, validator: x => x < 0 || x >= alphabet.Length ? "out of range" : null);
        var colors = GetArrayField<int>(comp, "colors").Get(expectedLength: 6, validator: x => x < 0 || x >= colorNames.Length ? "out of range" : null);

        addQuestions(module,
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "first" }, correctAnswers: new[] { alphabet[letters[0]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "second" }, correctAnswers: new[] { alphabet[letters[1]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "third" }, correctAnswers: new[] { alphabet[letters[2]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { alphabet[letters[3]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { alphabet[letters[4]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "sixth" }, correctAnswers: new[] { alphabet[letters[5]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "first" }, correctAnswers: new[] { colorNames[colors[0]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "second" }, correctAnswers: new[] { colorNames[colors[1]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "third" }, correctAnswers: new[] { colorNames[colors[2]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colorNames[colors[3]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { colorNames[colors[4]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "sixth" }, correctAnswers: new[] { colorNames[colors[5]].ToString() }, preferredWrongAnswers: colorNames));
    }
}