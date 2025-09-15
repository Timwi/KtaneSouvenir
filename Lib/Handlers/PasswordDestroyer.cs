using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPasswordDestroyer
{
    [SouvenirQuestion("What was the 2FAST™ value when you solved {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(100100, 999999)]
    TwoFactorV2
}

public partial class SouvenirModule
{
    [SouvenirHandler("pwDestroyer", "Password Destroyer", typeof(SPasswordDestroyer), "Eltrick")]
    private IEnumerator<SouvenirInstruction> ProcessPasswordDestroyer(ModuleData module)
    {
        var comp = GetComponent(module, "passwordDestroyer");

        var fldTwoFactorV2 = GetIntField(comp, "identityDigit");        // 2FAST value

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.PasswordDestroyerTwoFactorV2, module, correctAnswers: new[] { fldTwoFactorV2.Get(100100, 999999).ToString() }));
    }
}