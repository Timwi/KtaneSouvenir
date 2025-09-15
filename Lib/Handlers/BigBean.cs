using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBigBean
{
    [SouvenirQuestion("What was the bean in {0}?", OneColumn4Answers, "Wobbly Orange", "Wobbly Yellow", "Wobbly Green", "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green", TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("bigBean", "Big Bean", typeof(SBigBean), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBigBean(ModuleData module)
    {
        var comp = GetComponent(module, "bigBeanScript");
        yield return WaitForSolve;

        var bn = GetField<int>(comp, "bean").Get(i => i is < 0 or >= 6 ? "Bad bean value" : null);

        var flavors = new[] { "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green", "Wobbly Orange", "Wobbly Yellow", "Wobbly Green" };
        int? match = bn switch
        {
            0 => 5,
            5 => 0,
            2 => 3,
            3 => 2,
            _ => null
        };
        addQuestion(module, Question.BigBeanColor, correctAnswers: new[] { flavors[bn] }, preferredWrongAnswers: match is null ? null : new[] { flavors[match.Value] });
    }
}