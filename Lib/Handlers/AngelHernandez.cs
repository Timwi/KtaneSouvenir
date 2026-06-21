using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAngelHernandez
{
    [Question("What letter was shown by the raised buttons on the {1} stage on {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    MainLetter,

    [Discriminator("the Ángel Hernández where the {0}-stage letter was {1}", Arguments = [QandA.Ordinal, "A", QandA.Ordinal, "B", QandA.Ordinal, "C", QandA.Ordinal, "D", QandA.Ordinal, "E", QandA.Ordinal, "F", QandA.Ordinal, "G", QandA.Ordinal, "H", QandA.Ordinal, "I", QandA.Ordinal, "J", QandA.Ordinal, "K", QandA.Ordinal, "L", QandA.Ordinal, "M", QandA.Ordinal, "N", QandA.Ordinal, "O", QandA.Ordinal, "P", QandA.Ordinal, "Q", QandA.Ordinal, "R", QandA.Ordinal, "S", QandA.Ordinal, "T", QandA.Ordinal, "U", QandA.Ordinal, "V", QandA.Ordinal, "W", QandA.Ordinal, "X", QandA.Ordinal, "Y", QandA.Ordinal, "Z"], ArgumentGroupSize = 2)]
    Discriminator
}

public partial class SouvenirModule
{
    [Handler("AngelHernandezModule", "Ángel Hernández", typeof(SAngelHernandez), "Quinn Wuest")]
    [ManualQuestion("What letter was shown by the raised buttons in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessAngelHernandez(ModuleData module)
    {
        var comp = GetComponent(module, "AngelHernandezScript");
        var fldActivated = GetField<bool>(comp, "_canPress");
        var fldStage = GetIntField(comp, "_currentStage");
        var fldMainLetter = GetIntField(comp, "_mainLetter");

        while (!fldActivated.Get())
            yield return null;

        var displayedLetters = new string[2];
        var alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(i => i.ToString()).ToArray();

        for (var i = 0; i < 2; i++)
        {
            while (fldStage.Get() == i)
            {
                while (!fldActivated.Get())
                    yield return null;

                displayedLetters[i] = alph[fldMainLetter.Get()];

                while (fldActivated.Get())
                    yield return null;
            }
        }

        yield return WaitForSolve;
        for (var stage = 0; stage < displayedLetters.Length; stage++)
        {
            yield return question(SAngelHernandez.MainLetter, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"a-{stage}").Answers(displayedLetters[stage], preferredWrong: alph);
            yield return new Discriminator(SAngelHernandez.Discriminator, $"a-{stage}", displayedLetters[stage], args: [Ordinal(stage + 1), displayedLetters[stage]]);
        }
    }
}
