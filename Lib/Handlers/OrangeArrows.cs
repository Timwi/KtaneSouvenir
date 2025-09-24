using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOrangeArrows
{
    [SouvenirQuestion("What was the {1} arrow on the display of the {2} stage of {0}?", TwoColumns4Answers, "Up", "Right", "Down", "Left", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Sequences
}

public partial class SouvenirModule
{
    [SouvenirHandler("orangeArrowsModule", "Orange Arrows", typeof(SOrangeArrows), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessOrangeArrows(ModuleData module)
    {
        var comp = GetComponent(module, "OrangeArrowsScript");
        var fldMoves = GetArrayField<string>(comp, "moves");
        var fldStage = GetIntField(comp, "stage");

        // The module does not modify the arrays; it instantiates a new one for each stage.
        var correctMoves = new string[3][];

        var buttons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get();
        var prevButtonInteracts = buttons.Select(b => b.OnInteract).ToArray();
        for (var i = 0; i < buttons.Length; i++)
        {
            var prevInteract = prevButtonInteracts[i];
            buttons[i].OnInteract = delegate
            {
                var ret = prevInteract();
                var st = fldStage.Get();
                if (st is < 1 or > 3)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning Orange Arrows because ‘stage’ was out of range: {st}.");
                    correctMoves = null;
                    for (var j = 0; j < buttons.Length; j++)
                        buttons[j].OnInteract = prevButtonInteracts[j];
                }
                else
                {
                    // We need to capture the array at each button press because the user might get a strike and the array might change.
                    // Avoid throwing an exception within the button handler
                    correctMoves[st - 1] = fldMoves.Get(nullAllowed: true);
                }
                return ret;
            };
        }

        yield return WaitForSolve;

        if (correctMoves == null)   // an error message has already been output
            yield break;

        for (var i = 0; i < buttons.Length; i++)
            buttons[i].OnInteract = prevButtonInteracts[i];

        var directions = new[] { "UP", "RIGHT", "DOWN", "LEFT" };
        if (correctMoves.Any(arr => arr == null || arr.Any(dir => !directions.Contains(dir))))
            throw new AbandonModuleException($"One of the move arrays has an unexpected value: [{correctMoves.Select(arr => arr == null ? "null" : $"[{arr.JoinString(", ")}]").JoinString(", ")}].");
        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                yield return question(SOrangeArrows.Sequences, args: [Ordinal(j + 1), Ordinal(i + 1)]).Answers(correctMoves[i][j].Substring(0, 1) + correctMoves[i][j].Substring(1).ToLowerInvariant());
    }
}