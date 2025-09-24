using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SButtonage
{
    [SouvenirQuestion("How many {1} buttons were there on {0}?", ThreeColumns6Answers, ArgumentGroupSize = 1, TranslateArguments = [true], Arguments = ["red", "green", "orange", "blue", "pink", "white", "black", "white-bordered", "pink-bordered", "gray-bordered", "red-bordered", "“P”", "special"])]
    [AnswerGenerator.Integers(0, 64)]
    Buttons
}

public partial class SouvenirModule
{
    [SouvenirHandler("buttonageModule", "Buttonage", typeof(SButtonage), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessButtonage(ModuleData module)
    {
        yield return WaitForSolve;

        string[] colorLog = { "K", "W", "B", "G", "O", "I", "R", "Y", "A" };
        var comp = GetComponent(module, "ButtonageScript");
        var buttonColors = GetArrayField<int>(comp, "chosenBtns").Get(expectedLength: 64, validator: v => v is < 0 or > 8 ? "Expected range [0, 8]" : null);
        var borderColors = GetArrayField<int>(comp, "chosenBorders").Get(expectedLength: 64, validator: v => v is 1 or 5 or 6 or 8 ? null : "Expected 1, 5, 6, or 8");
        var special = GetIntField(comp, "specialCt").Get(0, 64);
        var p = GetIntField(comp, "pCt").Get(0, 64);

        var colorNums = new Dictionary<string, int>()
        {
            { "black", 0 },
            { "blue", 2 },
            { "green", 3 },
            { "orange", 4 },
            { "pink", 5 },
            { "red", 6 },
            { "yellow", 7 },
            { "gray", 8 }
        };

        var questions = Ut.NewList(
            ("white", buttonColors.Count(x => x is 1), 12, 11),
            ("white-bordered", borderColors.Count(x => x is 1), 15, 8),
            ("pink-bordered", borderColors.Count(x => x is 5), 15, 8),
            ("red-bordered", borderColors.Count(x => x is 6), 21, 13),
            ("gray-bordered", borderColors.Count(x => x is 8), 15, 8),
            ("“P”", p, 19, 9),
            ("special", special, 6, 5));
        questions.AddRange(colorNums.Select(c => (c.Key, buttonColors.Count(x => x == c.Value), 6, 6)));

        // Getting a number like 64 (while technically possible) is absurdly unlikely, so limit most of the answers accordingly
        static IEnumerable<string> reasonableRandom(int mu, int deviation, int real)
        {
            if (mu + deviation < real || mu - deviation > real)
                mu = real;
            var count = Rnd.Range(12, 18);
            for (var i = 0; i < count; i++)
                yield return Rnd.Range(Mathf.Max(mu - deviation, 0), Mathf.Min(mu + deviation, 65)).ToString();
        }

        foreach (var t in questions)
            yield return question(SButtonage.Buttons, args: [t.Item1]).Answers(t.Item2.ToString(), preferredWrong: reasonableRandom(t.Item3, t.Item4, t.Item2).ToArray());
    }
}
