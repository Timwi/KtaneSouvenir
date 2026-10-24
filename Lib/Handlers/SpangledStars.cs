using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSpangledStars
{
    [Question("Which of these colors appeared among the top three stars in {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet", TranslateAnswers = true)]
    QTopStars,

    [Question("What was the {1} flashed letter in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZ#")]
    QFlashedLetters,

    [Discriminator("the Spangled Stars that had {0} among the top three stars", TranslateArguments = [true], Arguments = ["a red star", "an orange star", "a yellow star", "a green star", "a blue star", "an indigo star", "a violet star"], ArgumentGroupSize = 1)]
    DTopStars,

    [Discriminator("the Spangled Stars where {0} was the {1} letter flashed", Arguments = ["A", QandA.Ordinal, "B", QandA.Ordinal, "C", QandA.Ordinal, "#", QandA.Ordinal], ArgumentGroupSize = 2)]
    DFlashedLetters
}

public partial class SouvenirModule
{
    [Handler("spangledStars", "Spangled Stars", typeof(SSpangledStars), "Infusion")]
    [ManualQuestion("What were the colors of the top three stars?")]
    [ManualQuestion("What were the flashed letters?")]
    private IEnumerator<SouvenirInstruction> ProcessSpangledStars(ModuleData module)
    {
        var comp = GetComponent(module, "spangledStarsScript");

        var keyOrder = GetField<string>(comp, "KeyOrder").Get();
        var topStars = GetField<string>(comp, "TopThreeStars").Get();
        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };
        var colorNamesWithArticle = new[] { "a red star", "an orange star", "a yellow star", "a green star", "a blue star", "an indigo star", "a violet star" };

        var words = GetField<string>(comp, "UsedWords").Get();
        var offset = GetField<int>(comp, "Offset").Get();

        var topColors = new int[3];
        var flashedLetters = new string[3];

        for (var i = 0; i < 3; i++)
        {
            char caesar(char ltr) => ltr == '#' ? '#' : (char) ('A' + (ltr - 'A' + offset) % 26);
            topColors[i] = keyOrder.IndexOf(topStars[i]);
            flashedLetters[i] = caesar(words[i]).ToString();
        }

        yield return WaitForSolve;

        foreach (var c in topColors)
            yield return new Discriminator(SSpangledStars.DTopStars, $"stars-{c}", args: [colorNamesWithArticle[c]], avoidAnswers: [colorNames[c]]);

        yield return question(SSpangledStars.QTopStars).Answers(topColors.Select(c => colorNames[c]).ToArray());

        for (var i = 0; i < 3; i++)
        {
            yield return new Discriminator(SSpangledStars.DFlashedLetters, $"letter{i}", flashedLetters[i], args: [flashedLetters[i], Ordinal(i + 1)]);
            yield return question(SSpangledStars.QFlashedLetters, args: [Ordinal(i + 1)]).AvoidDiscriminators($"letter{i}").Answers(flashedLetters[i], preferredWrong: flashedLetters);
        }
    }
}
