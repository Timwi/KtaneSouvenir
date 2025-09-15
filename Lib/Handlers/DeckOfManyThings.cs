using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDeckOfManyThings
{
    [SouvenirQuestion("What deck did the first card of {0} belong to?", TwoColumns4Answers, "Standard", "Metropolitan", "Maritime", "Arctic", "Tropical", "Oasis", "Celestial")]
    FirstCard
}

public partial class SouvenirModule
{
    [SouvenirHandler("deckOfManyThings", "Deck of Many Things", typeof(SDeckOfManyThings), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessDeckOfManyThings(ModuleData module)
    {
        var comp = GetComponent(module, "deckOfManyThingsScript");
        var fldSolution = GetIntField(comp, "solution");

        yield return WaitForSolve;

        var deck = GetField<Array>(comp, "deck").Get(d => d.Length == 0 ? "deck is empty" : null);
        var btns = GetArrayField<KMSelectable>(comp, "btns", isPublic: true).Get(expectedLength: 2);
        var prevCard = GetField<KMSelectable>(comp, "prevCard", isPublic: true).Get();
        var nextCard = GetField<KMSelectable>(comp, "nextCard", isPublic: true).Get();

        prevCard.OnInteract = delegate { return false; };
        nextCard.OnInteract = delegate { return false; };
        foreach (var btn in btns)
            btn.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
                btn.AddInteractionPunch(0.5f);
                return false;
            };

        var firstCardDeck = deck.GetValue(0).GetType().ToString().Replace("Card", "");

        // correcting original misspelling
        if (firstCardDeck == "Artic")
            firstCardDeck = "Arctic";

        var solution = fldSolution.Get();

        if (solution == 0)
            yield return legitimatelyNoQuestion(module, "The solution was the first card.");

        yield return question(SDeckOfManyThings.FirstCard).Answers(firstCardDeck);
    }
}