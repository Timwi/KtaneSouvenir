using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCursedDoubleOh
{
    [SouvenirQuestion("What was the first digit of the initially displayed number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    InitialPosition
}

public partial class SouvenirModule
{
    [SouvenirHandler("CursedDoubleOhModule", "Cursed Double-Oh", typeof(SCursedDoubleOh), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessCursedDoubleOh(ModuleData module)
    {
        var comp = GetComponent(module, "DoubleOhModule");

        yield return WaitForSolve;

        var firstNumber = GetField<List<int>>(comp, "visitedNumbers").Get().First();
        var firstDigit = (firstNumber / 10).ToString();
        yield return question(SCursedDoubleOh.InitialPosition).Answers(firstDigit);
    }
}