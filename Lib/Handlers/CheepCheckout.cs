using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCheepCheckout
{
    [Question("Which of these bird sounds can be heard in {0}?", TwoColumns4Answers, Type = AnswerType.Audio, AudioFieldName = "CheepCheckoutAudio")]
    Birds
}

public partial class SouvenirModule
{
    [Handler("cheepCheckout", "Cheep Checkout", typeof(SCheepCheckout), "Quinn Wuest")]
    [ManualQuestion("Which bird sounds can be heard?")]
    private IEnumerator<SouvenirInstruction> ProcessCheepCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "cheepCheckoutScript");
        var fldUnicorn = GetField<bool>(comp, "unicorn");
        yield return WaitForSolve;

        if (fldUnicorn.Get())
            yield return legitimatelyNoQuestion(module, "The unicorn applied.");

        var dict = new Dictionary<string, int>()
        {
            ["Auklet"] = 24,
            ["Bluebird"] = 5,
            ["Chickadee"] = 23,
            ["Dove"] = 20,
            ["Egret"] = 13,
            ["Finch"] = 25,
            ["Godwit"] = 16,
            ["Hummingbird"] = 15,
            ["Ibis"] = 4,
            ["Jay"] = 10,
            ["Kinglet"] = 11,
            ["Loon"] = 19,
            ["Magpie"] = 6,
            ["Nuthatch"] = 12,
            ["Oriole"] = 22,
            ["Pipit"] = 9,
            ["Quail"] = 17,
            ["Raven"] = 8,
            ["Shrike"] = 7,
            ["Thrush"] = 2,
            ["Umbrellabird"] = 3,
            ["Vireo"] = 21,
            ["Warbler"] = 18,
            ["Xantus’s Hummingbird"] = 14,
            ["Yellowlegs"] = 0,
            ["Zigzag Heron"] = 1
        };

        var birdNames = GetListField<string>(comp, "birdNames").Get();
        var shuffledList = GetListField<int>(comp, "numberList").Get();
        var birdsPresent = shuffledList.Take(5).Where(ix => ix < 26).Select(ix => CheepCheckoutAudio[dict[birdNames[ix]]]).ToArray();

        yield return question(SCheepCheckout.Birds).Answers(birdsPresent, preferredWrong: CheepCheckoutAudio);
    }
}