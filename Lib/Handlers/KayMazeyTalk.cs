using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SKayMazeyTalk
{
    [SouvenirQuestion("What was the {1} word in {0}?", ThreeColumns6Answers, "Knit", "Knows", "Knock", "Knew", "Knoll", "Kneed", "Knuff", "Knork", "Knout", "Knits", "Knife", "Knights", "Knap", "Knee", "Knocks", "Knacks", "Knab", "Knocked", "Knight", "Knitch", "Knots", "Knish", "Knob", "Knox", "Knur", "Knook", "Know", "Knack", "Knurl", "Knot", Arguments = ["starting", "goal"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    QWord,

    [SouvenirDiscriminator("the KayMazey Talk whose {0} word was {1}", Arguments = ["starting", "Knit", "goal", "Knows"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DWord
}

public partial class SouvenirModule
{
    [SouvenirHandler("KMazeyTalk", "KayMazey Talk", typeof(SKayMazeyTalk), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessKayMazeyTalk(ModuleData module)
    {
        var comp = GetComponent(module, "kayMazeyTalkScript");

        var startingPosition = GetIntField(comp, "currentPosition").Get(min: 0, max: 35);
        var endingPosition = GetIntField(comp, "goalPosition").Get(min: 0, max: 35);

        var struck = false;
        module.Module.OnStrike += () => { struck = true; return false; };

        yield return WaitForSolve;

        string[] mazeWords = [
            "Knit",   "Knows",   "Knock",   "",       "Knew",   "Knoll",
            "Kneed",  "Knuff",   "Knork",   "Knout",  "Knits",  "",
            "Knife",  "Knights", "Knap",    "Knee",   "Knocks", "",
            "Knacks", "Knab",    "Knocked", "Knight", "Knitch", "",
            "Knots",  "Knish",   "Knob",    "Knox",   "Knur",   "",
            "Knook",  "Know",    "",        "Knack",  "Knurl",  "Knot"
        ];

        var goalWord = mazeWords[endingPosition];
        var startWord = mazeWords[startingPosition];

        if (!struck)
            yield return question(SKayMazeyTalk.QWord, args: ["starting"]).AvoidDiscriminators("start").Answers(startWord);
        yield return question(SKayMazeyTalk.QWord, args: ["goal"]).AvoidDiscriminators("goal").Answers(goalWord);

        yield return new Discriminator(SKayMazeyTalk.DWord, "start", startWord, args: ["starting", startWord], avoidAnswers: [startWord]);
        yield return new Discriminator(SKayMazeyTalk.DWord, "goal", goalWord, args: ["goal", goalWord], avoidAnswers: [goalWord]);
    }
}
