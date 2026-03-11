using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCheapCheckout
{
    [SouvenirQuestion("What was {1} in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["the paid amount", "the first paid amount", "the second paid amount"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(5, 50, "$0\".00\"")]
    Paid
}

public partial class SouvenirModule
{
    [SouvenirHandler("CheapCheckoutModule", "Cheap Checkout", typeof(SCheapCheckout), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCheapCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "CheapCheckoutModule");

        yield return WaitForActivate;

        var paids = new List<decimal> { GetField<decimal>(comp, "Display").Get() };
        var paid = GetField<decimal>(comp, "Paid").Get();
        if (paid != paids[0])
            paids.Add(paid);

        yield return WaitForSolve;

        // Stops the left/right buttons from working so the defuser cannot see all the items
        var leftButton = GetField<KMSelectable>(comp, "MoveLeft", isPublic: true).Get();
        var rightButton = GetField<KMSelectable>(comp, "MoveRight", isPublic: true).Get();

        leftButton.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
            leftButton.AddInteractionPunch(0.5f);
            return false;
        };
        rightButton.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
            rightButton.AddInteractionPunch(0.5f);
            return false;
        };

        for (var i = 0; i < paids.Count; i++)
            yield return question(SCheapCheckout.Paid, args: [paids.Count == 1 ? "the paid amount" : i == 0 ? "the first paid amount" : "the second paid amount"])
                .Answers($"${paids[i]:N2}");
    }
}
