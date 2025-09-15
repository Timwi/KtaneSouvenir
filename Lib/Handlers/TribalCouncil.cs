using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum STribalCouncil
{
    [SouvenirQuestion("What was the {1} name in {0}?", TwoColumns4Answers, "Louise", "Mark", "Hannah", "Adam", "Harvey", "Maria", "Jonathan", "Carolyn", "Stacy", "Bob", Arguments = ["northeast", "southwest"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Name
}

public partial class SouvenirModule
{
    [SouvenirHandler("TribalCouncil", "Tribal Council", typeof(STribalCouncil), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessTribalCouncil(ModuleData module)
    {
        var comp = GetComponent(module, "tribalCouncilScript");
        var texts = GetArrayField<TextMesh>(comp, "namesText", true).Get(expectedLength: 6);

        var ne = texts[1].text;
        var sw = texts[4].text;

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.TribalCouncilName, module, correctAnswers: new[] { ne }, formatArgs: new[] { "northeast" }),
            makeQuestion(Question.TribalCouncilName, module, correctAnswers: new[] { sw }, formatArgs: new[] { "southwest" }));
    }
}