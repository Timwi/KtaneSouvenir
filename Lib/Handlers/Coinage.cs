using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCoinage
{
    [SouvenirQuestion("Which coin was flipped in {0}?", ThreeColumns6Answers, ExampleAnswers = ["e4", "h5", "d4", "h4", "c4", "h3", "c3", "g2", "f3", "h1", "f7"])]
    Flip
}

public partial class SouvenirModule
{
    [SouvenirHandler("Coinage", "Coinage", typeof(SCoinage), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessCoinage(ModuleData module)
    {
        var comp = GetComponent(module, "CoinageScript");
        yield return WaitForSolve;

        yield return question(SCoinage.Flip).Answers(GetField<string>(comp, "souvenirCoin").Get(), preferredWrong: Enumerable.Range(0, 64).Select(i => "abcdefgh"[i % 8].ToString() + "87654321"[i / 8]).ToArray());
    }
}