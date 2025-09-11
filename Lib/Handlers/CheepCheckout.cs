using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCheepCheckout
{
    [SouvenirQuestion("Which bird {1} present in {0}?", OneColumn4Answers, "Auklet", "Bluebird", "Chickadee", "Dove", "Egret", "Finch", "Godwit", "Hummingbird", "Ibis", "Jay", "Kinglet", "Loon", "Magpie", "Nuthatch", "Oriole", "Pipit", "Quail", "Raven", "Shrike", "Thrush", "Umbrellabird", "Vireo", "Warbler", "Xantus’s Hummingbird", "Yellowlegs", "Zigzag Heron", TranslateAnswers = true, Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Birds
}

public partial class SouvenirModule
{
    [SouvenirHandler("cheepCheckout", "Cheep Checkout", typeof(SCheepCheckout), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessCheepCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "cheepCheckoutScript");
        var fldUnicorn = GetField<bool>(comp, "unicorn");
        yield return WaitForSolve;

        if (fldUnicorn.Get())
            yield return legitimatelyNoQuestion(module, "The unicorn happened.");

        var shuffledList = GetField<List<int>>(comp, "numberList", isPublic: false).Get();
        var birdsPresent = shuffledList.Take(5).Where(ix => ix < 26).Select(ix => Question.CheepCheckoutBirds.GetAnswers()[ix]).ToArray();

        addQuestions(module,
           makeQuestion(Question.CheepCheckoutBirds, module, formatArgs: new[] { "was" }, correctAnswers: birdsPresent),
           makeQuestion(Question.CheepCheckoutBirds, module, formatArgs: new[] { "was not" }, correctAnswers: Question.CheepCheckoutBirds.GetAnswers().Except(birdsPresent).ToArray()));
    }
}