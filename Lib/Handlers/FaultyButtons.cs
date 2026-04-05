using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SFaultyButtons
{
    [Question("Which button referred to this button in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    ReferredToThisButton,

    [Question("Which button did this button refer to in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    ThisButtonReferredTo
}

public partial class SouvenirModule
{
    [Handler("GSFaultyButtons", "Faulty Buttons", typeof(SFaultyButtons), "Kuro")]
    [ManualQuestion("Which button did each button refer to?")]
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
