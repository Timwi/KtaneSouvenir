using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCruelBinary
{
    [Question("What was the displayed word in {0}?", TwoColumns4Answers, ExampleAnswers = ["LEAST", "YELLOW", "SIERRA", "WHITE"])]
    DisplayedWord
}

public partial class SouvenirModule
{
    [Handler("CruelBinary", "Cruel Binary", typeof(SCruelBinary), "Kuro")]
    [ManualQuestion("What was the displayed word?")]
    private IEnumerator<SouvenirInstruction> ProcessCruelBinary(ModuleData module)
    {
        var comp = GetComponent(module, "CruelBinary");

        yield return WaitForSolve;

        var wordList = GetArrayField<string>(comp, "_WordList", isPublic: true).Get();
        var displayedWord = GetField<string>(comp, "h", isPublic: true).Get();
        yield return question(SCruelBinary.DisplayedWord).Answers(displayedWord, preferredWrong: wordList);
    }
}