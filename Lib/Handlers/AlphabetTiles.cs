using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

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

        addQuestions(module,
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "first" }, correctAnswers: new[] { lettersShown[0] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "second" }, correctAnswers: new[] { lettersShown[1] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "third" }, correctAnswers: new[] { lettersShown[2] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { lettersShown[3] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { lettersShown[4] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "sixth" }, correctAnswers: new[] { lettersShown[5] }),
            makeQuestion(Question.AlphabetTilesMissingLetter, module, correctAnswers: new[] { shuffled[25] }));
    }
}