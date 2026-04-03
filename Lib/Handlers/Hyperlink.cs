using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHyperlink
{
    [SouvenirQuestion("What was the {1} character of the hyperlink in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "_", "-", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Characters
}

public partial class SouvenirModule
{
    [SouvenirHandler("hyperlink", "Hyperlink", typeof(SHyperlink), "Espik", AddThe = true)]
    [SouvenirManualQuestion("What was the hyperlink?")]
    private IEnumerator<SouvenirInstruction> ProcessHyperlink(ModuleData module)
    {
        var comp = GetComponent(module, "hyperlinkScript");
        yield return WaitForSolve;

        var hyperlink = GetField<string>(comp, "selectedString").Get();

        string baseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-";
        string ambiguous = "1Il";
        for (int i = 0; i < 11; i++)
        {
            var ch = hyperlink[i];
            var alphabet = baseAlphabet;

            foreach (var a in ambiguous)
                if (a != ch)
                    alphabet = alphabet.Replace(a.ToString(), "");

            yield return question(SHyperlink.Characters, args: [Ordinal(i + 1)]).Answers(hyperlink[i].ToString(), all: alphabet.Select(i => i.ToString()).ToArray());
        }
    }
}