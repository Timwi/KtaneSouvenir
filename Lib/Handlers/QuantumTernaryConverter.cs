using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SQuantumTernaryConverter
{
    [SouvenirQuestion("Which number was shown in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(-265720, -9842)]
    [AnswerGenerator.Integers(9842, 265720)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("quTern", "Quantum Ternary Converter", typeof(SQuantumTernaryConverter), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessQuantumTernaryConverter(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "QTCScript");
        var ints = GetArrayField<int>(comp, "ansints").Get(expectedLength: 2, validator: v => Mathf.Abs(v) is < 9842 or > 265720 ? "Expected range ±[9842, 265720]" : null);
        yield return question(SQuantumTernaryConverter.Number).Answers(ints.Select(i => i.ToString()).ToArray());
    }
}