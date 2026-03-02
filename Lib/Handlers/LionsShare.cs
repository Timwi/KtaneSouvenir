using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLionsShare
{
    [SouvenirQuestion("Which year was displayed on {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16")]
    Year
}

public partial class SouvenirModule
{
    [SouvenirHandler("LionsShareModule", "Lion’s Share", typeof(SLionsShare), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessLionsShare(ModuleData module)
    {
        var comp = GetComponent(module, "LionsShareModule");
        var yearText = GetField<TextMesh>(comp, "Year", isPublic: true).Get().text;
        if (!int.TryParse(yearText, out var year) || year < 1 || year > 16)
            throw new AbandonModuleException($"Expected year number between 1 and 16; got: {yearText}");

        yield return WaitForSolve;

        yield return question(SLionsShare.Year).Answers(yearText);
    }
}
