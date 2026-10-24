using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSevenChooseFour
{
    [Question("Which letter was flashed as {1} in {0}?", TwoColumns4Answers, Arguments = ["Morse code", "Tap code"], TranslateArguments = [true], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    QFlashedLetter,

    [Question("Which color was flashed {1} in the color sequence in {0}?", TwoColumns4Answers, "Blue", "Cyan", "Green", "Magenta", "Red", "Yellow", "White", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QColorSequence,

    [Question("How many times did the number-key LED flash in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 7)]
    QNumberKey,

    [Discriminator("the Seven Choose Four whose {1} letter was {0}", ArgumentGroupSize = 2, Arguments = ["A", "Morse code", "B", "Tap code"], TranslateArguments = [false, true])]
    DFlashedLetter,

    [Discriminator("the Seven Choose Four whose {1} color in the color sequence was {0}", ArgumentGroupSize = 2, Arguments = ["Blue", QandA.Ordinal, "Cyan", QandA.Ordinal, "Green", QandA.Ordinal, "Magenta", QandA.Ordinal, "Red", QandA.Ordinal, "Yellow", QandA.Ordinal, "White", QandA.Ordinal], TranslateArguments = [true, false])]
    DColorSequence,

    [Discriminator("the Seven Choose Four whose number-key LED flashed {0} times", ArgumentGroupSize = 1, Arguments = ["1", "2", "3"])]
    DNumberKey,
}

public partial class SouvenirModule
{
    [Handler("sevenChooseFour", "Seven Choose Four", typeof(SSevenChooseFour), "Timwi")]
    [ManualQuestion("What letters were flashed in Morse and Tap code?")]
    [ManualQuestion("What color sequence was flashed?")]
    [ManualQuestion("How many times did the number-key LED flash?")]
    private IEnumerator<SouvenirInstruction> ProcessSevenChooseFour(ModuleData module)
    {
        var comp = GetComponent(module, "sevenChooseFour");
        var fldLedIndex = GetListField<int>(comp, "ledindex");
        var fldMorseKeyIndex = GetIntField(comp, "morsekeyindex");
        var fldTapKeyIndex = GetIntField(comp, "tapkeyindex");
        var fldNumKey = GetIntField(comp, "numkey");

        yield return WaitForSolve;

        var ledIndex = fldLedIndex.Get(expectedLength: 5);
        var colorKeyNames = new[] { "Blue", "Cyan", "Green", "Magenta", "Red", "Yellow", "White" };

        var morseKey = fldMorseKeyIndex.Get(min: 0, max: 25);
        var tapKey = fldTapKeyIndex.Get(min: 0, max: 25);
        var numKey = fldNumKey.Get(min: 1, max: 7);

        yield return question(SSevenChooseFour.QFlashedLetter, args: ["Morse code"]).AvoidDiscriminators("morse").Answers(((char) ('A' + morseKey)).ToString());
        yield return question(SSevenChooseFour.QFlashedLetter, args: ["Tap code"]).AvoidDiscriminators("tap").Answers(((char) ('A' + tapKey)).ToString());
        for (var i = 0; i < 5; i++)
            yield return question(SSevenChooseFour.QColorSequence, args: [Ordinal(i + 1)]).AvoidDiscriminators(SSevenChooseFour.DColorSequence).Answers(colorKeyNames[ledIndex[i]]);
        yield return question(SSevenChooseFour.QNumberKey).AvoidDiscriminators(SSevenChooseFour.DNumberKey).Answers(numKey.ToString());

        yield return new Discriminator(SSevenChooseFour.DFlashedLetter, "morse", morseKey, args: [((char) ('A' + morseKey)).ToString(), "Morse code"]);
        yield return new Discriminator(SSevenChooseFour.DFlashedLetter, "tap", tapKey, args: [((char) ('A' + tapKey)).ToString(), "Tap code"]);
        for (var i = 0; i < 5; i++)
            yield return new Discriminator(SSevenChooseFour.DColorSequence, $"color{i}", ledIndex[i], args: [colorKeyNames[ledIndex[i]], Ordinal(i + 1)]);
        yield return new Discriminator(SSevenChooseFour.DNumberKey, "num", numKey, args: [numKey.ToString()]);
    }
}
