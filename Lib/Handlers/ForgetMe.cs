using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SForgetMe
{
    [SouvenirQuestion("What number was in the {1} position of the initial puzzle in {0}?", ThreeColumns6Answers, TranslateFormatArgs = [true], Arguments = ["top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 8)]
    InitialState
}

public partial class SouvenirModule
{
    [SouvenirHandler("forgetMe", "Forget Me", typeof(SForgetMe), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessForgetMe(ModuleData module)
    {
        var comp = GetComponent(module, "NotForgetMeNotScript");
        yield return WaitForSolve;

        string[] positions = { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        var initState = GetArrayField<int>(comp, "givenPuzzle").Get(expectedLength: 9);
        addQuestions(module,
            Enumerable.Range(0, 9).Where(ix => initState[ix] != 0).Select(ix =>
            makeQuestion(Question.ForgetMeInitialState, module, formatArgs: new[] { positions[ix] }, correctAnswers: new[] { initState[ix].ToString() })));
    }
}