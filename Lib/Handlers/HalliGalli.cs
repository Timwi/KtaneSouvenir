using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHalliGalli
{
    [SouvenirQuestion("Which fruit were there five of in {0}?", TwoColumns4Answers, "Strawberries", "Melons", "Lemons", "Raspberries", "Bananas", TranslateAnswers = true)]
    Fruit,

    [SouvenirQuestion("What were the relevant counts in {0}?", TwoColumns4Answers, "5", "1 4", "2 3", "1 1 3", "1 2 2")]
    Counts
}

public partial class SouvenirModule
{
    [SouvenirHandler("halliGalli", "Halli Galli", typeof(SHalliGalli), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessHalliGalli(ModuleData module)
    {
        var comp = GetComponent(module, "halliGalli");
        var bell = GetField<KMSelectable>(comp, "bell", isPublic: true).Get();
        var stage = GetIntField(comp, "stage");
        var fruit = -1;
        var figure = "";

        var oldInteract = bell.OnInteract;
        bell.OnInteract = () =>
        {
            if (stage.Get(min: 0, max: 2) != 1) return oldInteract();

            var fruits = GetArrayField<int>(comp, "displayedFruits").Get(expectedLength: 3);
            var counts = GetArrayField<int>(comp, "displayedCounts").Get(expectedLength: 3);

            fruit = -1;
            for (var i = 0; i < 5; i++)
                if (Enumerable.Range(0, 3).Where(j => fruits[j] == i).Select(j => counts[j]).Sum() == 5)
                    fruit = i;
            var contrib = new List<int>(3);
            for (var i = 0; i < 3; i++)
                if (fruits[i] == fruit)
                    contrib.Add(counts[i]);
            figure = contrib.OrderBy(x => x).JoinString(" ");

            return oldInteract();
        };

        yield return WaitForSolve;

        if (fruit == -1 || figure == "")
            throw new AbandonModuleException($"The solution was somehow missed. (fruit={fruit}, figure={figure})");

        addQuestions(module,
            makeQuestion(Question.HalliGalliFruit, module, correctAnswers: new[] { Question.HalliGalliFruit.GetAnswers()[fruit] }),
            makeQuestion(Question.HalliGalliCounts, module, correctAnswers: new[] { figure }));
    }
}