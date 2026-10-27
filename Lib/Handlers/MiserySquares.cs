using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMiserySquares
{
    [Question("What was the {1} color of the given sequence in {0}?", ThreeColumns6Answers, "Black", "Red", "Green", "Blue", "Yellow", "Magenta", "Cyan", "White", "Orange", "Brown", "Gray", "Purple", "Indigo", "Teal", "Lime", "Opal", "Olive", "Moonstone", "Menthol", "Peru", "Phlox", "Pistachio", "Rackley", "Quincy", "Rust", "Sapphire", "Tulip", "Tomato", "Toolbox", "Water", "Wine", "Watermelon", "Zinnwaldite", "Iceberg", "Ultramarine", "Tan", "Shampoo", "Salem", "Ruby", "Pear", "Oxley", "Lanzones", "Kobi", "Garnet", "Azure", "Bisque", "Artichoke", "Amber", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QSequence,

    [Discriminator("the Misery Squares whose {0} color given was {1}", Arguments = [QandA.Ordinal, "black", QandA.Ordinal, "red", QandA.Ordinal, "green", QandA.Ordinal, "blue", QandA.Ordinal, "yellow", QandA.Ordinal, "magenta", QandA.Ordinal, "cyan", QandA.Ordinal, "white", QandA.Ordinal, "orange", QandA.Ordinal, "brown", QandA.Ordinal, "gray", QandA.Ordinal, "purple", QandA.Ordinal, "indigo", QandA.Ordinal, "teal", QandA.Ordinal, "lime", QandA.Ordinal, "opal", QandA.Ordinal, "olive", QandA.Ordinal, "moonstone", QandA.Ordinal, "menthol", QandA.Ordinal, "peru", QandA.Ordinal, "phlox", QandA.Ordinal, "pistachio", QandA.Ordinal, "rackley", QandA.Ordinal, "quincy", QandA.Ordinal, "rust", QandA.Ordinal, "sapphire", QandA.Ordinal, "tulip", QandA.Ordinal, "tomato", QandA.Ordinal, "toolbox", QandA.Ordinal, "water", QandA.Ordinal, "wine", QandA.Ordinal, "watermelon", QandA.Ordinal, "zinnwaldite", QandA.Ordinal, "iceberg", QandA.Ordinal, "ultramarine", QandA.Ordinal, "tan", QandA.Ordinal, "shampoo", QandA.Ordinal, "salem", QandA.Ordinal, "ruby", QandA.Ordinal, "pear", QandA.Ordinal, "oxley", QandA.Ordinal, "lanzones", QandA.Ordinal, "kobi", QandA.Ordinal, "garnet", QandA.Ordinal, "azure", QandA.Ordinal, "bisque", QandA.Ordinal, "artichoke", QandA.Ordinal, "amber",], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DSequence
}

public partial class SouvenirModule
{
    [Handler("SquaresOfMisery", "Misery Squares", typeof(SMiserySquares), "Infusion")]
    [ManualQuestion("What were the colors in the given sequence?")]
    private IEnumerator<SouvenirInstruction> ProcessMiserySquares(ModuleData module)
    {
        var comp = GetComponent(module, "MiserySquaresScript");
        var sequence = GetArrayField<int[]>(comp, "Corporeal").Get(expectedLength: 2);
        var colorNames = new string[48] { "Black", "Red", "Green", "Blue", "Yellow", "Magenta", "Cyan", "White", "Orange", "Brown", "Gray", "Purple", "Indigo", "Teal", "Lime", "Opal", "Olive", "Moonstone", "Menthol", "Peru", "Phlox", "Pistachio", "Rackley", "Quincy", "Rust", "Sapphire", "Tulip", "Tomato", "Toolbox", "Water", "Wine", "Watermelon", "Zinnwaldite", "Iceberg", "Ultramarine", "Tan", "Shampoo", "Salem", "Ruby", "Pear", "Oxley", "Lanzones", "Kobi", "Garnet", "Azure", "Bisque", "Artichoke", "Amber" };

        yield return WaitForSolve;

        for (var i = 0; i < 10; i++)
        {
            var color = colorNames[sequence[0][i]];
            yield return new Discriminator(SMiserySquares.DSequence, $"color{i}", sequence[0][i], args: [Ordinal(i + 1), color]);
            yield return question(SMiserySquares.QSequence, args: [Ordinal(i + 1)]).AvoidDiscriminators($"letter{i}").Answers(color);
        }
    }
}
