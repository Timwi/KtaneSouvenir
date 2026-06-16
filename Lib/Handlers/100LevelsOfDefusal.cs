using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S100LevelsOfDefusal
{
    [Question("Which letter was displayed in {0}?", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
    Letters,

    [Discriminator("the 100 Levels of Defusal that had {0} on it", Arguments = ["a B", "a C", "a D", "an F", "a G", "an H", "a J", "a K", "an L", "an M", "an N", "a P", "a Q", "an R", "an S", "a T", "a V", "a W", "an X", "a Y", "a Z"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Discriminator
}

public partial class SouvenirModule
{
    [Handler("100LevelsOfDefusal", "100 Levels of Defusal", typeof(S100LevelsOfDefusal), "Espik")]
    [ManualQuestion("What were the displayed letters?")]
    private IEnumerator<SouvenirInstruction> Process100LevelsOfDefusal(ModuleData module)
    {
        var comp = GetComponent(module, "OneHundredLevelsOfDefusal");

        yield return WaitForSolve;

        var display = GetArrayField<char>(comp, "displayedLetters").Get(expectedLength: 12);
        var letters = display.Where(x => x != '.').Select(x => x.ToString()).ToArray();

        yield return question(S100LevelsOfDefusal.Letters).Answers(letters);

        var dict = "Ba B,Ca C,Da D,Fan F,Ga G,Han H,Ja J,Ka K,Lan L,Man M,Nan N,Pa P,Qa Q,Ran R,San S,Ta T,Va V,Wa W,Xan X,Ya Y,Za Z"
            .Split(',').ToDictionary(s => s[0], s => s.Substring(1));
        foreach (var ltr in letters)
            yield return new Discriminator(S100LevelsOfDefusal.Discriminator, $"100lod-{ltr}", null, args: [dict[ltr[0]]], avoidAnswers: [ltr]);
    }
}
