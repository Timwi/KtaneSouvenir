using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SBookOfMario
{
    [SouvenirQuestion("Who said the {1} quote in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Pictures,

    [SouvenirQuestion("What did {1} say in the {2} stage of {0}?", OneColumn4Answers, ExampleAnswers = ["Dark Koopatrol. These people just blow hard...", "I came, Mario! You finna", "Absolutely, I came! Got it!", "Well, I’m so desperate, so you better save me…"], Arguments = ["Goombell", QandA.Ordinal, "Prince Peach", QandA.Ordinal, "God Browser", QandA.Ordinal, "Mr.Krump", QandA.Ordinal, "Mario", QandA.Ordinal, "Flavio", QandA.Ordinal, "Quiz Thwomb", QandA.Ordinal, "Carbon", QandA.Ordinal, "Belda", QandA.Ordinal, "Make", QandA.Ordinal, "Yoshi Kid", QandA.Ordinal, "Bob", QandA.Ordinal, "Prosecutor Grubba", QandA.Ordinal], ArgumentGroupSize = 2)]
    Quotes
}

public partial class SouvenirModule
{
    [SouvenirHandler("BookOfMarioModule", "Book of Mario", typeof(SBookOfMario), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessBookOfMario(ModuleData module)
    {
        static string elideText(string s)
        {
            const int maxCount = 27;
            var str = s.Replace("\n", " ").Replace("  ", " ");
            return str.Length > maxCount ? str.Substring(0, maxCount) + "..." : str;
        }

        var comp = GetComponent(module, "BookOfMario");

        var quotes = GetStaticField<string[][]>(comp.GetType(), "quotes").Get(v => v.Length != 13 ? "expected length 13" : null)
            .Select(arr => arr.Select(elideText).ToArray()).ToArray();

        var spritesRaw = GetArrayField<Sprite>(comp, "genBtnSprites", isPublic: true).Get(expectedLength: 14, nullContentAllowed: true);
        if (spritesRaw.Select((spr, ix) => (spr == null) != (ix == 13)).Any(b => b))
            throw new AbandonModuleException($"Expected ‘genBtnSprites’ to have 13 sprites and one null value.");
        var sprites = spritesRaw.Take(13).TranslateSprites(1300f).ToArray();

        var fldStage = GetIntField(comp, "stage");
        var currentStage = 0;
        var fldNameIndex = GetIntField(comp, "x");
        var fldQuoteIndex = GetIntField(comp, "y");

        var answers = new List<(int charIx, int quoteIx)>();

        while (module.Unsolved)
        {
            var newStage = fldStage.Get();
            var answer = (characterIndex: fldNameIndex.Get(), quoteIndex: fldQuoteIndex.Get());
            if (currentStage != newStage)
            {
                answers.Add(answer);
                currentStage = newStage;
            }
            else
                answers[answers.Count - 1] = answer;
            yield return null;
        }

        for (var i = 0; i < answers.Count; i++)
        {
            var (charIx, quoteIx) = answers[i];
            yield return question(SBookOfMario.Pictures, args: [Ordinal(i + 1)]).Answers(sprites[charIx], all: sprites);
            if (charIx != 12 /* Yoshi Kid — has only three quotes */)
                yield return question(SBookOfMario.Quotes, args: [quotes[charIx][0], Ordinal(i + 1)])
                    .Answers(quotes[charIx][quoteIx], all: quotes[charIx].Skip(1).ToArray());
        }
    }
}
