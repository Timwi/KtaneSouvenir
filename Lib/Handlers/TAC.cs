using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STAC
{
    [SouvenirQuestion("Which card was {1} in the swap in {0}?", TwoColumns4Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "backwards 3", "backwards 4", "backwards 5", "single-step 6", "single-step 7", "8 or discard", "9 or discard", "10 or discard", "Warrior", "Trickster", Arguments = ["given away", "received"], ArgumentGroupSize = 1, TranslateArguments = [true], TranslateAnswers = true)]
    SwappedCard,

    [SouvenirQuestion("Which card was in your hand in {0}?", TwoColumns4Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "backwards 3", "backwards 4", "backwards 5", "single-step 6", "single-step 7", "8 or discard", "9 or discard", "10 or discard", "Warrior", "Trickster", TranslateAnswers = true)]
    HeldCard
}

public partial class SouvenirModule
{
    [SouvenirHandler("TACModule", "TAC", typeof(STAC), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessTAC(ModuleData module)
    {
        var comp = GetComponent(module, "TACScript");
        var swap = GetField<object>(comp, "_swappableCard").Get(nullAllowed: true);

        object topCard = null;
        if (swap is not null)
            yield return WaitForSolve;
        else
        {
            var fldHand = GetField<IList>(comp, "_hand");
            while (module.Unsolved)
            {
                var hand = fldHand.Get();
                topCard = hand.Cast<object>().SingleOrDefault(o => o is not null) ?? topCard;
                yield return null;
            }
        }

        var initialHand = GetField<IList>(comp, "_initialHand").Get(v => v.Count != 5 ? "Expected 5 cards" : v.Cast<object>().Any(v => v is null) ? "Expected non-null cards" : null);

        var validCards = new[] { "1", "2", "3", "backwards 4", "5", "6", "single-step 7", "8 or discard", "9", "10", "12", "13", "Trickster", "Warrior" };
        var ruleseed = GetField<object>(comp, "RuleSeedable", isPublic: true).Get();
        var rng = GetMethod<object>(ruleseed, "GetRNG", 0, isPublic: true).Invoke();
        var seed = GetProperty<int>(rng, "Seed", isPublic: true).Get();
        if (seed != 1)
        {
            GetMethod<object>(rng, "ShuffleFisherYates", 1, isPublic: true).Invoke(GetArrayField<string>(comp, "_allNames").Get(expectedLength: 32).ToArray());
            var next = GetMethod<int>(rng, "Next", 2, isPublic: true);
            var backwards = next.Invoke(3, 6);
            var singleStep = next.Invoke(6, 8);
            var discard = next.Invoke(8, 11);
            var missing = next.Invoke(0, 10);
            var cards = Enumerable.Range(1, 13).Except(new[] { backwards, singleStep, discard }).ToList();
            cards.RemoveAt(missing);
            validCards = cards
                .Select(c => c.ToString())
                .Concat(new[] { $"backwards {backwards}", $"single-step {singleStep}", $"{discard} or discard", "Trickster", "Warrior" })
                .ToArray();
        }

        static string toString(object card)
        {
            var cardStr = card.ToString();
            var match = Regex.Match(cardStr, "^(1[0-3]|[1-9])(?:(◊)|(⏪)|(∴))?$");
            return
                !match.Success ? cardStr :
                match.Groups[2].Success ? $"{match.Groups[1].Value} or discard" :
                match.Groups[3].Success ? $"backwards {match.Groups[1].Value}" :
                match.Groups[4].Success ? $"single-step {match.Groups[1].Value}" :
                match.Groups[1].Value;
        }

        if (swap is not null)
        {
            var ix = GetField<int?>(comp, "_mustSwapWith").Get(v => v is null or < 0 or > 4 ? "Expected number [0, 4]" : null);
            var usedCards = initialHand.Cast<object>().Select(toString).Concat(new[] { toString(swap) }).ToArray();
            addQuestions(module,
                makeQuestion(Question.TACSwappedCard, module, correctAnswers: new[] { toString(initialHand[ix.Value]) }, formatArgs: new[] { "given away" }, allAnswers: validCards, preferredWrongAnswers: usedCards),
                makeQuestion(Question.TACSwappedCard, module, correctAnswers: new[] { toString(swap) }, formatArgs: new[] { "received" }, allAnswers: validCards, preferredWrongAnswers: usedCards));
        }
        else
        {
            addQuestion(module, Question.TACHeldCard,
                correctAnswers: initialHand.Cast<object>().Select(toString).Except(new[] { toString(topCard) }).ToArray(),
                allAnswers: validCards.Except(new[] { toString(topCard) }).ToArray());
        }
    }
}