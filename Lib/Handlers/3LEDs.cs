using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S3LEDs
{
    [SouvenirQuestion("What was the initial state of the LEDs in {0} (in reading order)?", TwoColumns4Answers, "off/off/off", "off/off/on", "off/on/off", "off/on/on", "on/off/off", "on/off/on", "on/on/off", "on/on/on", TranslateAnswers = true)]
    InitialState
}

public partial class SouvenirModule
{
    [SouvenirHandler("threeLEDsModule", "3 LEDs", typeof(S3LEDs), "Timwi")]
    private IEnumerator<SouvenirInstruction> Process3LEDs(ModuleData module)
    {
        var comp = GetComponent(module, "ThreeLEDsScript");
        yield return WaitForSolve;

        var initialStates = GetArrayField<bool>(comp, "initialStates").Get(expectedLength: 3);
        yield return question(S3LEDs.InitialState).Answers(initialStates.Select(s => s ? "on" : "off").JoinString("/"));
    }
}