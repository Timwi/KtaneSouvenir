using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S100LevelsOfDefusal
{
    [SouvenirQuestion("What was the {1} displayed letter in {0}?", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Letters,

    [SouvenirDiscriminator("the 100 Levels of Defusal where the {0} displayed letter was {1}", Arguments = [QandA.Ordinal, "B", QandA.Ordinal, "C", QandA.Ordinal, "D"], ArgumentGroupSize = 2)]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("100LevelsOfDefusal", "100 Levels of Defusal", typeof(S100LevelsOfDefusal), "Espik")]
    private IEnumerator<SouvenirInstruction> Process100LevelsOfDefusal(ModuleData module)
    {
        var comp = GetComponent(module, "OneHundredLevelsOfDefusal");

        yield return WaitForSolve;

        var display = GetArrayField<char>(comp, "displayedLetters").Get(expectedLength: 12);
        foreach (var (ch, i) in display.Where(c => c != '.').Select((c, i) => (ch: c.ToString(), i)))
        {
            yield return new Discriminator(S100LevelsOfDefusal.Discriminator, $"stage{i}", ch, [Ordinal(i + 1), ch.ToString()]);
            yield return question(S100LevelsOfDefusal.Letters, args: [Ordinal(i + 1)]).AvoidDiscriminators($"stage{i}").Answers(ch);
        }
    }
}
