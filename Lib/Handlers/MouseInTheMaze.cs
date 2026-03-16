using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMouseInTheMaze
{
    [SouvenirQuestion("What color was the torus in {0}?", TwoColumns4Answers, "white", "green", "blue", "yellow", TranslateAnswers = true)]
    Torus
}

public partial class SouvenirModule
{
    [SouvenirHandler("MouseInTheMaze", "Mouse In The Maze", typeof(SMouseInTheMaze), "Timwi")]
    [SouvenirManualQuestion("What color was the torus?")]
    private IEnumerator<SouvenirInstruction> ProcessMouseInTheMaze(ModuleData module)
    {
        var comp = GetComponent(module, "MouseInTheMazeModule");

        yield return WaitForActivate;

        var torusColor = GetIntField(comp, "_torusColor").Get(min: 0, max: 3);
        yield return torusColor is not < 0 and not > 3
            ? WaitForSolve
            : throw new AbandonModuleException($"Unexpected color (torus={torusColor};)");
        yield return question(SMouseInTheMaze.Torus).Answers(new[] { "white", "green", "blue", "yellow" }[torusColor]);
    }
}
