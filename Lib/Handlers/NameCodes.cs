using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNameCodes
{
    [SouvenirQuestion("What was the {1} index in {0}?", TwoColumns4Answers, "2", "3", "4", "5", TranslateArguments = [true], Arguments = ["left", "right"], ArgumentGroupSize = 1)]
    Indices
}

public partial class SouvenirModule
{
    [SouvenirHandler("nameCodes", "Name Codes", typeof(SNameCodes), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessNameCodes(ModuleData module)
    {
        var comp = GetComponent(module, "NameCodesScript");
        yield return WaitForSolve;

        var leftIx = GetIntField(comp, "leftIndex").Get().ToString();
        var rightIx = GetIntField(comp, "rightIndex").Get().ToString();
        addQuestions(module, new[] {
            makeQuestion(Question.NameCodesIndices, module, formatArgs: new[] { "left" }, correctAnswers: new[] { leftIx }),
            makeQuestion(Question.NameCodesIndices, module, formatArgs: new[] { "right" }, correctAnswers: new[] { rightIx }),
        });
    }
}