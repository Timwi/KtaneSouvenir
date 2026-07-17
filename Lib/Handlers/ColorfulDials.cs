using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SColorfulDials
{
    [Question("What number was on the {1} display when the dials were in their initial calculated configurations in {0}?", ThreeColumns6Answers, Arguments = ["left", "middle", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(0, 99, "00")]
    QNumber,

    [Question("What color was the number on the {1} display when the dials were in their initial calculated configurations in {0}?", ThreeColumns6Answers, "red", "orange", "yellow", "green", "cyan", "blue", "magenta", "purple", Arguments = ["left", "middle", "right"], ArgumentGroupSize = 1, TranslateArguments = [true], TranslateAnswers = true)]
    [AnswerGenerator.Integers(0, 99, "00")]
    QColor,

    [Question("What was the {1} digit on the large display in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QLargeDisplayDigit,

    [Question("What color was the {1} digit on the large display in {0}?", ThreeColumns6Answers, "red", "orange", "yellow", "green", "cyan", "blue", "magenta", "purple", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QLargeDisplayColor,

    [Question("Which color was {1} in the color strip in {0}?", ThreeColumns6Answers, "red", "orange", "yellow", "green", "cyan", "blue", "magenta", "purple", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QColorOrder,

    [Discriminator("the Colorful Dials where {0} was on the {1} display when the dials were in their initial calculated configurations", Arguments = ["47", "left", "82", "middle", "69", "right"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DNumber,

    [Discriminator("the Colorful Dials where the number on the {1} display when the dials were in their initial calculated configurations was {0}", Arguments = ["red", "left", "orange", "middle", "yellow", "right", "green", "left", "cyan", "middle", "blue", "right", "magenta", "left", "purple", "middle"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    DColor,

    [Discriminator("the Colorful Dials where the {0} digit on the large display was {1}", Arguments = [QandA.Ordinal, "4", QandA.Ordinal, "8", QandA.Ordinal, "9"], ArgumentGroupSize = 2)]
    DLargeDisplayDigit,

    [Discriminator("the Colorful Dials where the {0} digit on the large display was {1}", Arguments = [QandA.Ordinal, "red", QandA.Ordinal, "orange", QandA.Ordinal, "yellow", QandA.Ordinal, "green", QandA.Ordinal, "cyan", QandA.Ordinal, "blue", QandA.Ordinal, "magenta", QandA.Ordinal, "purple"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DLargeDisplayColor,

    [Discriminator("the Colorful Dials where {0} was {1} in the color strip", Arguments = ["red", QandA.Ordinal, "orange", QandA.Ordinal, "yellow", QandA.Ordinal, "green", QandA.Ordinal, "cyan", QandA.Ordinal, "blue", QandA.Ordinal, "magenta", QandA.Ordinal, "purple", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DColorOrder
}

public partial class SouvenirModule
{
    [Handler("colorfulDials", "Colorful Dials", typeof(SColorfulDials), "Timwi")]
    [ManualQuestion("What were the numbers and their colors on the displays when the dials were in their initial calculated configurations?")]
    [ManualQuestion("What was each digit and its color on the large display?")]
    [ManualQuestion("What order was the color strip?")]
    private IEnumerator<SouvenirInstruction> ProcessColorfulDials(ModuleData module)
    {
        var comp = GetComponent(module, "colorfulDials");

        var colorList = new[] { "RED", "ORANGE", "YELLOW", "GREEN", "CYAN", "BLUE", "MAGENTA", "PURPLE" };
        var mainScreenColors = GetArrayField<string>(comp, "mainScreenColors").Get(expectedLength: 3, validator: v => colorList.Contains(v) ? null : $"expected one of: {colorList.JoinString(", ")}");
        var mainScreenDigits = GetArrayField<TextMesh>(comp, "mainScreen", isPublic: true).Get(expectedLength: 3, validator: v => v.text.RegexMatch(@"^\d$", out _) ? null : $"expected single digit, got {v.text}");
        var mainScreen = Enumerable.Range(0, 3).Select(ix => (digit: int.Parse(mainScreenDigits[ix].text), color: Array.IndexOf(colorList, mainScreenColors[ix]))).ToArray();
        var colorStripOrder = GetField<string[]>(comp, "clist")
            .Get(v => v.Length != 9 ? "expected length 9" : v.Take(8).Any(w => !colorList.Contains(w)) ? $"expected one of: {colorList.JoinString(", ")}" : v[8] != "BLACK" ? "expected BLACK as last element" : null)
            .Take(8)
            .Select(colorName => Array.IndexOf(colorList, colorName))
            .ToArray();
        var cardinalChart = GetArrayField<string[]>(comp, "cardinalChart").Get(expectedLength: 8, validator: v => v.Length != 8 ? "expected inner array length 8" : null);
        var colorGrid = GetArrayField<string[]>(comp, "colorGrid")
            .Get(expectedLength: 8, validator: arr => arr.Length != 10 ? "expected inner array length 10" : arr.Any(v => !v.RegexMatch(@"^[0-9][ROYGCBMP]$", out _)) ? $"expected digit plus color character" : null)
            .Select(arr => arr.Select(str => (digit: str[0] - '0', color: colorList.IndexOf(c => c[0] == str[1]))).ToArray())
            .ToArray();
        var dialNumColors = GetArrayField<string[]>(comp, "dialNumColors")
            .Get(expectedLength: 3, validator: arr => arr.Length != 10 ? "expected inner array length 10" : arr.Any(inner => !colorList.Contains(inner)) ? $"expected one of: {colorList.JoinString(", ")}" : null)
            .Select(array => array.Select(colorName => Array.IndexOf(colorList, colorName)).ToArray())
            .ToArray();

        var dialValues = GetField<int[][][]>(comp, "DialVals").Get(validator: arr =>
            arr.Length != 3 ? "expected length 3" :
            arr.Any(inner => inner.Length != 8) ? "expected inner length 8" :
            arr.Any(inner => inner.Any(innerInner => innerInner.Length != 10)) ? "expected inner inner length 10" :
            arr.Any(inner => inner.Any(innerInner => innerInner.Any(value => value < 0 || value > 99))) ? $"expected values 00–99" : null);
        var dialValColors = GetField<string[][][]>(comp, "DialValColors").Get(validator: arr =>
            arr.Length != 3 ? "expected length 3" :
            arr.Any(inner => inner.Length != 8) ? "expected inner length 8" :
            arr.Any(inner => inner.Any(innerInner => innerInner.Length != 10)) ? "expected inner inner length 10" :
            arr.Any(inner => inner.Any(innerInner => innerInner.Any(color => !colorList.Contains(color)))) ? $"expected one of {colorList.JoinString(", ")}" : null);

        // Calculate the “initial calculated configurations” of the dials because those are not stored in fields
        var dialAns = Ut.NewArray(3, dial =>
        {
            Debug.Log($"♦ 1");
            var cardinal = cardinalChart[mainScreen[dial].color][Array.IndexOf(colorStripOrder, mainScreen[dial].color)];
            Debug.Log($"♦ 2");
            var numTimes = 1 + dialNumColors[dial].Count(v => v == mainScreen[dial].color);
            Debug.Log($"♦ 3");
            var startRow = colorGrid.IndexOf(row => row.Contains(mainScreen[dial]));
            Debug.Log($"♦ 4");
            var row = ((startRow + (cardinal.Contains('N') ? -1 : cardinal.Contains('S') ? 1 : 0) * numTimes) % 8 + 8) % 8;
            Debug.Log($"♦ 5");
            var col = ((Array.IndexOf(colorGrid[startRow], mainScreen[dial]) + (cardinal.Contains('W') ? -1 : cardinal.Contains('E') ? 1 : 0) * numTimes) % 10 + 10) % 10;
            Debug.Log($"♦ 6");
            var (initialDigit, initialColor) = colorGrid[row][col];
            Debug.Log($"♦ 7");
            return (number: dialValues[dial][initialColor][initialDigit], color: Array.IndexOf(colorList, dialValColors[dial][initialColor][initialDigit]));
        });

        yield return WaitForSolve;

        // After this point, we don’t want the colors in all-caps anymore
        for (var i = 0; i < colorList.Length; i++)
            colorList[i] = colorList[i].ToLowerInvariant();

        var posNames = new[] { "left", "middle", "right" };
        for (var dial = 0; dial < 3; dial++)
        {
            yield return new Discriminator(SColorfulDials.DNumber, $"num-{dial}", dialAns[dial].number, args: [dialAns[dial].number.ToString("00"), posNames[dial]]);
            yield return question(SColorfulDials.QNumber, args: [posNames[dial]]).AvoidDiscriminators(SColorfulDials.DNumber).Answers(dialAns[dial].number.ToString("00"));
            yield return new Discriminator(SColorfulDials.DColor, $"color-{dial}", dialAns[dial].color, args: [colorList[dialAns[dial].color], posNames[dial]]);
            yield return question(SColorfulDials.QColor, args: [posNames[dial]]).AvoidDiscriminators(SColorfulDials.DColor).Answers(colorList[dialAns[dial].color]);
        }
        for (var digit = 0; digit < 3; digit++)
        {
            yield return new Discriminator(SColorfulDials.DLargeDisplayDigit, $"large-digit-{digit}", mainScreen[digit].digit, args: [Ordinal(digit + 1), mainScreen[digit].digit.ToString()]);
            yield return question(SColorfulDials.QLargeDisplayDigit, args: [Ordinal(digit + 1)]).AvoidDiscriminators(SColorfulDials.DLargeDisplayDigit).Answers(mainScreen[digit].digit.ToString());
            yield return new Discriminator(SColorfulDials.DLargeDisplayColor, $"large-color-{digit}", mainScreen[digit].color, args: [Ordinal(digit + 1), colorList[mainScreen[digit].color]]);
            yield return question(SColorfulDials.QLargeDisplayColor, args: [Ordinal(digit + 1)]).AvoidDiscriminators(SColorfulDials.DLargeDisplayColor).Answers(colorList[mainScreen[digit].color]);
        }
        for (var stripIx = 0; stripIx < 8; stripIx++)
        {
            yield return new Discriminator(SColorfulDials.DColorOrder, $"strip-{stripIx}", colorStripOrder[stripIx], args: [colorList[colorStripOrder[stripIx]], Ordinal(stripIx + 1)]);
            yield return question(SColorfulDials.QColorOrder, args: [Ordinal(stripIx + 1)]).AvoidDiscriminators(SColorfulDials.DColorOrder).Answers(colorList[colorStripOrder[stripIx]]);
        }
    }
}
