using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMorsematics
{
    [SouvenirQuestion("Which of these letters was {1} in {0}?", TwoColumns4Answers, Arguments = ["present", "not present"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings("A-Z")]
    QReceivedLetters,

    [SouvenirDiscriminator("the Morsematics that displayed the letter {0}", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"], ArgumentGroupSize = 1)]
    DReceivedLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("MorseV2", "Morsematics", typeof(SMorsematics), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMorsematics(ModuleData module)
    {
        var comp = GetComponent(module, "AdvancedMorse");
        var chars = GetArrayField<string>(comp, "DisplayCharsRaw").Get(expectedLength: 3);

        yield return WaitForSolve;

        var alph = Enumerable.Range('A', 26).Select(c => ((char) c).ToString()).ToArray();

        foreach (var ch in chars)
            yield return new Discriminator(SMorsematics.DReceivedLetters, $"ltrs-{ch}", args: [ch], avoidAnswers: [ch]);

        yield return question(SMorsematics.QReceivedLetters, args: ["present"])
            .Answers(chars);
        yield return question(SMorsematics.QReceivedLetters, args: ["not present"])
            .Answers(alph.Where(c => !chars.Contains(c)).ToArray(), preferredWrong: chars);
    }
}
