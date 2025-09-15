using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBeanSprouts
{
    [SouvenirQuestion("What was sprout {1} in {0}?", TwoColumns4Answers, "Raw", "Cooked", "Burnt", "Fake", TranslateAnswers = true, ArgumentGroupSize = 1, Arguments = ["1", "2", "3", "4", "5", "6", "7", "8", "9"])]
    Colors,

    [SouvenirQuestion("What bean was on sprout {1} in {0}?", TwoColumns4Answers, "Left", "Right", "None", "Both", TranslateAnswers = true, ArgumentGroupSize = 1, Arguments = ["1", "2", "3", "4", "5", "6", "7", "8", "9"])]
    Beans
}

public partial class SouvenirModule
{
    [SouvenirHandler("beanSprouts", "Bean Sprouts", typeof(SBeanSprouts), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBeanSprouts(ModuleData module)
    {
        var comp = GetComponent(module, "beanSproutsScript");
        yield return WaitForSolve;

        var bns = GetField<int[]>(comp, "beanArray").Get(a => a.Length != 9 ? "Bad length" : a.Any(i => i is < 0 or >= 9) ? "Bad bean value" : null);
        var eaten = GetField<KMSelectable[]>(comp, "Beans", true).Get(a => a.Length != 9 ? "Bad length" : null);

        var flavors = new[] { "Raw", "Cooked", "Burnt" };
        var flavors2 = new[] { "Left", "None", "Right" };
        IEnumerable<QandA> beansQ(int i)
        {
            yield return makeQuestion(SBeanSprouts.Colors, module, formatArgs: new string[] { (i + 1).ToString() }, correctAnswers: new string[] { flavors[bns[i] % 3] });
            yield return makeQuestion(SBeanSprouts.Beans, module, formatArgs: new string[] { (i + 1).ToString() }, correctAnswers: new string[] { flavors2[bns[i] / 3] });
        }
        addQuestions(module, Enumerable.Range(0, 9).Where(i => eaten[i].transform.localScale.magnitude <= Mathf.Epsilon).SelectMany(beansQ));
    }
}