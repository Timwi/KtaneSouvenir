using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUNO
{
    [SouvenirQuestion("What was the initial card in {0}?", OneColumn4Answers, "Red 0", "Red 1", "Red 2", "Red 3", "Red 4", "Red 5", "Red 6", "Red 7", "Red 8", "Red 9", "Red +2", "Red Skip", "Red Reverse", "Green 0", "Green 1", "Green 2", "Green 3", "Green 4", "Green 5", "Green 6", "Green 7", "Green 8", "Green 9", "Green +2", "Green Skip", "Green Reverse", "Yellow 0", "Yellow 1", "Yellow 2", "Yellow 3", "Yellow 4", "Yellow 5", "Yellow 6", "Yellow 7", "Yellow 8", "Yellow 9", "Yellow +2", "Yellow Skip", "Yellow Reverse", "Blue 0", "Blue 1", "Blue 2", "Blue 3", "Blue 4", "Blue 5", "Blue 6", "Blue 7", "Blue 8", "Blue 9", "Blue +2", "Blue Skip", "Blue Reverse", "+4", "Wild", TranslateAnswers = true)]
    InitialCard
}

public partial class SouvenirModule
{
    [SouvenirHandler("UNO", "UNO!", typeof(SUNO), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessUNO(ModuleData module)
    {
        var comp = GetComponent(module, "UNO");
        var fldFirstInDeck = GetField<string>(comp, "firstInDeck");
        var mthGetUnoName = GetMethod<string>(comp, "better", 1);

        yield return WaitForSolve;

        yield return question(SUNO.InitialCard).Answers(new string[] { titleCase(mthGetUnoName.Invoke(fldFirstInDeck.Get())) });
    }
}