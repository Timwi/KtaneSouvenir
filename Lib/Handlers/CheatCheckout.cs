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

    [Question("What was the hack method for the {1} hack of {0}?", TwoColumns4Answers, "DSA", "W", "CI", "XSS", "BFA", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Hack,

    [Question("What was the site for the {1} hack of {0}?", OneColumn4Answers, ExampleAnswers = ["medicalsite.co", "checkout.kt", "collection.com", "ktane.timwi.de", "cartoon.com", "galaxydeliver.com"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Site
}

public partial class SouvenirModule
{
    [Handler("kataCheatCheckout", "Cheat Checkout", typeof(SCheatCheckout), "Timwi")]
    [ManualQuestion("What was the cryptocurrency?")]
    [ManualQuestion("What was the site and hack method for each hack?")]
    private IEnumerator<SouvenirInstruction> ProcessCheatCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "CheatCheckoutV3");
        yield return WaitForSolve;

        var hackGenerator = GetField<object>(comp, "_hackGenerator").Get();
        var hacks = GetMethod<IList>(hackGenerator, "GetHacks", 0, isPublic: true).Invoke();
        if (hacks == null || hacks.Count != 5)
            throw new AbandonModuleException($"expected exactly 5 hacks, got {hacks.Stringify()}");

        var websites = new string[hacks.Count];
        var hackMethods = new string[hacks.Count];
        for (var i = 0; i < hacks.Count; i++)
        {
            hackMethods[i] = hacks[i].GetType().Name switch
            {
                "DSAHack" => "DSA",
                "WormHack" => "W",
                "CIHack" => "CI",
                "XSSHack" => "XSS",
                "BFAHack" => "BFA",
                var typeName => throw new AbandonModuleException($"Invalid hack method: {typeName}"),
            };
            var website = GetProperty<object>(hacks[i], "Website", isPublic: true).Get();
            websites[i] = GetField<string>(website, "Url", isPublic: true).Get();
        }

        var objCrypto = GetField<object>(comp, "_chosenCrypto").Get();
        var cryptoName = GetField<string>(objCrypto, "Name", isPublic: true).Get();
        var possibleCryptos = GetStaticField<Array>(comp.GetType(), "_possibleCryptos").Get(v => v.Length != 9 ? "expected 9 possible cryptos" : null);
        var cryptoIx = Array.IndexOf(possibleCryptos, objCrypto);

        var spriteRenderers = GetArrayField<SpriteRenderer>(comp, "_cryptoSymbols", isPublic: true).Get(expectedLength: 9);
        var sprites = spriteRenderers.Select(sr => Sprites.TranslateSprite(sr.sprite, 800)).ToArray();
        yield return question(SCheatCheckout.Currency).Answers(sprites[cryptoIx], all: sprites);

        for (var i = 0; i < hacks.Count; i++)
        {
            yield return question(SCheatCheckout.Hack, args: [Ordinal(i + 1)]).Answers(hackMethods[i], all: hackMethods);
            yield return question(SCheatCheckout.Site, args: [Ordinal(i + 1)]).Answers(websites[i], all: websites);
        }
    }
}
