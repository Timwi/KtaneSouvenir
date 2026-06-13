using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHexOS
{
    [Question("What was the deciphered phrase in {0}?", ThreeColumns6Answers, ExampleAnswers = ["a maze", "someda", "but i ", "they h", "shorn o", "more s", "if onl", "grew b"])]
    OctCipher,

    [Question("What were the deciphered letters in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("2* A-Z")]
    Cipher,

    [Question("Which rhythm value was present in {0}?", ThreeColumns6Answers, ExampleAnswers = ["0000", "0001", "0010", "0011", "0100", "0101"])]
    Rhythm,

    [Question("What was the {1} 3-digit number cycled by the screen in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 999, "000")]
    Screen
}

public partial class SouvenirModule
{
    [Handler("hexOS", "hexOS", typeof(SHexOS), "Emik")]
    [ManualQuestion("What were the deciphered letters or phrase?")]
    [ManualQuestion("What were the 3-digit numbers cycled by the screen?")]
    [ManualQuestion("What were the rhythm values?")]
    private IEnumerator<SouvenirInstruction> ProcessHexOS(ModuleData module)
    {
        var comp = GetComponent(module, "HexOS");
        yield return WaitForSolve;

        const string validCharacters = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var validPhrases = new string[24] { "a maze with edges like their knives", "someday ill be the shape they want me to be", "but i dont know how much more theyll wake away before theyre satisfied", "they have sliced away my flesh", "shorn of unsightly limbs and organs", "more stitch and scar than human", "if only marble", "grew back so quickly", "they have stolen away my spirit", "memories scattered into the slipstream", "i have no idea who i used to be", "i can only guess", "what they will make me", "they found me in my lowest days", "breathed life back into my frozen body", "promising a more beautiful future", "then i discovered", "what they really wanted", "they pulled me into their vortex", "and i saw my future reflected in their eyes", "a shimmering halo of impossible dreams", "void of my self", "it was", "perfect" };

        var octOS = GetField<bool>(comp, "solvedInOctOS").Get();
        var decipher = GetField<char[]>(comp, "decipher").Get(arr => arr.Length is not 2 and not 6 ? "expected length 2 or 6" : arr.Any(ch => !validCharacters.Contains(char.ToUpperInvariant(ch))) ? "expected characters A–Z or space" : null);
        var screen = GetField<string>(comp, "screen").Get(s => s.Length != 30 ? "expected length 30" : s.Any(ch => !char.IsDigit(ch)) ? "expected only digits" : null);
        var hexRhythms = GetArrayField<byte>(comp, "_rhythms").Get(expectedLength: 2);
        var octRhythms = GetArrayField<byte>(comp, "_octRhythms").Get(expectedLength: 2);
        var cipherWrongAnswers = octOS ? validPhrases.SelectMany(str => Enumerable.Range(0, str.Length - 6).Select(ix => str.Substring(ix, 6))).ToArray() : validCharacters.SelectMany(c1 => validCharacters.Select(c2 => string.Concat(c1, c2))).ToArray();

        var allRhythms = octOS
            ? new[] { "00", "01", "02", "03", "10", "11", "12", "13", "20", "21", "22", "23", "30", "31", "32", "33" }
            : new[] { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };

        yield return octOS
            ? question(SHexOS.OctCipher).Answers(decipher.JoinString(), preferredWrong: cipherWrongAnswers)
            : question(SHexOS.Cipher).Answers([decipher.JoinString(), decipher.Reverse().JoinString()], preferredWrong: cipherWrongAnswers);
        yield return octOS
            ? question(SHexOS.Rhythm).Answers(octRhythms.Select(x => allRhythms[x]).ToArray(), all: allRhythms)
            : question(SHexOS.Rhythm).Answers(hexRhythms.Select(x => allRhythms[x]).ToArray(), all: allRhythms);
        for (var offset = 0; offset < 10; offset++)
            yield return question(SHexOS.Screen, args: [Ordinal(offset + 1)]).Answers(screen.Substring(offset * 3, 3));
    }
}
