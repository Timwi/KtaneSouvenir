using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SKayMazeyTalk
{
    [SouvenirQuestion("What was the {1} word in {0}?", ThreeColumns6Answers, "Knit", "Knows", "Knock", "Knew", "Knoll", "Kneed", "Knuff", "Knork", "Knout", "Knits", "Knife", "Knights", "Knap", "Knee", "Knocks", "Knacks", "Knab", "Knocked", "Knight", "Knitch", "Knots", "Knish", "Knob", "Knox", "Knur", "Knook", "Know", "Knack", "Knurl", "Knot", Arguments = ["starting", "goal"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("KMazeyTalk", "KayMazey Talk", typeof(SKayMazeyTalk), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessKayMazeyTalk(ModuleData module)
    {
        var comp = GetComponent(module, "kayMazeyTalkScript");

        var startingPosition = GetIntField(comp, "currentPosition").Get(min: 0, max: 35);
        var endingPosition = GetIntField(comp, "goalPosition").Get(min: 0, max: 35);
        _kayMazeyTalkInfo.Add((startingPosition, endingPosition));

        var struck = false;
        module.Module.OnStrike += () => { struck = true; return false; };

        yield return WaitForSolve;

        string[] mazeWords = {
            "Knit",   "Knows",   "Knock",   "",       "Knew",   "Knoll",
            "Kneed",  "Knuff",   "Knork",   "Knout",  "Knits",  "",
            "Knife",  "Knights", "Knap",    "Knee",   "Knocks", "",
            "Knacks", "Knab",    "Knocked", "Knight", "Knitch", "",
            "Knots",  "Knish",   "Knob",    "Knox",   "Knur",   "",
            "Knook",  "Know",    "",        "Knack",  "Knurl",  "Knot"
        };

        var endWord = mazeWords[endingPosition];
        var startWord = mazeWords[startingPosition];

        IEnumerable<QandA> qs()
        {
            string startFormat = null;
            var usesFormat = _moduleCounts["KMazeyTalk"] > 1;

            if (!struck)
            {
                if (_kayMazeyTalkInfo.Count(i => i.start == startingPosition) == 1)
                    startFormat = string.Format(translateString(Question.KayMazeyTalkWord, "the KayMazey Talk whose starting word was {0}"), startWord);

                string endFormat = null;
                if (_kayMazeyTalkInfo.Count(i => i.end == endingPosition) == 1)
                    endFormat = string.Format(translateString(Question.KayMazeyTalkWord, "the KayMazey Talk whose goal word was {0}"), endWord);

                yield return makeQuestion(
                    Question.KayMazeyTalkWord,
                    module,
                    formattedModuleName: endFormat,
                    correctAnswers: new[] { startWord },
                    preferredWrongAnswers: usesFormat && endFormat != null ? new string[0] : new[] { endWord },
                    formatArgs: new[] { "starting" });
            }

            yield return makeQuestion(
                Question.KayMazeyTalkWord,
                module,
                formattedModuleName: startFormat,
                correctAnswers: new[] { endWord },
                preferredWrongAnswers: usesFormat && startFormat != null ? new string[0] : new[] { startWord },
                formatArgs: new[] { "ending" });
        }

        addQuestions(module, qs());
    }
}
