using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNumberedButtons
{
    [SouvenirQuestion("Which number was correctly pressed on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 100)]
    Buttons
}

public partial class SouvenirModule
{
    [SouvenirHandler("numberedButtonsModule", "Numbered Buttons", typeof(SNumberedButtons), "Eltrick")]
    private IEnumerator<SouvenirInstruction> ProcessNumberedButtons(ModuleData module)
    {
        var comp = GetComponent(module, "NumberedButtonsScript");
        var expectedButtons = GetListField<string>(comp, "ExpectedButtons").Get(list => list.Count == 0 ? "list is empty" : null).ToArray();

        var hadStrike = false;
        module.Module.OnStrike += delegate { hadStrike = true; return false; };

        while (module.Unsolved)
        {
            yield return null;
            if (hadStrike)
            {
                yield return null;
                expectedButtons = GetListField<string>(comp, "ExpectedButtons").Get(list => list.Count == 0 ? "list is empty" : null).ToArray();
            }
        }
        addQuestion(module, Question.NumberedButtonsButtons, correctAnswers: expectedButtons);
    }
}