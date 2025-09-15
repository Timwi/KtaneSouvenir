using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFaultyButtons
{
    [SouvenirQuestion("Which button referred to the {1} button in reading order in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    ReferredToThisButton,

    [SouvenirQuestion("Which button did the {1} button in reading order refer to in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
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
            var buttonRefersTo = new Coord(4, 4, referredButtons[pos]);
            var refersToButton = new Coord(4, 4, Array.IndexOf(referredButtons, pos));
            yield return question(SFaultyButtons.ReferredToThisButton, args: [Ordinal(pos + 1)]).Answers(refersToButton, preferredWrong: [buttonRefersTo]);
            yield return question(SFaultyButtons.ThisButtonReferredTo, args: [Ordinal(pos + 1)]).Answers(buttonRefersTo, preferredWrong: [refersToButton]);
        }
    }
}