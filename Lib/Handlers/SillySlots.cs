using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSillySlots
{
    [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", TwoColumns4Answers, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Question
}

public partial class SouvenirModule
{
    [SouvenirHandler("SillySlots", "Silly Slots", typeof(SSillySlots), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSillySlots(ModuleData module)
    {
        var comp = GetComponent(module, "SillySlots");
        yield return WaitForSolve;

        var prevSlots = GetField<IList>(comp, "mPreviousSlots").Get(lst => lst.Cast<object>().Any(obj => obj is not Array ar || ar.Length != 3) ? "expected arrays of length 3" : null);
        if (prevSlots.Count < 2)
            // Legitimate: first stage was a keep already
            yield return legitimatelyNoQuestion(module, "There was only one stage.");

        var testSlot = ((Array) prevSlots[0]).GetValue(0);
        var fldShape = GetField<object>(testSlot, "shape", isPublic: true);
        var fldColor = GetField<object>(testSlot, "color", isPublic: true);

        var qs = new List<QandA>();
        // Skip the last stage because if the last action was Keep, it is still visible on the module
        for (var stage = 0; stage < prevSlots.Count - 1; stage++)
        {
            var slotStrings = ((Array) prevSlots[stage]).Cast<object>().Select(obj => (fldColor.GetFrom(obj).ToString() + " " + fldShape.GetFrom(obj).ToString()).ToLowerInvariant()).ToArray();
            for (var slot = 0; slot < slotStrings.Length; slot++)
                qs.Add(makeQuestion(Question.SillySlots, module, formatArgs: new[] { Ordinal(slot + 1), Ordinal(stage + 1) }, correctAnswers: new[] { slotStrings[slot] }, preferredWrongAnswers: slotStrings));
        }
        addQuestions(module, qs);
    }
}