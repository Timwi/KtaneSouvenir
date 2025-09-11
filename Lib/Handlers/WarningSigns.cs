using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWarningSigns
{
    [SouvenirQuestion("What was the displayed sign in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "WarningSignsSprites")]
    DisplayedSign
}

public partial class SouvenirModule
{
    [SouvenirHandler("warningSigns", "Warning Signs", typeof(SWarningSigns), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessWarningSigns(ModuleData module)
    {
        var comp = GetComponent(module, "warningSignSrc");

        yield return WaitForSolve;

        var displayedSign = GetIntField(comp, "chosenSign").Get(min: 0, max: 19);
        addQuestion(module, Question.WarningSignsDisplayedSign, correctAnswers: new[] { WarningSignsSprites[displayedSign] }, preferredWrongAnswers: WarningSignsSprites);
    }
}