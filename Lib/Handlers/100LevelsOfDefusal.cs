using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S100LevelsOfDefusal
{
    [Question("Which letter was displayed in {0}?", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
    Letters
}

public partial class SouvenirModule
{
    [Handler("100LevelsOfDefusal", "100 Levels of Defusal", typeof(S100LevelsOfDefusal), "Espik")]
    [ManualQuestion("What were the displayed letters?")]
    private IEnumerator<SouvenirInstruction> Process100LevelsOfDefusal(ModuleData module)
    {
        var comp = GetComponent(module, "OneHundredLevelsOfDefusal");

        yield return WaitForSolve;

        var display = GetArrayField<char>(comp, "displayedLetters").Get(expectedLength: 12);
        var letters = display.Where(x => x != '.').Select(x => x.ToString()).ToArray();

        yield return question(S100LevelsOfDefusal.Letters).Answers(letters);
    }
}
