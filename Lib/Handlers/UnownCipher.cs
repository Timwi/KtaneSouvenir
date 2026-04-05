using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUnownCipher
{
    [Question("What stat appeared on the {1} display when pressing the {2} Unown letter in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 15)]
    Stats
}

public partial class SouvenirModule
{
    [Handler("UnownCipher", "Unown Cipher", typeof(SUnownCipher), "Quinn Wuest")]
    [ManualQuestion("What were the stats on the displays?")]
    private IEnumerator<SouvenirInstruction> ProcessUnownCipher(ModuleData module)
    {
        var comp = GetComponent(module, "UnownCipher");
        yield return WaitForSolve;

        var unowns = GetArrayField<object>(comp, "unown").Get(expectedLength: 5);
        var stats = unowns.Select(u => GetArrayField<int>(u, "stats").Get(expectedLength: 4).ToArray()).ToArray();

        var isShiny = unowns.Select(u => GetField<bool>(u, "shiny").Get()).ToArray();
        var prefWrong = Enumerable.Range(0, 16).Where(x => x % 4 >= 2).Select(x => x.ToString()).ToArray();

        for (int letter = 0; letter < 5; letter++) {
            if (isShiny[letter])
            {
                // Only ask a question for the first stat where the values can only be in prefWrong. The other three stats will be 10.
                yield return question(SUnownCipher.Stats, args: [Ordinal(1), Ordinal(letter + 1)]).Answers(stats[letter][0].ToString(), preferredWrong: prefWrong);
            }

            else
            {
                for (int stat = 0; stat < 4; stat++)
                    yield return question(SUnownCipher.Stats, args: [Ordinal(stat + 1), Ordinal(letter + 1)]).Answers(stats[letter][stat].ToString());
            }
        }
    }
}
