using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBrokenGuitarChords
{
    [SouvenirQuestion("What was the displayed chord in {0}?", ThreeColumns6Answers, ExampleAnswers = ["C", "Dm", "F#sus", "Gm7", "A9", "Eadd9"])]
    DisplayedChord,

    [SouvenirQuestion("In which position, from left to right, was the broken string in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 6)]
    MutedString
}

public partial class SouvenirModule
{
    [SouvenirHandler("BrokenGuitarChordsModule", "Broken Guitar Chords", typeof(SBrokenGuitarChords), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessBrokenGuitarChords(ModuleData module)
    {
        var comp = GetComponent(module, "BrokenGuitarChordsModule");
        var chordDisplay = GetField<TextMesh>(comp, "ChordDisplay", isPublic: true).Get();
        var displayedChord = chordDisplay.text;

        yield return WaitForSolve;

        chordDisplay.text = "";

        foreach (var renderer in GetField<MeshRenderer[]>(comp, "FretRenderers", isPublic: true).Get())
            renderer.enabled = false;
        foreach (var renderer in GetField<MeshRenderer[]>(comp, "MuteRenderers", isPublic: true).Get())
            renderer.enabled = false;

        var rootNames = GetStaticField<string[][]>(comp.GetType(), "_noteNames").Get(arr => arr.Length != 12 ? "expected length 12" : null);
        var rootPositions = Enumerable.Range(0, 12).Select(i => i.ToString("00"));
        var qualities = new[] { "", "m", "6", "7", "9", "add9", "m6", "m7", "maj7", "dim", "dim7", "+", "sus" };

        var randomChords = new HashSet<string>();
        while (randomChords.Count < 8)
            randomChords.Add($"{rootPositions.PickRandom()}{qualities.PickRandom()}");
        var possibleAnswers = randomChords.Select(chord => $"{rootNames[int.Parse(chord.Substring(0, 2))].PickRandom()}{chord.Substring(2)}").ToList();
        if (displayedChord.Length > 1 && "#b".Contains(displayedChord[1]))
        {
            var letters = "ABCDEFG";
            var offset = displayedChord[1] == '#' ? 1 : 6;
            possibleAnswers.Remove($"{letters[(letters.IndexOf(displayedChord[0]) + offset) % 7]}b{(displayedChord.Length > 2 ? displayedChord.Substring(2) : "")}");
        }

        var brokenStringPosition = GetIntField(comp, "_brokenString").Get(min: 0, max: 5) + 1;

        yield return question(SBrokenGuitarChords.DisplayedChord).Answers(displayedChord, preferredWrong: possibleAnswers.ToArray());
        yield return question(SBrokenGuitarChords.MutedString).Answers(brokenStringPosition.ToString());
    }
}