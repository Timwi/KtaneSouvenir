using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SChordQualities
{
    [SouvenirQuestion("Which note was part of the given chord in {0}?", ThreeColumns6Answers, "A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯")]
    Notes
}

public partial class SouvenirModule
{
    [SouvenirHandler("ChordQualities", "Chord Qualities", typeof(SChordQualities), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessChordQualities(ModuleData module)
    {
        var comp = GetComponent(module, "ChordQualities");

        var givenChord = GetField<object>(comp, "givenChord").Get();
        var lights = GetField<Array>(comp, "lights", isPublic: true).Get(v => v.Length != 12 ? "expected length 12" : null);
        var mthSetOutputLight = GetMethod<object>(lights.GetValue(0), "setOutputLight", numParameters: 1, isPublic: true);
        var mthTurnInputLightOff = GetMethod<object>(lights.GetValue(0), "turnInputLightOff", numParameters: 0, isPublic: true);

        yield return WaitForSolve;

        for (var lightIx = 0; lightIx < lights.Length; lightIx++)
        {
            mthSetOutputLight.InvokeOn(lights.GetValue(lightIx), false);
            mthTurnInputLightOff.InvokeOn(lights.GetValue(lightIx));
        }

        var noteNames = GetField<Array>(givenChord, "notes").Get(v => v.Length != 4 ? "expected length 4" : null).Cast<object>().Select(note => note.ToString().Replace("sharp", "♯")).ToArray();
        addQuestions(module, makeQuestion(Question.ChordQualitiesNotes, module, correctAnswers: noteNames));
    }
}