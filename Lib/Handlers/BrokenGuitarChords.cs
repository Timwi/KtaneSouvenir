using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBrokenGuitarChords
{
    [Question("What was the displayed chord in {0}?", ThreeColumns6Answers, ExampleAnswers = ["C", "Dm", "F#sus", "Gm7", "A9", "Eadd9"])]
    QDisplayedChord,

    [Question("In which position, from left to right, was the broken string in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 6)]
    QMutedString,

    [Discriminator("the Broken Guitar Chords where the displayed chord was {0}", Arguments = ["C", "Dm", "F#sus", "Gm7", "A9", "Eadd9"], ArgumentGroupSize = 1)]
    DDisplayedChord,

    [Discriminator("the Broken Guitar Chords where string {0} (from left to right) was broken", Arguments = ["1", "2", "3", "4", "5", "6"], ArgumentGroupSize = 1)]
    DMutedString,
}

public partial class SouvenirModule
{
    [Handler("BrokenGuitarChordsModule", "Broken Guitar Chords", typeof(SBrokenGuitarChords), "Kuro")]
    [ManualQuestion("What was the displayed chord?")]
    [ManualQuestion("Which string was broken?")]
    private IEnumerator<SouvenirInstruction> ProcessBrokenGuitarChords(ModuleData module)
    {
        var comp = GetComponent(module, "BrokenGuitarChordsModule");
        var chordDisplay = GetField<TextMesh>(comp, "ChordDisplay", isPublic: true).Get();
        var displayedChordText = chordDisplay.text;

        yield return WaitForSolve;

        chordDisplay.text = "";

        foreach (var renderer in GetField<MeshRenderer[]>(comp, "FretRenderers", isPublic: true).Get())
            renderer.enabled = false;
        foreach (var renderer in GetField<MeshRenderer[]>(comp, "MuteRenderers", isPublic: true).Get())
            renderer.enabled = false;

        var rootNames = GetStaticField<string[][]>(comp.GetType(), "_noteNames").Get(arr => arr.Length != 12 ? "expected length 12" : null);
        var qualities = new[] { "", "m", "6", "7", "9", "add9", "m6", "m7", "maj7", "dim", "dim7", "+", "sus" };

        var possibleAnswers = new HashSet<string>();
        while (possibleAnswers.Count < 8)
            if ((note: Random.Range(0, 12), quality: qualities.PickRandom()) is { } chord && rootNames[chord.note].All(root => root + chord.quality != displayedChordText))
                possibleAnswers.Add($"{rootNames[chord.note].PickRandom()}{chord.quality}");
        var brokenStringPosition = GetIntField(comp, "_brokenString").Get(min: 0, max: 5) + 1;

        yield return new Discriminator(SBrokenGuitarChords.DDisplayedChord, "chord", displayedChordText, args: [displayedChordText]);
        yield return question(SBrokenGuitarChords.QDisplayedChord).AvoidDiscriminators(SBrokenGuitarChords.DDisplayedChord).Answers(displayedChordText, preferredWrong: possibleAnswers.ToArray());

        yield return new Discriminator(SBrokenGuitarChords.DMutedString, "string", brokenStringPosition, args: [brokenStringPosition.ToString()]);
        yield return question(SBrokenGuitarChords.QMutedString).AvoidDiscriminators(SBrokenGuitarChords.DMutedString).Answers(brokenStringPosition.ToString());
    }
}
