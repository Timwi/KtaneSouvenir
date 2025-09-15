using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCheatCheckout
{
    [SouvenirQuestion("What was the cryptocurrency of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    Currency,

    [SouvenirQuestion("What was the hack method for the {1} hack of {0}?", TwoColumns4Answers, "DSA", "W", "CI", "XSS", "BFA", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Hack,

    [SouvenirQuestion("What was the site for the {1} hack of {0}?", OneColumn4Answers, ExampleAnswers = ["medicalsite.co", "checkout.kt", "collection.com", "ktane.timwi.de", "cartoon.com", "galaxydeliver.com"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Site
}

public partial class SouvenirModule
{
    [SouvenirHandler("kataCheatCheckout", "Cheat Checkout", typeof(SCheatCheckout), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessCheatCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "CheatCheckoutRemake");
        yield return WaitForSolve;

        var spriteRenderers = GetArrayField<SpriteRenderer>(comp, "_cryptoSymbols", isPublic: true).Get(expectedLength: 9);
        var sprites = spriteRenderers.Select(sr => Sprites.TranslateSprite(sr.sprite, 800)).ToArray();
        var spriteIndex = GetField<int>(comp, "_chosenCrypto").Get(validator: ix => ix is < 0 or > 8 ? "expected range 0–8" : null);
        var hackMethods = GetArrayField<string>(comp, "_possibleHacks").Get(expectedLength: 5).ToArray();
        var sites = GetArrayField<string>(comp, "_possibleWebsites").Get(expectedLength: 32).Select(s => s.Split(':')[0]).ToArray();
        var hacks = GetField<IList>(comp, "_hackList").Get(validator: v => v.Count != 5 ? "expected 5 hacks" : null);
        var hackSites = new List<string>();
        var hackHackMethods = new List<string>();

        for (var i = 0; i < hacks.Count; i++)
        {
            hackSites.Add(GetField<string>(hacks[i], "website").Get().Split(':')[0]);
            hackHackMethods.Add(hacks[i].GetType().Name switch
            {
                "DSA" => "DSA",
                "Worm" => "W",
                "CodeInjection" => "CI",
                "CrossSiteScripting" => "XSS",
                "BruteForceAttempt" => "BFA",
                var typeName => throw new AbandonModuleException($"Invalid hack method: {typeName}"),
            });
        }

        var questions = new List<QandA>();
        questions.Add(makeQuestion(Question.CheatCheckoutCurrency, module, correctAnswers: new[] { sprites[spriteIndex] }, allAnswers: sprites));

        for (var i = 0; i < hacks.Count; i++)
        {
            questions.Add(makeQuestion(Question.CheatCheckoutHack, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { hackHackMethods[i] }, allAnswers: hackMethods));
            questions.Add(makeQuestion(Question.CheatCheckoutSite, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { hackSites[i] }, allAnswers: sites));
        }

        addQuestions(module, questions);
    }
}