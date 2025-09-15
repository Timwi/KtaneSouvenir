using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAlgebra
{
    [SouvenirQuestion("What was the first equation in {0}?", TwoColumns4Answers, "a=3z", "a=5+y", "a=6-x", "a=7x", "a=8y", "a=9+z", "a=x/2", "a=x+1", "a=y/4", "a=y-2", "a=z/10", "a=z-7")]
    Equation1,

    [SouvenirQuestion("What was the second equation in {0}?", TwoColumns4Answers, "b=(2x/10)-y", "b=(7x)y", "b=(x+y)-(z/2)", "b=(y/2)-z", "b=(zy)-(2x)", "b=(z-y)/2", "b=2(z+7)", "b=2z+7", "b=xy-(2+x)", "b=xyz")]
    Equation2
}

public partial class SouvenirModule
{
    [SouvenirHandler("algebra", "Algebra", typeof(SAlgebra), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessAlgebra(ModuleData module)
    {
        var comp = GetComponent(module, "algebraScript");
        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, 2).Select(i => makeQuestion(
            question: i == 0 ? Question.AlgebraEquation1 : Question.AlgebraEquation2,
            data: module,
            correctAnswers: new[] { GetField<Texture>(comp, $"level{i + 1}Equation").Get().name.Replace(';', '/') })));
    }
}