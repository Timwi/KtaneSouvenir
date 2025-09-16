using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SWireSequence
{
    [SouvenirQuestion("How many {1} wires were there in {0}?", TwoColumns4Answers, TranslateArguments = [true], Arguments = ["red", "blue", "black"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    ColorCount
}

public partial class SouvenirModule
{
    [SouvenirHandler("WireSequence", "Wire Sequence", typeof(SWireSequence), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessWireSequence(ModuleData module)
    {
        var comp = GetComponent(module, "WireSequenceComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        module.SolveIndex = _modulesSolved.IncSafe("WireSequence");

        var wireSequence = GetField<IEnumerable>(comp, "wireSequence").Get();

        var counts = new int[3];
        var typeWireConfiguration = wireSequence.GetType().GetGenericArguments()[0];
        var fldNoWire = GetFieldFromType<bool>(typeWireConfiguration, "NoWire", isPublic: true);
        var fldColor = GetFieldFromType<object>(typeWireConfiguration, "Color", isPublic: true);

        foreach (var item in wireSequence.Cast<object>().Take(12))
            if (!fldNoWire.GetFrom(item))
                counts[(int) fldColor.GetFrom(item)]++;
        for (var color = 0; color < 3; color++)
        {
            var preferredWrongAnswers = new string[4];
            for (var i = 0; i < 3; i++)
                preferredWrongAnswers[i] = counts[i].ToString();
            preferredWrongAnswers[3] = (counts[color] == 0 ? 1 : counts[color] - 1).ToString();
            yield return question(SWireSequence.ColorCount, args: [new[] { "black", "blue", "red" }[color]]).Answers(counts[color].ToString(), preferredWrong: preferredWrongAnswers);
        }
    }
}