using System.Collections;
using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SThirtyOne
{
    [Question("What was the first card in the {1} stage of {0}?", ThreeColumns6Answers,
        // These are ranks Ace-King and suits ♠/♥/♣/♦ but mapped to the codepoints in the symbol font
        "1=", "2=", "3=", "4=", "5=", "6=", "7=", "8=", "9=", "0=", ":=", ";=", "<=", "1>", "2>", "3>", "4>", "5>", "6>", "7>", "8>", "9>", "0>", ":>", ";>", "<>", "1?", "2?", "3?", "4?", "5?", "6?", "7?", "8?", "9?", "0?", ":?", ";?", "<?", "1@", "2@", "3@", "4@", "5@", "6@", "7@", "8@", "9@", "0@", ":@", ";@", "<@",
        AnswerType = InfoType.SymbolsFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    FirstCard
}

public partial class SouvenirModule
{
    [Handler("thirtyOne", "Thirty One", typeof(SThirtyOne), "Espik")]
    [ManualQuestion("What were the first cards in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessThirtyOne(ModuleData module)
    {
        var comp = GetComponent(module, "ThirtyOneModuleScript");

        var ranks = new string[13] { "1" /*A*/, "2", "3", "4", "5", "6", "7", "8", "9", "0" /*10*/, ":" /*J*/, ";" /*Q*/, "<" /*K*/ };
        var suits = new string[4] { "=" /*♠*/, ">" /*♥*/, "?" /*♣*/, "@" /*♦*/ };

        var cards = new string[3];

        var solveManager = GetField<object>(comp, "solveManager", isPublic: true).Get();
        var fldStageCounter = GetIntField(solveManager, "count");

        var cardScreenObject = GetField<GameObject>(comp, "cardScreen", isPublic: true).Get();

        var fldCurrentRank = GetIntField(comp, "currRank");
        var fldCurrentSuit = GetIntField(comp, "currSuit");

        yield return WaitForActivate;

        void RetrieveCard()
        {
            var stage = fldStageCounter.Get();
            var rank = ranks[fldCurrentRank.Get() - 1];
            var suit = suits[fldCurrentSuit.Get()];
            cards[stage] = rank + suit;
        }

        RetrieveCard();

        while (!module.IsSolved)
        {
            if (!cardScreenObject.activeSelf)
            {
                while (!cardScreenObject.activeSelf)
                    yield return null;

                yield return null;
                RetrieveCard();
            }

            yield return null;
        }

        for (var i = 0; i < cards.Length; i++)
            yield return question(SThirtyOne.FirstCard, args: [Ordinal(i + 1)]).Answers(cards[i], preferredWrong: cards);
    }
}
