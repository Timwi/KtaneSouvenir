using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using Rnd = UnityEngine.Random;

using static Souvenir.AnswerLayout;

public enum SAzureButton
{
    [Question("Which of these cards was shown in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "AzureButtonSprites")]
    QCard,

    [Question("Which of these numbers was shown in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(-9, 89)]
    QNumber,

    [Question("Which of these arrows was shown in {0}?", TwoColumns4Answers, ExampleAnswers = ["↑↑↑", "↗↗↗", "→→→", "↘↘↘", "↓↓↓", "↙↙↙", "←←←", "↖↖↖"])]
    [AnswerGenerator.Strings("←↖↑↗→↘↓↙", "←↖↑↗→↘↓↙", "←↖↑↗→↘↓↙")]
    QArrow,

    [Discriminator("the Azure Button that had this card", QuestionExtraType = InfoType.Sprites)]
    DCard,

    [Discriminator("the Azure Button that had the number {0}", Arguments = ["1", "2", "3"], ArgumentGroupSize = 1)]
    DNumber,

    [Discriminator("the Azure Button that had the arrow {0}", Arguments = ["↑↑↑", "↗↗↗", "→→→", "↘↘↘", "↓↓↓", "↙↙↙", "←←←", "↖↖↖"], ArgumentGroupSize = 1)]
    DArrow,
}

public partial class SouvenirModule
{
    [Handler("AzureButtonModule", "Azure Button", typeof(SAzureButton), "Timwi", AddThe = true)]
    [ManualQuestion("What were the displayed cards?")]
    [ManualQuestion("What were the numbers in the sequence?")]
    [ManualQuestion("What were the arrows?")]
    private IEnumerator<SouvenirInstruction> ProcessAzureButton(ModuleData module)
    {
        var comp = GetComponent(module, "AzureButtonScript");
        var cards = GetListField<int>(comp, "_cards").Get(expectedLength: 7);
        var offset = GetField<int>(comp, "_offset").Get(v => Math.Abs(v) is >= 1 and <= 9 ? null : "value out of range 1–9 (or -1–-9)");
        var numbers = cards.Take(6).Select(x => x + offset).ToArray();
        var puzzle = GetField<object>(comp, "_puzzle").Get();
        var arrowsRaw = GetField<Array>(puzzle, "Arrows", isPublic: true).Get(a => a.Length != 5 ? "expected length 5" : null);
        var fldArrowDirections = GetProperty<int[]>(arrowsRaw.GetValue(0), "Directions", isPublic: true);
        var arrows = Enumerable.Range(0, 5)
            .Select(arrowIx => fldArrowDirections.GetFrom(arrowsRaw.GetValue(arrowIx), validator: ar => ar.Length != 3 ? "expected length 3" : null).Select(dir => "↑↗→↘↓↙←↖"[dir]).JoinString())
            .ToArray();

        yield return WaitForSolve;

        // Questions
        var preferredWrongAnswers = cards.Select(c => AzureButtonSprites[c]).ToArray();
        yield return question(SAzureButton.QCard).AvoidDiscriminators(SAzureButton.DCard).Answers(cards.Select(c => AzureButtonSprites[c]).ToArray(), preferredWrong: preferredWrongAnswers);
        yield return question(SAzureButton.QNumber).AvoidDiscriminators(SAzureButton.DNumber).Answers(numbers.Select(x => x.ToString()).ToArray());
        yield return question(SAzureButton.QArrow).AvoidDiscriminators(SAzureButton.DArrow).Answers(arrows);

        // Discriminators
        foreach (var card in cards.Distinct())
            yield return new Discriminator(SAzureButton.DCard, $"card-{card}", questionExtra: AzureButtonSprites[card]);
        foreach (var number in numbers.Distinct())
            yield return new Discriminator(SAzureButton.DNumber, $"num-{number}", args: [number.ToString()]);
        foreach (var arrow in arrows.Distinct())
            yield return new Discriminator(SAzureButton.DArrow, $"arrow-{arrow}", args: [arrow]);
    }
}
