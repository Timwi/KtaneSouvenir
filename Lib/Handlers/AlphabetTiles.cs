using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAlphabetTiles
{
    [SouvenirQuestion("What was the {1} letter shown during the cycle in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Cycle,

    [SouvenirQuestion("What was the missing letter in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
    MissingLetter
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

        yield return question(SAlphabetTiles.Cycle, args: [Ordinal(1)]).Answers(lettersShown[0]);
        yield return question(SAlphabetTiles.Cycle, args: [Ordinal(2)]).Answers(lettersShown[1]);
        yield return question(SAlphabetTiles.Cycle, args: [Ordinal(3)]).Answers(lettersShown[2]);
        yield return question(SAlphabetTiles.Cycle, args: [Ordinal(4)]).Answers(lettersShown[3]);
        yield return question(SAlphabetTiles.Cycle, args: [Ordinal(5)]).Answers(lettersShown[4]);
        yield return question(SAlphabetTiles.Cycle, args: [Ordinal(6)]).Answers(lettersShown[5]);
        yield return question(SAlphabetTiles.MissingLetter).Answers(shuffled[25]);
    }
}
