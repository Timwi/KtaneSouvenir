using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNandMs
{
    [SouvenirQuestion("What was the label of the correct button in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings(5, 'M', 'N')]
    Answer
}

public partial class SouvenirModule
{
    [SouvenirHandler("NandMs", "N&Ms", typeof(SNandMs), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessNandMs(ModuleData module)
    {
        var comp = GetComponent(module, "NandMs");
        yield return WaitForSolve;

        var words = GetArrayField<string>(comp, "otherWords").Get();
        var index = GetIntField(comp, "otherwordindex").Get(min: 0, max: words.Length - 1);
        addQuestion(module, Question.NandMsAnswer, correctAnswers: new[] { words[index] });
    }
}