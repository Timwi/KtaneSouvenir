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
    QArrow, // Currently pending custom graphics

    [Discriminator("the Azure Button that had this card", QuestionExtraType = InfoType.Sprites)]
    DCard,
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
        var arrows = GetField<Array>(puzzle, "Arrows", isPublic: true).Get(a => a.Length != 5 ? "expected length 5" : null);
        var fldArrowDirections = GetProperty<int[]>(arrows.GetValue(0), "Directions", isPublic: true);
        var arrowDirections = Enumerable.Range(0, 5).Select(arrowIx => fldArrowDirections.GetFrom(arrows.GetValue(arrowIx), validator: ar => ar.Length != 3 ? "expected length 3" : null)).ToArray();

        yield return WaitForSolve;

        var dirArrows = new[] { "↑", "↗", "→", "↘", "↓", "↙", "←", "↖" };

        foreach (var card in cards)
            yield return new Discriminator(SAzureButton.DCard, $"card{card}", questionExtra: AzureButtonSprites[card]);

        var preferredWrongAnswers = cards.Select(c => AzureButtonSprites[c]).ToArray();
        yield return question(SAzureButton.QCard).AvoidDiscriminators(SAzureButton.DCard).Answers(cards.Select(c => AzureButtonSprites[c]).ToArray(), preferredWrong: preferredWrongAnswers);
        yield return question(SAzureButton.QNumber).Answers(numbers.Select(x => x.ToString()).ToArray());

        var arrowAnswers = new List<string>();
        var prefWrongArrows = new HashSet<string>();
        for (var arrowIx = 0; arrowIx < 5; arrowIx++)
        {
            var arrowStr = "";
            for (var dirIx = 0; dirIx < 3; dirIx++)
                arrowStr += dirArrows[arrowDirections[arrowIx][dirIx]];

            arrowAnswers.Add(arrowStr);
            prefWrongArrows.Add(arrowStr);
        }

        for (var i = 0; i < 12; i++)
        {
            var arrowStr = "";
            for (var pos = 0; pos < 3; pos++)
                arrowStr += dirArrows[Rnd.Range(0, dirArrows.Length)];

            prefWrongArrows.Add(arrowStr);
        }

        yield return question(SAzureButton.QArrow).Answers(arrowAnswers.ToArray(), all: prefWrongArrows.ToArray());
    }
}
