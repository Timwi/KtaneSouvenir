using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBoomtarTheGreat
{
    [SouvenirQuestion("What was rule {1} in {0}?", ThreeColumns6Answers, ArgumentGroupSize = 1, Arguments = ["one", "two"], TranslateArguments = [true])]
    [AnswerGenerator.Integers(1, 6)]
    Rules
}

public partial class SouvenirModule
{
    [SouvenirHandler("boomtarTheGreat", "Boomtar the Great", typeof(SBoomtarTheGreat), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBoomtarTheGreat(ModuleData module)
    {
        var comp = GetComponent(module, "boomtarTheGreat");
        yield return WaitForSolve;

        var rule1 = GetField<int>(comp, "rule1").Get(i => i is < 0 or > 5 ? "Bad rule 1" : null);
        var rule2 = GetField<int>(comp, "rule2").Get(i => i is < 0 or > 5 ? "Bad rule 2" : null);

        addQuestions(module,
            makeQuestion(Question.BoomtarTheGreatRules, module, formatArgs: new string[] { "one" }, correctAnswers: new string[] { (rule1 + 1).ToString() }, preferredWrongAnswers: new string[] { (rule2 + 1).ToString() }),
            makeQuestion(Question.BoomtarTheGreatRules, module, formatArgs: new string[] { "two" }, correctAnswers: new string[] { (rule2 + 1).ToString() }, preferredWrongAnswers: new string[] { (rule1 + 1).ToString() }));
    }
}