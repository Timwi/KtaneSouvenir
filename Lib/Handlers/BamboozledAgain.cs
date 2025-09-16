using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBamboozledAgain
{
    [SouvenirQuestion("What was the text on the {1} correct button in {0}?", TwoColumns4Answers, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE", "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ButtonText,

    [SouvenirQuestion("What color was the {1} correct button in {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ButtonColor,

    [SouvenirQuestion("What was the {1} decrypted text on the display in {0}?", TwoColumns4Answers, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayTexts1,

    [SouvenirQuestion("What was the {1} decrypted text on the display in {0}?", TwoColumns4Answers, "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayTexts2,

    [SouvenirQuestion("What color was the {1} text on the display in {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("bamboozledAgain", "Bamboozled Again", typeof(SBamboozledAgain), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessBamboozledAgain(ModuleData module)
    {
        var comp = GetComponent(module, "BamboozledAgainScript");
        var fldDisplayTexts = GetArrayField<string[]>(comp, "message");
        var fldColorIndex = GetArrayField<int>(comp, "textRandomiser");
        var fldStage = GetIntField(comp, "pressCount");
        var fldCorrectButtons = GetArrayField<int[]>(comp, "answerKey");
        var fldButtonInfo = GetArrayField<string[]>(comp, "buttonRandomiser");
        var fldButtonTextMesh = GetArrayField<TextMesh>(comp, "buttonText", isPublic: true);

        //Beginning of correct button section.

        var stage = 0;

        var correctButtonTexts = new string[4];
        var correctButtonColors = new string[4];

        //The module cycle the stage count back to 0 regardless. So it gives no indications whether the module is solved or not on the fourth press.
        //Stores the first button in a separate variable. Then, restore it once the module is solved. Index 0 for text. Index 1 for color.

        var correctFirstStageButton = new string[2];

        var dataAdded = false;

        while (module.Unsolved)
        {
            var newStage = fldStage.Get(min: 0, max: 3);
            if (!dataAdded)
            {
                var buttonInfo = fldButtonInfo.Get(expectedLength: 2, validator: v => v.Length != 6 ? "expected length 6" : null);
                var correctButtons = fldCorrectButtons.Get(expectedLength: 2, validator: v => v.Length != 4 ? "expected length 4" : null);
                if (stage == 0)
                {
                    correctFirstStageButton[0] = correctButtonTexts[stage];
                    correctFirstStageButton[1] = correctButtonColors[stage];
                }
                correctButtonTexts[stage] = Regex.Replace(buttonInfo[1][correctButtons[0][stage]], "#", " ");
                correctButtonColors[stage] = buttonInfo[0][correctButtons[0][stage]][0] + buttonInfo[0][correctButtons[0][stage]].Substring(1).ToLowerInvariant();
                dataAdded = true;
            }
            if (stage != newStage)
            {
                stage = newStage;
                dataAdded = false;
            }
            var buttonTextMesh = fldButtonTextMesh.Get();

            if (buttonTextMesh == null)
                yield break;

            //Check if the module is resetting. There is no flag indicating the module is resetting, but each button will have exactly a string with length of 1 on it.
            if (buttonTextMesh.Any(strMesh => strMesh.text.Length == 1))
                dataAdded = false;

            yield return null;
        }

        //Restore the first button to the arrays.

        correctButtonTexts[0] = correctFirstStageButton[0];
        correctButtonColors[0] = correctFirstStageButton[1];

        //End of correct button section.

        //Beginning of the displayed texts section.

        var displayTexts = fldDisplayTexts.Get(expectedLength: 4, validator: v => v.Length != 8 ? "expected length 8" : null).ToArray();
        var colorIndex = fldColorIndex.Get(expectedLength: 8);

        if (displayTexts[0].Any(string.IsNullOrEmpty))
            throw new AbandonModuleException($"‘displayText[0]’ contains null or an empty string: [{displayTexts[0].Select(str => str ?? "<null>").JoinString(", ")}]");

        displayTexts[0] = displayTexts[0].Select(str => Regex.Replace(str, "#", " ")).ToArray();

        var firstRowTexts = displayTexts[0].Where((item, index) => index is 0 or 2 or 4).ToArray();
        var lastThreeTexts = displayTexts[0].Where((item, index) => index is > 4 and < 8).ToArray();
        var color = new string[14] { "White", "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Grey", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose" };
        var displayColors = colorIndex.Select(index => color[index]).ToArray();

        //End of the displayed texts section.

        for (var index = 0; index < correctButtonTexts.Length; index++)
            yield return question(SBamboozledAgain.ButtonText, args: [Ordinal(index + 1)])
                .Answers(correctButtonTexts[index], preferredWrong: correctButtonTexts.Except([correctButtonTexts[index]]).ToArray());
        for (var index = 0; index < correctButtonColors.Length; index++)
            yield return question(SBamboozledAgain.ButtonColor, args: [Ordinal(index + 1)])
                .Answers(correctButtonColors[index], preferredWrong: correctButtonColors.Except([correctButtonColors[index]]).ToArray());
        for (var index = 0; index < firstRowTexts.Length; index++)
            yield return question(SBamboozledAgain.DisplayTexts1, args: [Ordinal(2 * index + 1)])
                .Answers(firstRowTexts[index], preferredWrong: firstRowTexts.Except([firstRowTexts[index]]).ToArray());
        for (var index = 0; index < lastThreeTexts.Length; index++)
            yield return question(SBamboozledAgain.DisplayTexts2, args: [Ordinal(index + 6)])
                .Answers(lastThreeTexts[index], preferredWrong: lastThreeTexts.Except([lastThreeTexts[index]]).ToArray());
        for (var index = 0; index < displayColors.Length; index++)
            yield return question(SBamboozledAgain.DisplayColor, args: [Ordinal(index + 1)])
                .Answers(displayColors[index], preferredWrong: displayColors.Except([displayColors[index]]).ToArray());
    }
}
