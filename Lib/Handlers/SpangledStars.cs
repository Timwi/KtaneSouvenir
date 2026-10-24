using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSpangledStars
{
	[Question("Which of these colors appeared in the top three stars in {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet", TranslateAnswers = true)]
    QTopStars,

    [Question("What was the {1} flashed letter in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZ#")]
    QFlashedLetters,

    [Discriminator("the Spangled Stars that had {0} {1} star in the top three stars", TranslateArguments = [true, true], Arguments = ["a", "red", "an", "orange", "a", "yellow", "a", "green", "a", "blue", "an", "indigo", "a", "violet"], ArgumentGroupSize = 2)]
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

        var fldKeyOrder = GetField<string>(comp, "KeyOrder").Get();
        var fldTopStars = GetField<string>(comp, "TopThreeStars").Get();
        var colorNames = new List<string> { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };
        
        var fldWords = GetField<string>(comp, "UsedWords").Get();
        var fldOffset = GetField<int>(comp, "Offset").Get();

        var topColors = new string[3];
        var flashedLetters = new string[3];

        string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (var i = 0; i < 3; i++)
        {
            topColors[i] = colorNames[fldKeyOrder.IndexOf(fldTopStars[i])];
            flashedLetters[i] = Caesar(fldWords[i]).ToString();
        }
        
        char Caesar (char L) {
            if (L != '#') {
                return Alphabet[Alphabet.IndexOf(L) + fldOffset];
            } else {
                return '#';
            }
        }

        yield return WaitForSolve;

        var article = "";

        foreach (var c in topColors)
        {
            if (c == "Orange" || c == "Indigo")
                article = "an";
            else
                article = "a";
            yield return new Discriminator(SSpangledStars.DTopStars, $"stars-{c}", args: [article, c.ToLowerInvariant()], avoidAnswers: [c]);
        }
        
        yield return question(SSpangledStars.QTopStars).Answers(topColors);

        for (var i = 0; i < 3; i++)
        {
            yield return new Discriminator(SSpangledStars.DFlashedLetters, $"letter{i}", flashedLetters[i], args: [flashedLetters[i], Ordinal(i + 1)]);
            yield return question(SSpangledStars.QFlashedLetters, args: [Ordinal(i + 1)]).AvoidDiscriminators($"letter{i}").Answers(flashedLetters[i], preferredWrong: flashedLetters);
        }
    }
}