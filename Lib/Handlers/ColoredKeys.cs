using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SColoredKeys
{
    [Question("What was the displayed word in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white")]
    QDisplayWord,

    [Question("What was the displayed word’s color in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white", TranslateAnswers = true)]
    QDisplayWordColor,

    [Question("What letter was on the {1} key in {0}?", ThreeColumns6Answers, Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings('A', 'Z')]
    QKeyLetter,

    [Question("What was the color of the {1} key in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white", Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateArguments = [true])]
    QKeyColor,

    [Discriminator("the Colored Keys whose displayed word was “{0}”", Arguments = ["red", "blue", "green", "yellow", "purple", "white"], ArgumentGroupSize = 1)]
    DDisplayWord,

    [Discriminator("the Colored Keys whose word was displayed in {0}", Arguments = ["red", "blue", "green", "yellow", "purple", "white"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DDisplayWordColor,

    [Discriminator("the Colored Keys whose letter on the {0} key was {1}", Arguments = ["top-left", "A", "top-right", "B", "bottom-left", "C", "bottom-right", "D"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DKeyLetter,

    [Discriminator("the Colored Keys whose {0} key was {1}", Arguments = ["top-left", "red", "top-right", "blue", "bottom-left", "green", "bottom-right", "yellow", "top-left", "purple", "top-right", "white"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    DKeyColor
}

public partial class SouvenirModule
{
    [Handler("lgndColoredKeys", "Colored Keys", typeof(SColoredKeys), "luisdiogo98")]
    [ManualQuestion("What was the displayed word and its color?")]
    [ManualQuestion("What were the colors and letters on each key?")]
    private IEnumerator<SouvenirInstruction> ProcessColoredKeys(ModuleData module)
    {
        var comp = GetComponent(module, "ColoredKeysScript");

        yield return WaitForSolve;

        var colors = GetArrayField<string>(comp, "loggingWords", isPublic: true).Get();
        var letters = GetArrayField<string>(comp, "letters", isPublic: true).Get();
        var displayWord = GetIntField(comp, "displayIndex").Get(0, colors.Length - 1);
        var displayColor = GetIntField(comp, "displayColIndex").Get(0, colors.Length - 1);
        var matsNames = GetArrayField<Material>(comp, "buttonmats", isPublic: true).Get().Select(x => x.name).ToArray();

        var btnLetter = Enumerable.Range(1, 4).Select(i => GetIntField(comp, $"b{i}LetIndex").Get(0, letters.Length - 1)).ToArray();
        var btnColor = Enumerable.Range(1, 4).Select(i => GetIntField(comp, $"b{i}ColIndex").Get(0, matsNames.Length - 1)).ToArray();

        yield return question(SColoredKeys.QDisplayWord).AvoidDiscriminators("word").Answers(colors[displayWord], preferredWrong: colors);
        yield return question(SColoredKeys.QDisplayWordColor).AvoidDiscriminators("color").Answers(colors[displayColor], preferredWrong: colors);
        yield return question(SColoredKeys.QKeyLetter, args: ["top-left"]).AvoidDiscriminators("key-ltr-1").Answers(letters[btnLetter[0]], preferredWrong: letters);
        yield return question(SColoredKeys.QKeyLetter, args: ["top-right"]).AvoidDiscriminators("key-ltr-2").Answers(letters[btnLetter[1]], preferredWrong: letters);
        yield return question(SColoredKeys.QKeyLetter, args: ["bottom-left"]).AvoidDiscriminators("key-ltr-3").Answers(letters[btnLetter[2]], preferredWrong: letters);
        yield return question(SColoredKeys.QKeyLetter, args: ["bottom-right"]).AvoidDiscriminators("key-ltr-4").Answers(letters[btnLetter[3]], preferredWrong: letters);
        yield return question(SColoredKeys.QKeyColor, args: ["top-left"]).AvoidDiscriminators("key-color-1").Answers(matsNames[btnColor[0]], preferredWrong: matsNames);
        yield return question(SColoredKeys.QKeyColor, args: ["top-right"]).AvoidDiscriminators("key-color-2").Answers(matsNames[btnColor[1]], preferredWrong: matsNames);
        yield return question(SColoredKeys.QKeyColor, args: ["bottom-left"]).AvoidDiscriminators("key-color-3").Answers(matsNames[btnColor[2]], preferredWrong: matsNames);
        yield return question(SColoredKeys.QKeyColor, args: ["bottom-right"]).AvoidDiscriminators("key-color-4").Answers(matsNames[btnColor[3]], preferredWrong: matsNames);

        yield return new Discriminator(SColoredKeys.DDisplayWord, "word", displayWord, args: [colors[displayWord]]);
        yield return new Discriminator(SColoredKeys.DDisplayWordColor, "color", displayColor, args: [colors[displayColor]]);
        yield return new Discriminator(SColoredKeys.DKeyLetter, "key-ltr-1", letters[btnLetter[0]], args: ["top-left", letters[btnLetter[0]]]);
        yield return new Discriminator(SColoredKeys.DKeyLetter, "key-ltr-2", letters[btnLetter[1]], args: ["top-right", letters[btnLetter[1]]]);
        yield return new Discriminator(SColoredKeys.DKeyLetter, "key-ltr-3", letters[btnLetter[2]], args: ["bottom-left", letters[btnLetter[2]]]);
        yield return new Discriminator(SColoredKeys.DKeyLetter, "key-ltr-4", letters[btnLetter[3]], args: ["bottom-right", letters[btnLetter[3]]]);
        yield return new Discriminator(SColoredKeys.DKeyColor, "key-color-1", btnColor[0], args: ["top-left", matsNames[btnColor[0]]]);
        yield return new Discriminator(SColoredKeys.DKeyColor, "key-color-2", btnColor[1], args: ["top-right", matsNames[btnColor[1]]]);
        yield return new Discriminator(SColoredKeys.DKeyColor, "key-color-3", btnColor[2], args: ["bottom-left", matsNames[btnColor[2]]]);
        yield return new Discriminator(SColoredKeys.DKeyColor, "key-color-4", btnColor[3], args: ["bottom-right", matsNames[btnColor[3]]]);
    }
}
