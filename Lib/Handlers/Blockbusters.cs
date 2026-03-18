using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SBlockbusters
{
    [SouvenirQuestion("Which letter was in the leftmost column at the start of {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y")]
    FirstLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("blockbusters", "Blockbusters", typeof(SBlockbusters), "Espik")]
    [SouvenirManualQuestion("What letters were in the leftmost column at the start?")]
    private IEnumerator<SouvenirInstruction> ProcessBlockbusters(ModuleData module)
    {
        var comp = GetComponent(module, "blockbustersScript");
        var tiles = GetField<Array>(comp, "tiles", isPublic: true).Get(arr => arr.Cast<object>().Any(v => v == null) ? "contains null" : null);
        var selectedLetters = new List<string>();

        IEnumerator retrieveLetters()
        {
            yield return null;
            selectedLetters.Clear();

            for (var i = 0; i < 4; i++)
            {
                var letter = GetField<TextMesh>(tiles.GetValue(i), "containedLetter", isPublic: true).Get();
                selectedLetters.Add(letter.text);
            }
        }

        StartCoroutine(retrieveLetters());

        module.Module.OnStrike += delegate
        {
            StartCoroutine(retrieveLetters());
            return false;
        };

        yield return WaitForSolve;

        yield return question(SBlockbusters.FirstLetters).Answers(selectedLetters.ToArray());
    }
}
