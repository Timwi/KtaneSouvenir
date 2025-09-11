using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHexOS
{
    [SouvenirQuestion("What was the deciphered phrase in {0}?", ThreeColumns6Answers, ExampleAnswers = ["a maze", "someda", "but i ", "they h", "shorn o", "more s", "if onl", "grew b"])]
    OctCipher,
    
    [SouvenirQuestion("What were the deciphered letters in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("2* A-Z")]
    Cipher,
    
    [SouvenirQuestion("What were the rhythm values in {0}?", ThreeColumns6Answers, ExampleAnswers = ["0001", "0012", "0123", "1230", "2300", "3000"])]
    Sum,
    
    [SouvenirQuestion("What was the {1} 3-digit number cycled by the screen in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 999, "000")]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("hexOS", "hexOS", typeof(SHexOS), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessHexOS(ModuleData module)
    {
        var comp = GetComponent(module, "HexOS");
        yield return WaitForSolve;

        const string validCharacters = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var validPhrases = new string[24] { "a maze with edges like their knives", "someday ill be the shape they want me to be", "but i dont know how much more theyll wake away before theyre satisfied", "they have sliced away my flesh", "shorn of unsightly limbs and organs", "more stitch and scar than human", "if only marble", "grew back so quickly", "they have stolen away my spirit", "memories scattered into the slipstream", "i have no idea who i used to be", "i can only guess", "what they will make me", "they found me in my lowest days", "breathed life back into my frozen body", "promising a more beautiful future", "then i discovered", "what they really wanted", "they pulled me into their vortex", "and i saw my future reflected in their eyes", "a shimmering halo of impossible dreams", "void of my self", "it was", "perfect" };

        var octOS = GetField<bool>(comp, "solvedInOctOS").Get();
        var decipher = GetField<char[]>(comp, "decipher").Get(arr => arr.Length is not 2 and not 6 ? "expected length 2 or 6" : arr.Any(ch => !validCharacters.Contains(char.ToUpperInvariant(ch))) ? "expected characters A–Z or space" : null);
        var screen = GetField<string>(comp, "screen").Get(s => s.Length != 30 ? "expected length 30" : s.Any(ch => !char.IsDigit(ch)) ? "expected only digits" : null);
        var sum = GetField<string>(comp, "sum").Get(s => s.Length != 4 ? "expected length 4" : s.Any(ch => ch is not '0' and not '1' and not '2' and not '3') ? "expected only characters 0–3" : null);

        var qs = new List<QandA>();
        var cipherWrongAnswers = octOS ? validPhrases.SelectMany(str => Enumerable.Range(0, str.Length - 6).Select(ix => str.Substring(ix, 6))).ToArray() : validCharacters.SelectMany(c1 => validCharacters.Select(c2 => string.Concat(c1, c2))).ToArray();

        var wrongAnswers = octOS
            // Generate every combination of 0, 1, 2, & 3 so long as the left two numbers don’t match the right (3031 is valid but 3131 is not)
            ? Enumerable.Range(0, 256).Where(i => i / 16 != i % 16).Select(i => new[] { i / 64, (i / 16) % 4, (i / 4) % 4, i % 4 }.JoinString()).ToArray()
            // Generate every combination of 0, 1, & 2 so long as the left two numbers don’t match the right (2021 is valid but 2121 is not)
            : Enumerable.Range(0, 81).Where(i => i / 9 != i % 9).Select(i => new[] { i / 27, (i / 9) % 3, (i / 3) % 3, i % 3 }.JoinString()).ToArray();

        qs.Add(octOS
            ? makeQuestion(Question.HexOSOctCipher, module, correctAnswers: new[] { decipher.JoinString() }, preferredWrongAnswers: cipherWrongAnswers)
            : makeQuestion(Question.HexOSCipher, module, correctAnswers: new[] { decipher.JoinString(), decipher.Reverse().JoinString() }, preferredWrongAnswers: cipherWrongAnswers));
        qs.Add(makeQuestion(Question.HexOSSum, module, correctAnswers: new[] { sum }, preferredWrongAnswers: wrongAnswers));
        for (var offset = 0; offset < 10; offset++)
            qs.Add(makeQuestion(Question.HexOSScreen, module, formatArgs: new[] { Ordinal(offset + 1) }, correctAnswers: new[] { screen.Substring(offset * 3, 3) }));
        addQuestions(module, qs);
    }
}