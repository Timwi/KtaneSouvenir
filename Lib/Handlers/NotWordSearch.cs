using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotWordSearch
{
    [Question("Which of these consonants was missing in {0}?", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
    Missing,

    [Question("What was the first correctly pressed letter in {0}?", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
    FirstPress
}

public partial class SouvenirModule
{
    [Handler("notWordSearch", "Not Word Search", typeof(SNotWordSearch), "tandyCake")]
    [ManualQuestion("Which consonants were missing?")]
    [ManualQuestion("What was the first correctly pressed letter?")]
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