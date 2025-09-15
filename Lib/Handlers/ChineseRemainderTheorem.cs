using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SChineseRemainderTheorem
{
    [SouvenirQuestion("Which equation was used in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.ChineseRemainderTheorem]
    Equations
}

public partial class SouvenirModule
{
    [SouvenirHandler("ChineseRemainderTheoremModule", "Chinese Remainder Theorem", typeof(SChineseRemainderTheorem), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessChineseRemainderTheorem(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "ChineseRemainderTheoremScript");
        var moduli = GetListField<int>(comp, "_moduli").Get(minLength: 4, maxLength: 8, validator: v => v is < 2 or > 51 ? "Out of range 2–51" : null);
        var remainders = GetListField<int>(comp, "_remainder").Get(expectedLength: moduli.Count, validator: v => v is < 0 or > 50 ? "Out of range 0–50" : null);
        if (moduli.Select((m, i) => remainders[i] >= m).Any(x => x))
            throw new AbandonModuleException($"A remainder was bigger than its corresponding modulus: {moduli.Select((m, i) => $"N % {m} = {remainders[i]}").JoinString("; ")}");

        static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        var remove = moduli
            .Select((m, i) => (m, i))
            .Where(t => moduli.Any(m => m != t.m && GCD(t.m, m) != 1))
            .Select(t => t.i)
            .ToArray();
        if (remove.Length == moduli.Count)
            yield return legitimatelyNoQuestion(module, "Every modulus was noncoprime with at least one other modulus.");

        var right = moduli
            .Select((m, i) => $"N % {m} = {remainders[i]}")
            .Where((_, i) => !remove.Contains(i))
            .ToArray();

        yield return question(SChineseRemainderTheorem.Equations).Answers(right);
    }
}