using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPictionary
{
    [SouvenirQuestion("What was the code in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("0-579", "0-68", "0-7", "0-68")]
    Code
}

public partial class SouvenirModule
{
    [SouvenirHandler("pictionaryModule", "Pictionary", typeof(SPictionary), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessPictionary(ModuleData module)
    {
        var comp = GetComponent(module, "pictionaryModuleScript");

        yield return WaitForSolve;

        var code = GetField<string>(comp, "code").Get(c => c.Length != 4 || c.Any(ch => !char.IsDigit(ch)) ? "expected a sequence of four digits" : null);
        addQuestion(module, Question.PictionaryCode, correctAnswers: new[] { code });
    }
}