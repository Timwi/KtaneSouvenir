using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAzureButton
{
    [SouvenirQuestion("What was the {1} direction in the decoy arrow in {0}?", TwoColumns4Answers, "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QDecoyArrowDirection,

    [SouvenirQuestion("What was the {1} direction in the {2} non-decoy arrow in {0}?", TwoColumns4Answers, "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    QNonDecoyArrowDirection,

    [SouvenirQuestion("What was T in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "AzureButtonSprites", TranslatableStrings = ["the Azure Button that had this card in Stage 1", "the Azure Button where M was {0}", "the Azure Button where the decoy arrow went {0} at some point", "the Azure Button where the {1} non-decoy arrow went {0} at some point", "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west"])]
    QT,

    [SouvenirQuestion("Which of these cards was shown in Stage 1, but not T, in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "AzureButtonSprites")]
    QNotT,

    [SouvenirQuestion("What was M in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    QM,

    [SouvenirDiscriminator("the Azure Button that had this card in Stage 1", UsesQuestionSprite = true)]
    DCard,

    [SouvenirDiscriminator("the Azure Button where M was {0}", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    DM,

    [SouvenirDiscriminator("the Azure Button where the decoy arrow went {0} at some point", Arguments = ["north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west"], ArgumentGroupSize = 1)]
    DDecoyArrowDirection,

    [SouvenirDiscriminator("the Azure Button where the {1} non-decoy arrow went {0} at some point", Arguments = ["north", QandA.Ordinal, "north-east", QandA.Ordinal, "east", QandA.Ordinal, "south-east", QandA.Ordinal, "south", QandA.Ordinal, "south-west", QandA.Ordinal, "west", QandA.Ordinal, "north-west"], ArgumentGroupSize = 2)]
    DNonDecoyArrowDirection,
}

public partial class SouvenirModule
{
    [SouvenirHandler("AzureButtonModule", "Azure Button", typeof(SAzureButton), "Timwi", AddThe = true)]
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

        var dirNames = new[] { "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west" };

        foreach (var card in cards)
            yield return new Discriminator(SAzureButton.DCard, $"card{card}", true, questionSprite: AzureButtonSprites[card]);

        yield return new Discriminator(SAzureButton.DM, "m", m, [m.ToString()]);

        for (var arrowIx = 0; arrowIx < 5; arrowIx++)
            foreach (var dir in arrowDirections[arrowIx])
                yield return new Discriminator(arrowIx == 0 ? SAzureButton.DDecoyArrowDirection : SAzureButton.DNonDecoyArrowDirection,
                    $"arr-{arrowIx}-{dir}", true, [dirNames[dir], Ordinal(arrowIx)]);

        var preferredWrongAnswers = cards.Select(c => AzureButtonSprites[c]).ToArray();
        yield return question(SAzureButton.QT).AvoidDiscriminators(SAzureButton.DCard).Answers(AzureButtonSprites[cardT], preferredWrong: preferredWrongAnswers);
        yield return question(SAzureButton.QNotT).AvoidDiscriminators(SAzureButton.DCard).Answers(cards.Take(6).Select(c => AzureButtonSprites[c]).ToArray(), preferredWrong: preferredWrongAnswers);
        yield return question(SAzureButton.QM).AvoidDiscriminators(SAzureButton.DM).Answers(m.ToString());

        for (var arrowIx = 0; arrowIx < 5; arrowIx++)
            for (var dirIx = 0; dirIx < 3; dirIx++)
                yield return question(arrowIx == 0 ? SAzureButton.QDecoyArrowDirection : SAzureButton.QNonDecoyArrowDirection, args: [Ordinal(dirIx + 1), Ordinal(arrowIx)])
                    .AvoidDiscriminators(SAzureButton.DDecoyArrowDirection, SAzureButton.DNonDecoyArrowDirection)
                    .Answers(dirNames[arrowDirections[arrowIx][dirIx]]);
    }
}
