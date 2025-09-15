using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMoreCode
{
    [SouvenirQuestion("What was the flashing word in {0}?", TwoColumns4Answers, "Allocate", "Bulwarks", "Compiler", "Disposal", "Encipher", "Formulae", "Gauntlet", "Hunkered", "Illusory", "Jousting", "Kinetics", "Linkwork", "Monolith", "Nanobots", "Octangle", "Postsync", "Quartics", "Revolved", "Stanzaic", "Tomahawk", "Ultrahot", "Vendetta", "Wafflers", "Yokozuna", "Zugzwang", "Allotype", "Bulkhead", "Computer", "Dispatch", "Encrypts", "Fortunes", "Gateways", "Huntress", "Illusion", "Junction", "Kilobyte", "Linkages", "Monogram", "Nanogram", "Octuples", "Positron", "Quintics", "Revealed", "Stoccata", "Tomogram", "Ultrared", "Venomous", "Weakened", "Xenolith", "Yeasayer", "Zymogram", ExampleAnswers = ["Allocate", "Bulwarks", "Compiler", "Disposal", "Encipher", "Formulae", "Gauntlet", "Hunkered", "Illusory", "Jousting", "Kinetics", "Linkwork"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("MoreCode", "More Code", typeof(SMoreCode), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessMoreCode(ModuleData module)
    {
        var comp = GetComponent(module, "MoreCode");
        yield return WaitForSolve;

        var word = GetField<string>(comp, "SolutionWord").Get();
        word = word.Substring(0, 1) + word.Substring(1).ToLowerInvariant();
        addQuestion(module, Question.MoreCodeWord, correctAnswers: new[] { word });
    }
}