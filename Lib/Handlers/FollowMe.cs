using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFollowMe
{
    [SouvenirQuestion("What was the {1} flashing direction in {0}?", TwoColumns4Answers, "Up", "Down", "Left", "Right", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    DisplayedPath
}

public partial class SouvenirModule
{
    [SouvenirHandler("FollowMe", "Follow Me", typeof(SFollowMe), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessFollowMe(ModuleData module)
    {
        var comp = GetComponent(module, "FollowMe");

        yield return WaitForSolve;

        var directionWords = new Dictionary<string, string> { { "U", "Up" }, { "D", "Down" }, { "L", "Left" }, { "R", "Right" } };
        var path = GetListField<string>(comp, "Path").Get(minLength: 1, validator: x => !directionWords.ContainsKey(x) ? $"expected only {directionWords.Keys.JoinString(", ")}" : null);

        var qs = new List<QandA>();
        for (var pos = 0; pos < path.Count; pos++)
            qs.Add(makeQuestion(Question.FollowMeDisplayedPath, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { directionWords[path[pos]] }));
        addQuestions(module, qs);
    }
}