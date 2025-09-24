using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBinaryLEDs
{
    [SouvenirQuestion("At which numeric value did you cut the correct wire in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 31)]
    Value
}

public partial class SouvenirModule
{
    [SouvenirHandler("BinaryLeds", "Binary LEDs", typeof(SBinaryLEDs), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessBinaryLEDs(ModuleData module)
    {
        var comp = GetComponent(module, "BinaryLeds");
        var fldSequences = GetField<int[,]>(comp, "sequences");
        var fldSequenceIndex = GetIntField(comp, "sequenceIndex");
        var fldColors = GetArrayField<int>(comp, "colorIndices");
        var fldSolutions = GetField<int[,]>(comp, "solutions");
        var fldSolved = GetField<bool>(comp, "solved");
        var fldBlinkDelay = GetField<float>(comp, "blinkDelay");
        var mthGetIndexFromTime = GetMethod<int>(comp, "GetIndexFromTime", 2);

        var answer = -1;
        var wires = GetArrayField<KMSelectable>(comp, "wires", isPublic: true).Get(expectedLength: 3);

        foreach (var i in Enumerable.Range(0, wires.Length))    // Do not use ‘for’ loop as the loop variable is captured by a lambda
        {
            var oldInteract = wires[i].OnInteract;
            wires[i].OnInteract = delegate
            {
                wires[i].OnInteract = oldInteract;  // Restore original interaction, so that this can only ever be called once per wire.
                var wasSolved = fldSolved.Get();    // Get this before calling oldInteract()
                var seqIx = fldSequenceIndex.Get();
                var numIx = mthGetIndexFromTime.Invoke(Time.time, fldBlinkDelay.Get());
                var colors = fldColors.Get(nullAllowed: true);  // We cannot risk throwing an exception during the module’s button handler
                var solutions = fldSolutions.Get(nullAllowed: true);
                var result = oldInteract();

                if (wasSolved)
                    return result;

                if (colors == null || colors.Length <= i)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning Binary LEDs because ‘colors’ array has unexpected length ({(colors == null ? "null" : colors.Length.ToString())}).");
                    return result;
                }

                if (solutions == null || solutions.GetLength(0) <= seqIx || solutions.GetLength(1) <= colors[i])
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning Binary LEDs because ‘solutions’ array has unexpected lengths ({(solutions == null ? "null" : solutions.GetLength(0).ToString())}, {(solutions == null ? "null" : solutions.GetLength(1).ToString())}).");
                    return result;
                }

                // Ignore if this wasn’t a solve
                if (solutions[seqIx, colors[i]] != numIx)
                    return result;

                // Find out which value is displayed
                var sequences = fldSequences.Get(nullAllowed: true);

                if (sequences == null || sequences.GetLength(0) <= seqIx || sequences.GetLength(1) <= numIx)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning Binary LEDs because ‘sequences’ array has unexpected lengths ({(sequences == null ? "null" : sequences.GetLength(0).ToString())}, {(sequences == null ? "null" : sequences.GetLength(1).ToString())}).");
                    return result;
                }

                answer = sequences[seqIx, numIx];
                return result;
            };
        }

        yield return WaitForSolve;

        if (answer == -1)
            legitimatelyNoQuestion(module, "The module auto-solved after all three wires were cut incorrectly.");
        else
            yield return question(SBinaryLEDs.Value).Answers(answer.ToString());
    }
}
