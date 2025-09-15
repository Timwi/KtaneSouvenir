using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAlfaBravo
{
    [SouvenirQuestion("Which letter was pressed in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
    PressedLetter,

    [SouvenirQuestion("Which letter was to the left of the pressed one in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
    LeftPressedLetter,

    [SouvenirQuestion("Which letter was to the right of the pressed one in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
    RightPressedLetter,

    [SouvenirQuestion("What was the last digit on the small display in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    Digit
}

public partial class SouvenirModule
{
    [SouvenirHandler("alfa_bravo", "Alfa-Bravo", typeof(SAlfaBravo), "NickLatkovich")]
    private IEnumerator<SouvenirInstruction> ProcessAlfaBravo(ModuleData module)
    {
        var comp = GetComponent(module, "AlfaBravoModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
            yield return legitimatelyNoQuestion(module, "The module was force-solved.");

        var pressedLetter = GetProperty<char>(comp, "souvenirPressedLetter", true).Get();
        if (pressedLetter != 0)
            yield return question(SAlfaBravo.PressedLetter).Answers(pressedLetter.ToString());

        var letterToTheLeftOfPressedOne = GetProperty<char>(comp, "souvenirLetterToTheLeftOfPressedOne", true).Get();
        if (letterToTheLeftOfPressedOne != 0)
            yield return question(SAlfaBravo.LeftPressedLetter).Answers(letterToTheLeftOfPressedOne.ToString());

        var letterToTheRightOfPressedOne = GetProperty<char>(comp, "souvenirLetterToTheRightOfPressedOne", true).Get();
        if (letterToTheRightOfPressedOne != 0)
            yield return question(SAlfaBravo.RightPressedLetter).Answers(letterToTheRightOfPressedOne.ToString());

        yield return question(SAlfaBravo.Digit).Answers(GetProperty<int>(comp, "souvenirDisplayedDigit", true).Get().ToString());
    }
}
