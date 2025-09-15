using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBeans
{
    [SouvenirQuestion("What was this bean in {0}?", OneColumn4Answers, "Wobbly Orange", "Wobbly Yellow", "Wobbly Green", "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green", UsesQuestionSprite = true, TranslateAnswers = true)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("beans", "Beans", typeof(SBeans), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBeans(ModuleData module)
    {
        var comp = GetComponent(module, "beansScript");
        yield return WaitForSolve;

        var bns = GetField<int[]>(comp, "beanArray").Get(a => a.Length != 9 ? "Bad length" : a.Any(i => i is < 0 or >= 6) ? "Bad bean value" : null);
        var eaten = GetField<KMSelectable[]>(comp, "Beans", true).Get(a => a.Length != 9 ? "Bad length" : null);

        var flavors = new[] { "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green", "Wobbly Orange", "Wobbly Yellow", "Wobbly Green" };

        addQuestions(module, Enumerable.Range(0, 9)
            .Where(i => eaten[i].transform.localScale.magnitude <= Mathf.Epsilon)
            .Select(i => makeQuestion(SBeans.Colors, module, questionSprite: Sprites.GenerateGridSprite(3, 3, i), correctAnswers: new string[] { flavors[bns[i]] })));
    }
}