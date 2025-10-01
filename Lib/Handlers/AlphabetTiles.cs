using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAlphabetTiles
{
    [SouvenirQuestion("What was the {1} letter shown during the cycle in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QCycle,

    [SouvenirQuestion("What was the missing letter in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
    QMissingLetter,

    [SouvenirDiscriminator("the Alphabet Tiles where the {1} letter in the cycle was {0}", Arguments = [QandA.Ordinal, "X"], ArgumentGroupSize = 2)]
    DCycle,

    [SouvenirDiscriminator("the Alphabet Tiles whose missing letter was {0}", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"], ArgumentGroupSize = 1)]
    DMissingLetter,
}

public partial class SouvenirModule
{
    [SouvenirHandler("AlphabetTiles", "Alphabet Tiles", typeof(SAlphabetTiles), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessAlphabetTiles(ModuleData module)
    {
        var comp = GetComponent(module, "AlphabetTilesScript");

        yield return WaitForSolve;

        var shuffled = GetArrayField<string>(comp, "ShuffledAlphabet").Get(expectedLength: 26);
        var lettersShown = GetArrayField<string>(comp, "LettersShown").Get(expectedLength: 6);

        for (var ltrIx = 0; ltrIx < 6; ltrIx++)
        {
            yield return question(SAlphabetTiles.QCycle, args: [Ordinal(ltrIx + 1)]).AvoidDiscriminators(SAlphabetTiles.DCycle).Answers(lettersShown[ltrIx]);
            yield return new Discriminator(SAlphabetTiles.DCycle, $"ltr-{ltrIx}", lettersShown[ltrIx], args: [lettersShown[ltrIx], Ordinal(ltrIx + 1)]);
        }
        yield return question(SAlphabetTiles.QMissingLetter).AvoidDiscriminators(SAlphabetTiles.DMissingLetter).Answers(shuffled[25]);
        yield return new Discriminator(SAlphabetTiles.DMissingLetter, "missing", shuffled[25], args: [shuffled[25]]);
    }
}
