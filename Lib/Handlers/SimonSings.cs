using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonSings
{
    [SouvenirQuestion("Which key’s color flashed {1} in the {2} stage of {0}?", ThreeColumns6Answers, "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Flashing
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonSingsModule", "Simon Sings", typeof(SSimonSings), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSings(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSingsModule");
        yield return WaitForSolve;

        var noteNames = new[] { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };
        var flashingColorSequences = GetArrayField<int[]>(comp, "_flashingColors").Get(expectedLength: 3, validator: seq => seq.Any(col => col < 0 || col >= noteNames.Length) ? $"expected range 0–{noteNames.Length - 1}" : null);
        addQuestions(module, flashingColorSequences.SelectMany((seq, stage) => seq.Select((col, ix) => makeQuestion(Question.SimonSingsFlashing, module, formatArgs: new[] { Ordinal(ix + 1), Ordinal(stage + 1) }, correctAnswers: new[] { noteNames[col] }))));
    }
}