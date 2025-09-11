using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SForgetUsNot
{
    [SouvenirQuestion("Which module name was used for stage {1} in {0}?", OneColumn4Answers, ExampleAnswers = ["Souvenir", "The Button", "The Needlessly Complicated Button", "8", "Eight", "Zero, Zero"], Arguments = ["1", "2", "3", "4", "5"], ArgumentGroupSize = 1, TranslatableStrings = ["the Forget Us Not in which {0} was used for stage {1}"])]
    Stage
}

public partial class SouvenirModule
{
    [SouvenirHandler("forgetUsNot", "Forget Us Not", typeof(SForgetUsNot), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessForgetUsNot(ModuleData module)
    {
        // The format args for this question aren't ordinals because that would be ambiguous (i.e. does "first stage" refer to the stage displayed first or input first?)

        const string moduleId = "forgetUsNot";
        var comp = GetComponent(module, "AdvancedMemory");
        var foreignIgnored = new HashSet<string>(GetStaticField<string[]>(comp.GetType(), "ignoredModules", true).Get());
        var wrongAnswers = Bomb.GetSolvableModuleNames().Where(x => !foreignIgnored.Contains(x)).ToArray();
        if (wrongAnswers.Length == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        var solved = new List<string>();
        var order = new List<string>();

        while (!_noUnignoredModulesLeft)
        {
            var newCount = Bomb.GetSolvedModuleNames().Where(x => !foreignIgnored.Contains(x)).Count();
            if (newCount > solved.Count)
            {
                var nowSolved = Bomb.GetSolvedModuleNames().Where(x => !foreignIgnored.Contains(x)).ToList();
                var delta = nowSolved.ToList();
                foreach (var s in solved)
                    delta.Remove(s);
                solved = nowSolved;
                string name = null; // Replicating FUN's own logic with this. This is off if multiple modules solve within a few frames of one another, but it should be good enough
                foreach (var m in delta)
                    name = m;
                order.Add(name);
            }
            yield return null;
        }

        var stageOrder = GetArrayField<int>(comp, "Display").Get();
        order = order.Select((s, i) => (s, i: stageOrder[i])).OrderBy(t => t.i).Select(t => t.s).ToList();

        var allAnswers = Bomb.GetSolvableModuleNames().ToArray();

        if (_moduleCounts[moduleId] == 1)
        {
            addQuestions(module, order.Select((n, i) => makeQuestion(Question.ForgetUsNotStage, moduleId, 1, formatArgs: new[] { (i + 1).ToString() }, correctAnswers: new[] { n }, allAnswers: allAnswers, preferredWrongAnswers: wrongAnswers)));
            yield break;
        }

        _forgetUsNotStages.Add(order);
        yield return null;

        if (_forgetUsNotStages.Any(s => s.Count != order.Count))
            throw new AbandonModuleException("Stage counts were not consistent among modules.");

        var qs = new List<QandA>();

        for (var i = 0; i < order.Count; i++)
        {
            var n = order[i];
            var disambiguators = Enumerable.Range(0, order.Count).Where(x => x != i && _forgetUsNotStages.Count(s => s[x] == n) is 1).ToArray();
            if (disambiguators.Length == 0)
                continue;
            var d = disambiguators.PickRandom();
            var format = string.Format(translateString(Question.ForgetUsNotStage, "the Forget Us Not in which {0} was used for stage {1}"), order[d], d + 1);
            qs.Add(makeQuestion(Question.ForgetUsNotStage, module, formattedModuleName: format, formatArgs: new[] { (i + 1).ToString() }, correctAnswers: new[] { n }, allAnswers: allAnswers, preferredWrongAnswers: wrongAnswers));
        }

        if (qs.Count == 0)
            yield return legitimatelyNoQuestion(module, $"There were not enough stages in which this one(#{GetIntField(comp, "thisLoggingID", true).Get()}) was unique.");

        addQuestions(module, qs);
    }
}