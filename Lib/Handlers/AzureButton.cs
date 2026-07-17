using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAzureButton
{
    [Question("What was the {1} direction in the decoy arrow in {0}?", TwoColumns4Answers, "up", "up-right", "right", "down-right", "down", "down-left", "left", "up-left", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QDecoyArrowDirection,

    [Question("What was the {1} direction in the {2} non-decoy arrow in {0}?", TwoColumns4Answers, "up", "up-right", "right", "down-right", "down", "down-left", "left", "up-left", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2, TranslateAnswers = true)]
    QNonDecoyArrowDirection,

    [Question("What was T in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "AzureButtonSprites")]
    QT,

    [Question("Which of these cards was shown in Stage 1, but not T, in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "AzureButtonSprites")]
    QNotT,

    [Question("What was M in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    QM,

    [Discriminator("the Azure Button that had this card in Stage 1", QuestionExtraType = InfoType.Sprites)]
    DCard,

    [Discriminator("the Azure Button where M was {0}", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    DM,

    [Discriminator("the Azure Button where the decoy arrow went {0} at some point", Arguments = ["up", "up-right", "right", "down-right", "down", "down-left", "left", "up-left"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DDecoyArrowDirection,

    [Discriminator("the Azure Button where the {1} non-decoy arrow went {0} at some point", Arguments = ["up", QandA.Ordinal, "up-right", QandA.Ordinal, "right", QandA.Ordinal, "down-right", QandA.Ordinal, "down", QandA.Ordinal, "down-left", QandA.Ordinal, "left", QandA.Ordinal, "up-left", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DNonDecoyArrowDirection,
}

public partial class SouvenirModule
{
    [Handler("AzureButtonModule", "Azure Button", typeof(SAzureButton), "Timwi", AddThe = true)]
    [ManualQuestion("What were T and the other displayed cards?")]
    [ManualQuestion("What was M?")]
    [ManualQuestion("What were the arrows?")]
    private IEnumerator<SouvenirInstruction> ProcessAzureButton(ModuleData module)
    {
        var comp = GetComponent(module, "AzureButtonScript");
        var cards = GetListField<int>(comp, "_cards").Get(expectedLength: 7);
        var cardT = cards.Last();
        var m = Math.Abs(GetField<int>(comp, "_offset").Get(v => Math.Abs(v) is >= 1 and <= 9 ? null : "value out of range 1–9 (or -1–-9)"));
        var puzzle = GetField<object>(comp, "_puzzle").Get();
        var arrows = GetField<Array>(puzzle, "Arrows", isPublic: true).Get(a => a.Length != 5 ? "expected length 5" : null);
        var fldArrowDirections = GetProperty<int[]>(arrows.GetValue(0), "Directions", isPublic: true);
        var arrowDirections = Enumerable.Range(0, 5).Select(arrowIx => fldArrowDirections.GetFrom(arrows.GetValue(arrowIx), validator: ar => ar.Length != 3 ? "expected length 3" : null)).ToArray();

        yield return WaitForSolve;

        var dirNames = new[] { "up", "up-right", "right", "down-right", "down", "down-left", "left", "up-left" };

        foreach (var card in cards)
            yield return new Discriminator(SAzureButton.DCard, $"card{card}", questionExtra: AzureButtonSprites[card]);

        yield return new Discriminator(SAzureButton.DM, "m", m, [m.ToString()]);

        var preferredWrongAnswers = cards.Select(c => AzureButtonSprites[c]).ToArray();
        yield return question(SAzureButton.QT).AvoidDiscriminators(SAzureButton.DCard).Answers(AzureButtonSprites[cardT], preferredWrong: preferredWrongAnswers);
        yield return question(SAzureButton.QNotT).AvoidDiscriminators(SAzureButton.DCard).Answers(cards.Take(6).Select(c => AzureButtonSprites[c]).ToArray(), preferredWrong: preferredWrongAnswers);
        yield return question(SAzureButton.QM).AvoidDiscriminators(SAzureButton.DM).Answers(m.ToString());

        for (var arrowIx = 0; arrowIx < 5; arrowIx++)
            for (var dirIx = 0; dirIx < 3; dirIx++)
            {
                yield return question(arrowIx == 0 ? SAzureButton.QDecoyArrowDirection : SAzureButton.QNonDecoyArrowDirection, args: [Ordinal(dirIx + 1), Ordinal(arrowIx)])
                    .AvoidDiscriminators(SAzureButton.DDecoyArrowDirection, SAzureButton.DNonDecoyArrowDirection)
                    .Answers(dirNames[arrowDirections[arrowIx][dirIx]]);
                yield return new Discriminator(arrowIx == 0 ? SAzureButton.DDecoyArrowDirection : SAzureButton.DNonDecoyArrowDirection,
                    $"arr-{arrowIx}-{dirIx}", true, [dirNames[arrowDirections[arrowIx][dirIx]], Ordinal(arrowIx)]);
            }
    }
}
