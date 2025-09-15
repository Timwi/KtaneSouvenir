using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBookOfMario
{
    [SouvenirQuestion("Who said the {1} quote in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "BookOfMarioSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Pictures,

    [SouvenirQuestion("What did {1} say in the {2} stage of {0}?", OneColumn4Answers, ExampleAnswers = ["Dark Koopatrol. These people just blow hard...", "I came, Mario! You finna", "Absolutely, I came! Got it!", "Well, I’m so desperate, so you better save me…"], Arguments = ["Goombell", QandA.Ordinal, "Prince Peach", QandA.Ordinal, "God Browser", QandA.Ordinal, "Mr.Krump", QandA.Ordinal, "Mario", QandA.Ordinal, "Flavio", QandA.Ordinal, "Quiz Thwomb", QandA.Ordinal, "Carbon", QandA.Ordinal, "Belda", QandA.Ordinal, "Make", QandA.Ordinal, "Yoshi Kid", QandA.Ordinal, "Bob", QandA.Ordinal, "Prosecutor Grubba", QandA.Ordinal], ArgumentGroupSize = 2)]
    Quotes
}

public partial class SouvenirModule
{
    [SouvenirHandler("BookOfMarioModule", "Book of Mario", typeof(SBookOfMario), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessBookOfMario(ModuleData module)
    {
        var comp = GetComponent(module, "BookOfMario");
        var fldStage = GetIntField(comp, "stage");
        var currentStage = 0;
        var fldNameIndex = GetIntField(comp, "x");
        var fldQuoteIndex = GetIntField(comp, "y");
        var quotes = GetStaticField<string[][]>(comp.GetType(), "quotes").Get();

        var answer = new List<(string name, string quote)>();
        var dictionary = new Dictionary<string, string[]>();

        (string, string) GetPersonKeyValue(int characterIndex, int characterQuote) =>
            (UpdateString(quotes[characterIndex][0]), UpdateString(quotes[characterIndex][fldQuoteIndex.Get()]));

        while (module.Unsolved)
        {
            var characterIndex = fldNameIndex.Get();
            var quoteIndex = fldQuoteIndex.Get();
            var newStage = fldStage.Get();
            var person = GetPersonKeyValue(characterIndex, quoteIndex);

            if (currentStage != newStage)
            {
                answer.Add(person);
                currentStage = newStage;
            }
            else
            {
                if (!answer.Last().Equals(person))
                    answer[answer.Count - 1] = person;
            }
            yield return null;
        }

        if (BookOfMarioSprites.Length != 13)
            throw new AbandonModuleException($"Book of Mario should have 13 sprites. Counted {BookOfMarioSprites.Length}");

        string UpdateString(string s)
        {
            const int maxCount = 27;
            var str = s.Replace("\n", " ").Replace("  ", " ");
            return str.Length > maxCount ? str.Substring(0, maxCount) + "..." : str;
        }

        string[] GetUpdatedQuotes(string name)
        {
            foreach (var q in quotes)
                if (q[0].Replace("\n", "") == name)
                    return Enumerable.Range(1, q.Length - 1).Select(i => UpdateString(q[i])).ToArray();
            return null;
        }

        var unaviableCharacters = new[] { "Bob", "God Browser", "Flavio", "Make", "Quiz Thwomb", "Yoshi Kid" };

        for (var i = 0; i < answer.Count; i++)
        {
            var (name, quote) = answer[i];
            yield return question(SBookOfMario.Pictures, args: [Ordinal(i + 1)]).Answers(BookOfMarioSprites.First(sprite => sprite.name == name), preferredWrong: BookOfMarioSprites);

            if (!unaviableCharacters.Contains(name))
            {
                yield return question(SBookOfMario.Quotes, args: [name, Ordinal(i + 1)]).Answers(quote, preferredWrong: GetUpdatedQuotes(name));
            }
        }
    }
}