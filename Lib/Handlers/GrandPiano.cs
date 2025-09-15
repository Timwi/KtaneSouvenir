using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGrandPiano
{
    [SouvenirQuestion("Which key was part of the {1} set in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G", "1-7")]
    [AnswerGenerator.Strings("AB", "0")]
    [AnswerGenerator.Strings("C", "8")]
    [AnswerGenerator.Strings("CDFGA", "♯", "1-7")]
    [AnswerGenerator.Strings("A", "♯", "0")]
    Key,

    [SouvenirQuestion("Which key was the fifth set in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("DEGAB", "♭", "1-7")]
    [AnswerGenerator.Strings("B", "♭", "0")]
    FinalKey
}

public partial class SouvenirModule
{
    [SouvenirHandler("grandPiano", "Grand Piano", typeof(SGrandPiano), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessGrandPiano(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "grandPianoScript");
        var sets = GetArrayField<int[]>(comp, "Duck").Get(expectedLength: 5, validator: v => v.Length is not 5 ? "Expected length 5" : v.Any(c => c is < -1 or > 87) ? "Expected range [-1, 87]" : null);

        var noteNames = new[] { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };
        string toNote(int note, bool flat = false)
        {
            note += 9;
            var octave = note / 12;
            var name = noteNames[note % 12];
            if (flat && name.Length is 2)
                name = noteNames[note % 12 + 1] + "♭";
            name += octave;
            return name;
        }

        addQuestions(module, sets.Take(4).Select((s, i) =>
            makeQuestion(Question.GrandPianoKey, module,
                correctAnswers: s.Select(n => toNote(n)).ToArray(),
                formatArgs: new[] { Ordinal(i + 1) }))
            .Concat(new[] {
                makeQuestion(Question.GrandPianoFinalKey, module,
                correctAnswers: new[] { toNote(sets[4][4], true) })}));
    }
}