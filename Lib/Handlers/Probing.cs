using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SProbing
{
    [SouvenirQuestion("What was the missing frequency in the {1} wire in {0}?", TwoColumns4Answers, "10Hz", "22Hz", "50Hz", "60Hz", TranslateArguments = [true], Arguments = ["red-white", "yellow-black", "green", "gray", "yellow-red", "red-blue"], ArgumentGroupSize = 1)]
    Frequencies
}

public partial class SouvenirModule
{
    [SouvenirHandler("Probing", "Probing", typeof(SProbing), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessProbing(ModuleData module)
    {
        var comp = GetComponent(module, "ProbingModule");
        yield return WaitForSolve;

        var display = GetField<TextMesh>(comp, "display", isPublic: true).Get();

        // Blank out the display so that the user cannot see the readout for the solution wires
        display.text = "";

        // Prevent the user from interacting with the wires after solving
        foreach (var selectable in GetArrayField<KMSelectable>(comp, "selectables", isPublic: true).Get(expectedLength: 6))
            selectable.OnInteract = delegate { return false; };

        var wireNames = new[] { "red-white", "yellow-black", "green", "gray", "yellow-red", "red-blue" };
        var frequencyDic = new Dictionary<int, string> { { 7, "60Hz" }, { 11, "50Hz" }, { 13, "22Hz" }, { 14, "10Hz" } };
        var wireFrequenciesRaw = GetField<Array>(comp, "mWires").Get(ar => ar.Length != 6 ? "expected length 6" : ar.Cast<int>().Any(v => !frequencyDic.ContainsKey(v)) ? "contains unknown frequency value" : null);
        var wireFrequencies = wireFrequenciesRaw.Cast<int>().Select(val => frequencyDic[val]).ToArray();

        for (var ix = 0; ix < wireFrequencies.Length; ix++)
            yield return question(SProbing.Frequencies, args: [wireNames[ix]]).Answers(wireFrequencies[ix]);
    }
}