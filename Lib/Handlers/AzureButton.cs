using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAzureButton
{
    [SouvenirQuestion("What was the {1} direction in the decoy arrow in {0}?", TwoColumns4Answers, "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DecoyArrowDirection,

    [SouvenirQuestion("What was T in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "AzureButtonSprites", TranslatableStrings = ["the Azure Button that had this card in Stage 1", "the Azure Button where M was {0}", "the Azure Button where the decoy arrow went {0} at some point", "the Azure Button where the {1} non-decoy arrow went {0} at some point", "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west"])]
    T,

    [SouvenirQuestion("Which of these cards was shown in Stage 1, but not T, in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "AzureButtonSprites")]
    NotT,

    [SouvenirQuestion("What was M in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    M,

    [SouvenirQuestion("What was the {1} direction in the {2} non-decoy arrow in {0}?", TwoColumns4Answers, "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    NonDecoyArrowDirection
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
        _azureButtonInfos.Add((cards.ToArray(), m, arrowDirections));

        yield return WaitForSolve;

        var dirNames = Question.AzureButtonDecoyArrowDirection.GetAnswers().Select(dir => translateString(Question.AzureButtonT, dir)).ToArray();
        var candidateDiscriminators = new List<(string format, string name, Sprite qSprite)> { (null, null, null) };
        foreach (var card in cards)
            if (_azureButtonInfos.Count(tup => tup.cards.Contains(card)) == 1)
                candidateDiscriminators.Add((translateString(Question.AzureButtonT, "the Azure Button that had this card in Stage 1"), "s1c", AzureButtonSprites[card]));
        if (_azureButtonInfos.Count(tup => tup.m == m) == 1)
            candidateDiscriminators.Add((string.Format(translateString(Question.AzureButtonT, "the Azure Button where M was {0}"), m), "m", null));
        for (var arrowIx = 0; arrowIx < 5; arrowIx++)
            foreach (var dir in arrowDirections[arrowIx])
                if (_azureButtonInfos.Count(tup => tup.arrowDirs[arrowIx].Contains(dir)) == 1)
                    candidateDiscriminators.Add((
                        arrowIx == 0
                            ? string.Format(translateString(Question.AzureButtonT, "the Azure Button where the decoy arrow went {0} at some point"), dirNames[dir])
                            : string.Format(translateString(Question.AzureButtonT, "the Azure Button where the {1} non-decoy arrow went {0} at some point"), dirNames[dir], Ordinal(arrowIx)),
                        "ar", null));

        var qs = new List<QandA>();
        void makeQ(string forbiddenDiscriminator, Question q, object correctAnswers, object preferredWrongAnswers = null, string[] formatArgs = null)
        {
            var fmt = candidateDiscriminators.Where(tup => tup.name != forbiddenDiscriminator).PickRandom();
            qs.Add(correctAnswers is Sprite[] caSpr && preferredWrongAnswers is Sprite[] pwaSpr
                ? makeQuestion(q, module, formattedModuleName: fmt.format, questionSprite: fmt.qSprite, formatArgs: formatArgs, correctAnswers: caSpr, preferredWrongAnswers: pwaSpr)
                : makeQuestion(q, module, formattedModuleName: fmt.format, questionSprite: fmt.qSprite, formatArgs: formatArgs, correctAnswers: (string[]) correctAnswers, preferredWrongAnswers: (string[]) preferredWrongAnswers));
        }

        makeQ("s1c", Question.AzureButtonT, correctAnswers: new[] { AzureButtonSprites[cardT] }, preferredWrongAnswers: cards.Select(c => AzureButtonSprites[c]).ToArray());
        makeQ("s1c", Question.AzureButtonNotT, correctAnswers: cards.Take(6).Select(c => AzureButtonSprites[c]).ToArray(), preferredWrongAnswers: cards.Select(c => AzureButtonSprites[c]).ToArray());
        makeQ("m", Question.AzureButtonM, correctAnswers: new[] { m.ToString() });

        for (var arrowIx = 0; arrowIx < 5; arrowIx++)
            for (var dirIx = 0; dirIx < 3; dirIx++)
                makeQ("ar",
                    arrowIx == 0 ? Question.AzureButtonDecoyArrowDirection : Question.AzureButtonNonDecoyArrowDirection,
                    correctAnswers: new[] { dirNames[arrowDirections[arrowIx][dirIx]] },
                    formatArgs: arrowIx == 0 ? new[] { Ordinal(dirIx + 1) } : new[] { Ordinal(dirIx + 1), Ordinal(arrowIx) });

        addQuestions(module, qs);
    }
}