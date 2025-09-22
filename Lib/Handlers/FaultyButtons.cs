using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SFaultyButtons
{
    [SouvenirQuestion("Which button referred to this button in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    ReferredToThisButton,

    [SouvenirQuestion("Which button did this button refer to in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    ThisButtonReferredTo
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSFaultyButtons", "Faulty Buttons", typeof(SFaultyButtons), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessFaultyButtons(ModuleData module)
    {
        var comp = GetComponent(module, "FaultyButtonsScript");

        yield return WaitForSolve;

        var referredButtons = GetField<int[]>(comp, "ReferredButtons").Get();
        for (var pos = 0; pos < 16; pos++)
        {
            var thisButton = new Coord(4, 4, pos);
            var buttonRefersTo = new Coord(4, 4, referredButtons[pos]);
            yield return question(SFaultyButtons.ThisButtonReferredTo, questionSprite: Sprites.GenerateGridSprite(thisButton)).Answers(buttonRefersTo);
            yield return question(SFaultyButtons.ReferredToThisButton, questionSprite: Sprites.GenerateGridSprite(buttonRefersTo)).Answers(thisButton);
        }
    }
}
