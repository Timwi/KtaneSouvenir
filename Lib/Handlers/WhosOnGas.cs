using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWhosOnGas
{
    [SouvenirQuestion("What was the display in the first phase of the {1} stage on {0}?", ThreeColumns6Answers, ExampleAnswers = ["DISPLAY", "PRESS", "PRESSED", "LAST", "START", "ONE"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("whosOnGas", "Who’s on Gas", typeof(SWhosOnGas), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessWhosOnGas(ModuleData module)
    {
        var comp = GetComponent(module, "WhosOnGasScript");
        var line = GetIntField(comp, "line");
        var stage = GetIntField(comp, "stage");
        var displays = new string[] { "DISPLAY", "PRESS", "PRESSED", "LAST", "START", "ONE", "STRIKES", "TO", "SCREEN", "TWO", "RESET", "DISARMED", "STRIKE" };
        var screens = new string[6];

        while (module.Unsolved)
        {
            var s = stage.Get();
            var l = line.Get();
            screens[s] = displays[l];
            yield return null;
        }
        var qs = new List<QandA>();
        for (var s = 0; s < 3; s++)
        {
            var ix = s * 2;
            qs.Add(makeQuestion(Question.WhosOnGasDisplay, module, formatArgs: new[] { Ordinal(s + 1) }, correctAnswers: new[] { screens[ix] }, preferredWrongAnswers: displays));
        }
        addQuestions(module, qs);
    }
}