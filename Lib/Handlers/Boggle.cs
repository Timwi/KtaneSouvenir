using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBoggle
{
    [SouvenirQuestion("What letter was initially visible on {0}?", ThreeColumns6Answers, ExampleAnswers = ["A", "E", "G", "M", "T", "W"])]
    Letters
}

public partial class SouvenirModule
{
    [SouvenirHandler("boggle", "Boggle", typeof(SBoggle), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessBoggle(ModuleData module)
    {
        var comp = GetComponent(module, "boggle");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var map = GetField<char[,]>(comp, "letterMap").Get(m => m.GetLength(0) != 10 || m.GetLength(1) != 10 ? $"size was {m.GetLength(0)}×{m.GetLength(1)}, expected 10×10" : null);
        var visible = GetField<string>(comp, "visableLetters", isPublic: true).Get(v => v.Length != 4 ? "expected length 4" : null);
        var verOffset = GetIntField(comp, "verOffset").Get(min: 0, max: 6);
        var horOffset = GetIntField(comp, "horOffset").Get(min: 0, max: 6);

        yield return WaitForSolve;

        var letters = new List<string>();
        for (var i = verOffset; i < verOffset + 4; i++)
            for (var j = horOffset; j < horOffset + 4; j++)
                letters.Add(map[i, j].ToString());

        yield return question(SBoggle.Letters).Answers(visible.Select(v => v.ToString()).ToArray(), preferredWrong: letters.ToArray());
    }
}