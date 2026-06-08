using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SCheatCheckout
{
    [Question("What was the cryptocurrency of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    Currency,

    [Question("Which website got hacked in {0}?", OneColumn4Answers, "repost.com", "pointercat.com", "usb.os", "color.org", "ktane.timwi.de", "lol.gg", "velvet.ss", "watch.tv", "onion.co", "flybird.tv", "sellcoin.org", "collection.com", "razor.pt", "checkout.kt", "crunch.bg", "locco.pt", "plant.tr", "cartoon.com", "blogsite.co", "voila.lc", "ktane.gov", "loli.co", "anime.st", "medicalsite.co", "recoil.pt", "numerical.ss", "isight.com", "symbolic.co", "grocery.st", "galaxydeliver.com", "vilesight.ei", "random.site")]
    Sites
}

public partial class SouvenirModule
{
    [Handler("kataCheatCheckout", "Cheat Checkout", typeof(SCheatCheckout), "Timwi")]
    [ManualQuestion("What was the cryptocurrency?")]
    [ManualQuestion("Which websites got hacked?")]
    private IEnumerator<SouvenirInstruction> ProcessCheatCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "CheatCheckoutV3");
        yield return WaitForSolve;

        var hackGenerator = GetField<object>(comp, "_hackGenerator").Get();
        var hacks = GetMethod<IList>(hackGenerator, "GetHacks", 0, isPublic: true).Invoke([], h => h.Count != 5 ? "expected exactly 5 hacks" : null);

        var websites = new string[hacks.Count];
        for (var i = 0; i < hacks.Count; i++)
        {
            var website = GetProperty<object>(hacks[i], "Website", isPublic: true).Get();
            websites[i] = GetField<string>(website, "Url", isPublic: true).Get();
        }

        var objCrypto = GetField<object>(comp, "_chosenCrypto").Get();
        var possibleCryptos = GetStaticField<Array>(comp.GetType(), "_possibleCryptos").Get(v => v.Length != 9 ? "expected 9 possible cryptos" : null);
        var cryptoIx = Array.IndexOf(possibleCryptos, objCrypto);

        var spriteRenderers = GetArrayField<SpriteRenderer>(comp, "_cryptoSymbols", isPublic: true).Get(expectedLength: 9);
        var sprites = spriteRenderers.Select(sr => Sprites.TranslateSprite(sr.sprite, 800)).ToArray();
        yield return question(SCheatCheckout.Currency).Answers(sprites[cryptoIx], all: sprites);

        yield return question(SCheatCheckout.Sites).Answers(websites);
    }
}
