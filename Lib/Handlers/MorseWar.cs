using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMorseWar
{
    [SouvenirQuestion("What code was transmitted in {0}?", ThreeColumns6Answers, "ABR", "RBS", "SVR", "ZUX", "ZAQ", "MOI", "OPA", "VZQ", "XRP", "OLL", "AIR", "RHG", "MJN", "VTT", "XZS", "SUN")]
    Code,

    [SouvenirQuestion("What were the LEDs in the {1} row in {0} (1\u00a0=\u00a0on, 0\u00a0=\u00a0off)?", ThreeColumns6Answers, "1100", "1010", "1001", "0110", "0101", "0011", TranslateArguments = [true], Arguments = ["bottom", "middle", "top"], ArgumentGroupSize = 1)]
    Leds
}

public partial class SouvenirModule
{
    [SouvenirHandler("MorseWar", "Morse War", typeof(SMorseWar), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMorseWar(ModuleData module)
    {
        var comp = GetComponent(module, "MorseWar");
        yield return WaitForSolve;

        var wordTable = GetStaticField<string[]>(comp.GetType(), "WordTable").Get();
        var rowTable = GetStaticField<string[]>(comp.GetType(), "RowTable").Get(ar => ar.Length != 6 ? "expected length 6" : null);
        var wordNum = GetIntField(comp, "wordNum").Get(min: 0, max: wordTable.Length - 1);
        var lights = GetField<string>(comp, "lights").Get(str => str.Length != 3 ? "expected length 3" : str.Any(ch => ch is < '1' or > '6') ? "expected characters 1–6" : null);

        var qs = new List<QandA>() { makeQuestion(Question.MorseWarCode, module, correctAnswers: new[] { wordTable[wordNum].ToUpperInvariant() }) };
        var rowNames = new[] { "bottom", "middle", "top" };
        for (var i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.MorseWarLeds, module, formatArgs: new[] { rowNames[i] }, correctAnswers: new[] { rowTable[lights[i] - '1'] }));

        addQuestions(module, qs);
    }
}