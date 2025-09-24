using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotWordSearch
{
    [SouvenirQuestion("Which of these consonants was missing in {0}?", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
    Missing,

    [SouvenirQuestion("What was the first correctly pressed letter in {0}?", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
    FirstPress
}

public partial class SouvenirModule
{
    [SouvenirHandler("notWordSearch", "Not Word Search", typeof(SNotWordSearch), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessNotWordSearch(ModuleData module)
    {
        var comp = GetComponent(module, "NWSScript");
        yield return WaitForSolve;

        var missingConsonants = GetArrayField<string>(comp, "missing").Get(expectedLength: 3);
        var pressed = GetArrayField<string>(comp, "ans").Get(expectedLength: 12);

        yield return question(SNotWordSearch.Missing).Answers(missingConsonants);
        yield return question(SNotWordSearch.FirstPress).Answers(pressed[0]);
    }
}