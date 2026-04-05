using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAlgebra
{
    [Question("What was the first equation in {0}?", TwoColumns4Answers, "a=3z", "a=5+y", "a=6-x", "a=7x", "a=8y", "a=9+z", "a=x/2", "a=x+1", "a=y/4", "a=y-2", "a=z/10", "a=z-7")]
    Equation1,

    [Question("What was the second equation in {0}?", TwoColumns4Answers, "b=(2x/10)-y", "b=(7x)y", "b=(x+y)-(z/2)", "b=(y/2)-z", "b=(zy)-(2x)", "b=(z-y)/2", "b=2(z+7)", "b=2z+7", "b=xy-(2+x)", "b=xyz")]
    Equation2,

    [Discriminator("the Algebra where the first equation was {0}", Arguments = ["a=3z", "a=5+y", "a=6-x", "a=7x", "a=8y", "a=9+z", "a=x/2", "a=x+1", "a=y/4", "a=y-2", "a=z/10", "a=z-7"], ArgumentGroupSize = 1)]
    Discriminator1,

    [Discriminator("the Algebra where the second equation was {0}", Arguments = ["b=(2x/10)-y", "b=(7x)y", "b=(x+y)-(z/2)", "b=(y/2)-z", "b=(zy)-(2x)", "b=(z-y)/2", "b=2(z+7)", "b=2z+7", "b=xy-(2+x)", "b=xyz"], ArgumentGroupSize = 1)]
    Discriminator2
}

public partial class SouvenirModule
{
    [Handler("algebra", "Algebra", typeof(SAlgebra), "Timwi")]
    [ManualQuestion("What were the first two equations?")]
    private IEnumerator<SouvenirInstruction> ProcessAlgebra(ModuleData module)
    {
        var comp = GetComponent(module, "algebraScript");
        yield return WaitForSolve;

        for (var i = 0; i < 2; i++)
        {
            var eqn = GetField<Texture>(comp, $"level{i + 1}Equation").Get().name.Replace(';', '/');
            yield return question(i == 0 ? SAlgebra.Equation1 : SAlgebra.Equation2)
                .AvoidDiscriminators(i == 0 ? SAlgebra.Discriminator1 : SAlgebra.Discriminator2)
                .Answers(eqn);
            yield return new Discriminator(i == 0 ? SAlgebra.Discriminator1 : SAlgebra.Discriminator2, $"d{i}", eqn, args: [eqn]);
        }
    }
}
