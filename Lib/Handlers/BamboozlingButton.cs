using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBamboozlingButton
{
    [SouvenirQuestion("What color was the button in the {1} stage of {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color,

    [SouvenirQuestion("What was the color of the {2} display in the {1} stage of {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    DisplayColor,

    [SouvenirQuestion("What was the {2} display in the {1} stage of {0}?", TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Display,

    [SouvenirQuestion("What was the {2} label on the button in the {1} stage of {0}?", TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE", TranslateArguments = [false, true], Arguments = [QandA.Ordinal, "top", QandA.Ordinal, "bottom"], ArgumentGroupSize = 2)]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("bamboozlingButton", "Bamboozling Button", typeof(SBamboozlingButton), "TasThiluna")]
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
            yield return new WaitForSeconds(.1f);
        }

        var colors = new string[15] { "White", "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Grey", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "Black" };
        var texts = new string[55] { "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE" };
        for (var i = 0; i < 2; i++)
        {
            yield return question(SBamboozlingButton.Color, args: [Ordinal(i + 1)]).Answers(colors[moduleData[i][0]]);
            yield return question(SBamboozlingButton.DisplayColor, args: [Ordinal(i + 1), "fourth"]).Answers(colors[moduleData[i][1]]);
            yield return question(SBamboozlingButton.DisplayColor, args: [Ordinal(i + 1), "fifth"]).Answers(colors[moduleData[i][2]]);
            yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), "first"]).Answers(texts[moduleData[i][3]]);
            yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), "third"]).Answers(texts[moduleData[i][4]]);
            yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), "fourth"]).Answers(texts[moduleData[i][5]]);
            yield return question(SBamboozlingButton.Display, args: [Ordinal(i + 1), "fifth"]).Answers(texts[moduleData[i][6]]);
            yield return question(SBamboozlingButton.Label, args: [Ordinal(i + 1), "top"]).Answers(texts[moduleData[i][7]]);
            yield return question(SBamboozlingButton.Label, args: [Ordinal(i + 1), "bottom"]).Answers(texts[moduleData[i][8]]);
        }
    }
}