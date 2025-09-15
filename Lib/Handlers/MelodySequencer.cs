using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMelodySequencer
{
    [SouvenirQuestion("Which slot contained part #{1} at the start of {0}?", ThreeColumns6Answers, Arguments = ["1", "2"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 8)]
    Parts,

    [SouvenirQuestion("Which part was in slot #{1} at the start of {0}?", ThreeColumns6Answers, Arguments = ["1", "2"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 8)]
    Slots
}

public partial class SouvenirModule
{
    [SouvenirHandler("melodySequencer", "Melody Sequencer", typeof(SMelodySequencer), "Goofy")]
    private IEnumerator<SouvenirInstruction> ProcessMelodySequencer(ModuleData module)
    {
        var comp = GetComponent(module, "MelodySequencerScript");

        var parts = GetArrayField<int[]>(comp, "parts").Get(expectedLength: 8);  // the 8 parts in their “correct” order
        var moduleParts = GetArrayField<int[]>(comp, "moduleParts").Get(expectedLength: 8, nullContentAllowed: true);      // the parts as assigned to the slots
        var partsPerSlot = Enumerable.Range(0, 8).Select(slot => parts.IndexOf(p => ReferenceEquals(p, moduleParts[slot]))).ToArray();
        Debug.Log($"<Souvenir #{_moduleId}> Melody Sequencer: parts are: [{partsPerSlot.JoinString(", ")}].");

        yield return WaitForSolve;
        var givenSlots = Enumerable.Range(0, partsPerSlot.Length).Where(slot => partsPerSlot[slot] != -1).Select(slot => (slot + 1).ToString()).ToArray();
        var givenParts = partsPerSlot.Where(part => part != -1).Select(part => (part + 1).ToString()).ToArray();
        for (var i = 0; i < partsPerSlot.Length; i++)
        {
            if (partsPerSlot[i] != -1)
            {
                yield return question(SMelodySequencer.Parts, args: [(partsPerSlot[i] + 1).ToString()]).Answers((i + 1).ToString(), preferredWrong: givenSlots);
                yield return question(SMelodySequencer.Slots, args: [(i + 1).ToString()]).Answers((partsPerSlot[i] + 1).ToString(), preferredWrong: givenParts);
            }
        }
    }
}