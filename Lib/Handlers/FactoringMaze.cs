using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFactoringMaze
{
    [SouvenirQuestion("What was one of the prime numbers chosen in {0}?", ThreeColumns6Answers, "2", "3", "5", "7", "11", "13", "17", "19", "23", "29")]
    ChosenPrimes
}

public partial class SouvenirModule
{
    [SouvenirHandler("factoringMaze", "Factoring Maze", typeof(SFactoringMaze), "Eltrick")]
    private IEnumerator<SouvenirInstruction> ProcessFactoringMaze(ModuleData module)
    {
        var comp = GetComponent(module, "FactoringMazeScript");
        yield return WaitForSolve;

        var chosenPrimes = GetArrayField<int>(comp, "chosenPrimes").Get(expectedLength: 4);
        yield return question(SFactoringMaze.ChosenPrimes).Answers(chosenPrimes.Select(i => i.ToString()).ToArray());
    }
}