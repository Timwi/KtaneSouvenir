using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SForgetMe
{
    [SouvenirQuestion("What number was in the {1} position of the initial puzzle in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1)]
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
        for (var ix = 0; ix < 9; ix++)
            if (initState[ix] != 0)
                yield return question(SForgetMe.InitialState, args: [positions[ix]]).Answers(initState[ix].ToString());
    }
}
