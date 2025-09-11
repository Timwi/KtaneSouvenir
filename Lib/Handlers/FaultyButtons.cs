using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

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
        var qs = new List<QandA>();
        for (var pos = 0; pos < 16; pos++)
        {
            var buttonRefersTo = new Coord(4, 4, referredButtons[pos]);
            var refersToButton = new Coord(4, 4, Array.IndexOf(referredButtons, pos));
            qs.Add(makeQuestion(Question.FaultyButtonsReferredToThisButton, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { refersToButton }, preferredWrongAnswers: new[] { buttonRefersTo }));
            qs.Add(makeQuestion(Question.FaultyButtonsThisButtonReferredTo, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { buttonRefersTo }, preferredWrongAnswers: new[] { refersToButton }));
        }
        addQuestions(module, qs);
    }
}