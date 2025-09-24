using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SEquationsX
{
    [SouvenirQuestion("What was the displayed symbol in {0}?", ThreeColumns6Answers, "H(T)", "P", "\u03C7", "\u03C9", "Z(T)", "\u03C4", "\u03BC", "\u03B1", "K")]
    Symbols
}

public partial class SouvenirModule
{
    [SouvenirHandler("equationsXModule", "Equations X", typeof(SEquationsX), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessEquationsX(ModuleData module)
    {
        var comp = GetComponent(module, "EquationsScript");

        yield return WaitForActivate;

        var symbol = GetField<GameObject>(comp, "symboldisplay", isPublic: true).Get().GetComponentInChildren<TextMesh>().text;

        if (!new[] { "H(T)", "R", "c", "w", "Z(T)", "t", "m", "a", "K" }.Contains(symbol))
            throw new AbandonModuleException($"‘symbol’ has an unexpected character: {symbol}");

        // Equations X uses symbols that don’t translate well to Souvenir. This switch statement is used to correctly translate the answer.
        switch (symbol)
        {
            case "c":
                symbol = "χ";
                break;
            case "R":
                symbol = "P";
                break;
            case "w":
                symbol = "ω";
                break;
            case "t":
                symbol = "τ";
                break;
            case "m":
                symbol = "μ";
                break;
            case "a":
                symbol = "α";
                break;
        }

        yield return WaitForSolve;

        yield return question(SEquationsX.Symbols).Answers(symbol);
    }
}