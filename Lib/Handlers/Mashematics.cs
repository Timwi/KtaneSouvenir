using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMashematics
{
    [Question("What was the {1} number in the equation on {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 99)]
    Calculation
}

public partial class SouvenirModule
{
    [Handler("mashematics", "Mashematics", typeof(SMashematics), "Marksam")]
    [ManualQuestion("What were the numbers in the equation?")]
    private IEnumerator<SouvenirInstruction> ProcessMashematics(ModuleData module)
    {
        var comp = GetComponent(module, "mashematicsScript");
        yield return WaitForSolve;

        var numberClass = GetField<object>(comp, "number").Get();
        var number1 = GetField<int>(numberClass, "Number1", isPublic: true).Get();
        var number2 = GetField<int>(numberClass, "Number2", isPublic: true).Get();
        var number3 = GetField<int>(numberClass, "Number3", isPublic: true).Get();

        for (var i = 0; i < 3; i++)
        {
            var number = i == 0 ? number1 : (i == 1 ? number2 : number3);
            yield return question(SMashematics.Calculation, args: [Ordinal(i + 1)]).Answers(number.ToString());
        }
    }
}
