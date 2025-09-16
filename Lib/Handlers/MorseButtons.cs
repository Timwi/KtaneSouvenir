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

        yield return question(SMorseButtons.ButtonLabel, args: [Ordinal(1)]).Answers(alphabet[letters[0]].ToString(), preferredWrong: alphabet.Select(x => x.ToString()).ToArray());
        yield return question(SMorseButtons.ButtonLabel, args: [Ordinal(2)]).Answers(alphabet[letters[1]].ToString(), preferredWrong: alphabet.Select(x => x.ToString()).ToArray());
        yield return question(SMorseButtons.ButtonLabel, args: [Ordinal(3)]).Answers(alphabet[letters[2]].ToString(), preferredWrong: alphabet.Select(x => x.ToString()).ToArray());
        yield return question(SMorseButtons.ButtonLabel, args: [Ordinal(4)]).Answers(alphabet[letters[3]].ToString(), preferredWrong: alphabet.Select(x => x.ToString()).ToArray());
        yield return question(SMorseButtons.ButtonLabel, args: [Ordinal(5)]).Answers(alphabet[letters[4]].ToString(), preferredWrong: alphabet.Select(x => x.ToString()).ToArray());
        yield return question(SMorseButtons.ButtonLabel, args: [Ordinal(6)]).Answers(alphabet[letters[5]].ToString(), preferredWrong: alphabet.Select(x => x.ToString()).ToArray());
        yield return question(SMorseButtons.ButtonColor, args: [Ordinal(1)]).Answers(colorNames[colors[0]].ToString(), preferredWrong: colorNames);
        yield return question(SMorseButtons.ButtonColor, args: [Ordinal(2)]).Answers(colorNames[colors[1]].ToString(), preferredWrong: colorNames);
        yield return question(SMorseButtons.ButtonColor, args: [Ordinal(3)]).Answers(colorNames[colors[2]].ToString(), preferredWrong: colorNames);
        yield return question(SMorseButtons.ButtonColor, args: [Ordinal(4)]).Answers(colorNames[colors[3]].ToString(), preferredWrong: colorNames);
        yield return question(SMorseButtons.ButtonColor, args: [Ordinal(5)]).Answers(colorNames[colors[4]].ToString(), preferredWrong: colorNames);
        yield return question(SMorseButtons.ButtonColor, args: [Ordinal(6)]).Answers(colorNames[colors[5]].ToString(), preferredWrong: colorNames);
    }
}
