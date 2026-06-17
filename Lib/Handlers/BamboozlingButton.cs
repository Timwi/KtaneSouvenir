using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBamboozlingButton
{
    [Question("What color was the button in the {1} stage of {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color,

    [Question("What was the color of the {2} display in the {1} stage of {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    DisplayColor,

    [Question("What was the {2} display in the {1} stage of {0}?", TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Display,

    [Question("What was the {2} label on the button in the {1} stage of {0}?", TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE", TranslateArguments = [false, true], Arguments = [QandA.Ordinal, "top", QandA.Ordinal, "bottom"], ArgumentGroupSize = 2)]
    Label
}

public partial class SouvenirModule
{
    [Handler("bamboozlingButton", "Bamboozling Button", typeof(SBamboozlingButton), "TasThiluna")]
    [ManualQuestion("What color was the button in each stage?")]
    [ManualQuestion("What were the labels on the button in each stage?")]
    [ManualQuestion("What were the displays and their colors in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessBamboozlingButton(ModuleData module)
    {
        var comp = GetComponent(module, "BamboozlingButtonScript");
        var fldRandomiser = GetArrayField<int>(comp, "randomiser");
        var fldStage = GetIntField(comp, "stage");

        var moduleData = new int[2][];
        var stage = 0;

        while (module.Unsolved)
        {
            var randomiser = fldRandomiser.Get(expectedLength: 11);
            var newStage = fldStage.Get(min: 1, max: 2);
            if (stage != newStage || !randomiser.SequenceEqual(moduleData[newStage - 1]))
            {
                stage = newStage;
                moduleData[stage - 1] = randomiser.ToArray(); // Take a copy of the array.
            }
            yield return null;
        }

        var colors = new string[15] { "White", "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Grey", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "Black" };
        var texts = new string[55] { "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE" };

        var firstDisplayTexts = Enumerable.Range(0, 8).Select(x => texts[x]).ToArray();
        var secondDisplayTexts = Enumerable.Range(8, 47).Select(x => texts[x]).ToArray();

        for (var i = 0; i < 2; i++)
        {
            // Checks for the special cases
            var allDisplays = new[] { texts[moduleData[i][3]], texts[moduleData[i][4]], texts[moduleData[i][5]], texts[moduleData[i][6]] };

            if (allDisplays.Contains(texts[moduleData[i][7]]))
            {
                yield return question(SBamboozlingButton.DisplayColor, args: [Ordinal(i + 1), Ordinal(4)]).Answers(colors[moduleData[i][1]]);
                yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), Ordinal(4)]).Answers(texts[moduleData[i][5]], all:secondDisplayTexts);
            }

            else if (allDisplays.Contains(texts[moduleData[i][8]]))
            {
                yield return question(SBamboozlingButton.DisplayColor, args: [Ordinal(i + 1), Ordinal(5)]).Answers(colors[moduleData[i][2]]);
                yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), Ordinal(5)]).Answers(texts[moduleData[i][6]], all: secondDisplayTexts);
            }

            else
            {
                yield return question(SBamboozlingButton.Color, args: [Ordinal(i + 1)]).Answers(colors[moduleData[i][0]]);
                yield return question(SBamboozlingButton.DisplayColor, args: [Ordinal(i + 1), Ordinal(4)]).Answers(colors[moduleData[i][1]]);
                yield return question(SBamboozlingButton.DisplayColor, args: [Ordinal(i + 1), Ordinal(5)]).Answers(colors[moduleData[i][2]]);
                yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), Ordinal(1)]).Answers(texts[moduleData[i][3]], all: firstDisplayTexts);
                yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), Ordinal(3)]).Answers(texts[moduleData[i][4]], all: firstDisplayTexts);
                yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), Ordinal(4)]).Answers(texts[moduleData[i][5]], all: secondDisplayTexts);
                yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), Ordinal(5)]).Answers(texts[moduleData[i][6]], all: secondDisplayTexts);
                yield return question(SBamboozlingButton.Label, args: [Ordinal(i + 1), "top"]).Answers(texts[moduleData[i][7]]);
                yield return question(SBamboozlingButton.Label, args: [Ordinal(i + 1), "bottom"]).Answers(texts[moduleData[i][8]]);
            }
        }
    }
}
