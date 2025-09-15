using System.Collections.Generic;
using Souvenir;

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
        for (var pos = 0; pos < path.Count; pos++)
            yield return question(SFollowMe.DisplayedPath, args: [Ordinal(pos + 1)]).Answers(directionWords[path[pos]]);
    }
}