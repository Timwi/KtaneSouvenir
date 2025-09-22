using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SForgetUsNot
{
    // The format args for this question aren't ordinals because that would be ambiguous (i.e. does "first stage" refer to the stage displayed first or input first?)
    [SouvenirQuestion("Which module name was used for stage {1} in {0}?", OneColumn4Answers, ExampleAnswers = ["Souvenir", "The Button", "The Needlessly Complicated Button", "8", "Eight", "Zero, Zero"], Arguments = ["1", "2", "3", "4", "5"], ArgumentGroupSize = 1)]
    Stage,

    [SouvenirDiscriminator("the Forget Us Not in which {0} was used for stage {1}", Arguments = ["Memory", "1", "The Button", "2", "Polyhedral Maze", "1", "Coordinates", "2"], ArgumentGroupSize = 2)]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("forgetUsNot", "Forget Us Not", typeof(SForgetUsNot), "Anonymous", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessForgetUsNot(ModuleData module)
    {
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

        for (var i = 0; i < order.Count; i++)
        {
            yield return new Discriminator(SForgetUsNot.Discriminator, $"stage{i}", order[i], args: [order[i], (i + 1).ToString()]);
            yield return question(SForgetUsNot.Stage, args: [(i + 1).ToString()]).AvoidDiscriminators($"stage{i}").Answers(order[i], all: allAnswers, preferredWrong: wrongAnswers);
        }
    }
}
