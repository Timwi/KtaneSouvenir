using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSymbolCycle
{
    [SouvenirQuestion("How many symbols were cycling on the {1} screen in {0}?", TwoColumns4Answers, "2", "3", "4", "5", TranslateArguments = [true], Arguments = ["left", "right"], ArgumentGroupSize = 1)]
    SymbolCounts
}

public partial class SouvenirModule
{
    [SouvenirHandler("SymbolCycleModule", "Symbol Cycle", typeof(SSymbolCycle), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessSymbolCycle(ModuleData module)
    {
        var comp = GetComponent(module, "SymbolCycleModule");
        var fldCycles = GetArrayField<int[]>(comp, "_cycles");
        var fldState = GetField<object>(comp, "_state");

        int[][] cycles = null;
        while (fldState.Get().ToString() != "Solved")
        {
            cycles = fldCycles.Get(expectedLength: 2, validator: x => x.Length is < 2 or > 5 ? "expected length 2–5" : null);

            while (fldState.Get().ToString() == "Cycling")
                yield return new WaitForSeconds(0.1f);

            while (fldState.Get().ToString() is "Retrotransphasic" or "Anterodiametric")
                yield return new WaitForSeconds(0.1f);
        }

        yield return cycles == null ? throw new AbandonModuleException("No cycles.") : (YieldInstruction) WaitForSolve;
        addQuestions(module, new[] { "left", "right" }.Select((screen, ix) => makeQuestion(Question.SymbolCycleSymbolCounts, module, formatArgs: new[] { screen }, correctAnswers: new[] { cycles[ix].Length.ToString() })));
    }
}