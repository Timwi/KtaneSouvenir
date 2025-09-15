using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SASquare
{
    [SouvenirQuestion("Which of these was an index color in {0}?", ThreeColumns6Answers, "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White")]
    IndexColors,

    [SouvenirQuestion("Which color was submitted {1} in {0}?", ThreeColumns6Answers, "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    CorrectColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("ASquareModule", "A Square", typeof(SASquare), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessASquare(ModuleData module)
    {
        var comp = GetComponent(module, "ASquareScript");
        yield return WaitForSolve;
        var colorNames = new[] { "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White" };

        // Index colors
        var indexColors = GetListField<int>(comp, "_indexColors").Get();
        var indexColorNames = Enumerable.Range(0, indexColors.Count).Select(index => colorNames[indexColors[index]]).ToArray();

        yield return question(SASquare.IndexColors).Answers(indexColorNames, preferredWrong: colorNames);

        // Correct colors
        var correctColors = GetArrayField<int>(comp, "_correctColors").Get(expectedLength: 3);
        var correctColorNames = Enumerable.Range(0, correctColors.Length).Select(correct => colorNames[correctColors[correct]]).ToArray();
        for (var correct = 0; correct < 3; correct++)
            yield return question(SASquare.CorrectColors, args: [Ordinal(correct + 1)]).Answers(correctColorNames[correct], preferredWrong: colorNames);
    }
}