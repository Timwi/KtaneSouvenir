using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDevilishEggs
{
    [SouvenirQuestion("What was the {1} egg’s {2} rotation in {0}?", TwoColumns4Answers, "W90CW", "W180CW", "W270CW", "W360CW", "W90CCW", "W180CCW", "W270CCW", "W360CCW", "T90CW", "T180CW", "T270CW", "T360CW", "T90CCW", "T180CCW", "T270CCW", "T360CCW", TranslateArguments = [true, false], Arguments = ["top", QandA.Ordinal, "bottom", QandA.Ordinal], ArgumentGroupSize = 2)]
    Rotations,

    [SouvenirQuestion("What was the {1} digit in the string of numbers on {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Numbers,

    [SouvenirQuestion("What was the {1} letter in the string of letters on {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Letters
}

public partial class SouvenirModule
{
    [SouvenirHandler("devilishEggs", "Devilish Eggs", typeof(SDevilishEggs), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessDevilishEggs(ModuleData module)
    {
        var comp = GetComponent(module, "devilishEggs");
        var prismTexts = GetArrayField<TextMesh>(comp, "prismTexts", isPublic: true).Get(expectedLength: 3);
        var digits = prismTexts[0].text.Split(' ');
        var letters = prismTexts[1].text.Split(' ');
        if (digits.Length != 8 || digits.Any(str => str.Length != 1 || str[0] < '0' || str[0] > '9'))
            throw new AbandonModuleException($"Expected 8 digits; got {digits.Stringify()}");
        if (letters.Length != 8 || letters.Any(str => str.Length != 1 || str[0] < 'A' || str[0] > 'Z'))
            throw new AbandonModuleException($"Expected 8 letters; got {letters.Stringify()}");

        yield return WaitForSolve;

        var topRotations = GetField<Array>(comp, "topRotations").Get(validator: arr => arr.Length != 6 ? "expected length 6" : null).Cast<object>().Select(rot => rot.ToString()).ToArray();
        var bottomRotations = GetField<Array>(comp, "bottomRotations").Get(validator: arr => arr.Length != 6 ? "expected length 6" : null).Cast<object>().Select(rot => rot.ToString()).ToArray();
        var allRotations = topRotations.Concat(bottomRotations).ToArray();

        var qs = new List<QandA>();
        for (var rotIx = 0; rotIx < 6; rotIx++)
        {
            qs.Add(makeQuestion(Question.DevilishEggsRotations, module, formatArgs: new[] { "top", Ordinal(rotIx + 1) }, correctAnswers: new[] { topRotations[rotIx] }, preferredWrongAnswers: allRotations));
            qs.Add(makeQuestion(Question.DevilishEggsRotations, module, formatArgs: new[] { "bottom", Ordinal(rotIx + 1) }, correctAnswers: new[] { bottomRotations[rotIx] }, preferredWrongAnswers: allRotations));
        }
        for (var ix = 0; ix < 8; ix++)
        {
            qs.Add(makeQuestion(Question.DevilishEggsNumbers, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { digits[ix] }, preferredWrongAnswers: digits));
            qs.Add(makeQuestion(Question.DevilishEggsLetters, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { letters[ix] }, preferredWrongAnswers: letters));
        }
        addQuestions(module, qs);
    }
}