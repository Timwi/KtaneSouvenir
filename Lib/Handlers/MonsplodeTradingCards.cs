using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMonsplodeTradingCards
{
    [SouvenirQuestion("Which of these cards was in your hand before the last action in {0}?", TwoColumns4Answers, "Aluga", "Asteran", "Bob", "Buhar", "Caadarim", "Clondar", "Cutie Pie", "Docsplode", "Flaurim", "Gloorim", "Lanaluff", "Lugirit", "Magmy", "Melbor", "Mountoise", "Myrchat", "Nibs", "Percy", "Pouse", "Ukkens", "Vellarim", "Violan", "Zapra", "Zenlad", "Aluga, The Fighter", "Bob, The Ancestor", "Buhar, The Protector", "Melbor, The Web Bug")]
    CardsAny,

    [SouvenirQuestion("Which of these print versions was present on a card in your hand before the last action in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-I", "1-9")]
    PrintVersionsAny
}

public partial class SouvenirModule
{
    [SouvenirHandler("monsplodeCards", "Monsplode Trading Cards", typeof(SMonsplodeTradingCards), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMonsplodeTradingCards(ModuleData module)
    {
        var comp = GetComponent(module, "MonsplodeCardModule");
        var fldStage = GetIntField(comp, "correctOffer", isPublic: true);

        var stageCount = GetIntField(comp, "offerCount", isPublic: true).Get(min: 3, max: 3);
        var data = GetField<object>(comp, "CD", isPublic: true).Get();
        var monsplodeNames = GetArrayField<string>(data, "names", isPublic: true).Get().Select(s => s.Replace("\r", "").Replace("\n", " ")).ToArray();

        yield return WaitForSolve;

        if (fldStage.Get() != stageCount)
            throw new AbandonModuleException($"Abandoning Monsplode Trading Cards because ‘correctOffer’ has unexpected value {fldStage.Get()} instead of {stageCount}.");

        var deck = GetField<Array>(comp, "deck", isPublic: true).Get(ar => ar.Length != 3 ? "expected length 3" : null).Cast<object>().ToArray();
        var offer = GetField<object>(comp, "offer", isPublic: true).Get();
        var fldMonsplode = GetIntField(offer, "monsplode", isPublic: true);
        var fldPrintDigit = GetIntField(offer, "printDigit", isPublic: true);
        var fldPrintChar = GetField<char>(offer, "printChar", isPublic: true);

        var monsplodeIds = new[] { fldMonsplode.Get(0, monsplodeNames.Length - 1) }.Concat(deck.Select(card => fldMonsplode.GetFrom(card, 0, monsplodeNames.Length - 1))).ToArray();
        var monsplodes = monsplodeIds.Select(mn => monsplodeNames[mn]).ToArray();
        var printVersions = new[] { fldPrintChar.Get() + "" + fldPrintDigit.Get() }.Concat(deck.Select(card => fldPrintChar.GetFrom(card) + "" + fldPrintDigit.GetFrom(card))).ToArray();
        
        yield return question(SMonsplodeTradingCards.CardsAny).Answers([monsplodes[1], monsplodes[2], monsplodes[3]], preferredWrong: monsplodeNames);
        yield return question(SMonsplodeTradingCards.PrintVersionsAny).Answers([printVersions[1], printVersions[2], printVersions[3]], preferredWrong: printVersions);
    }
}
