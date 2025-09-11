using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBombDiffusal
{
    [SouvenirQuestion("What was the license number in {0}?", TwoColumns4Answers, ExampleAnswers = ["A4BIK5", "HI391D", "ZX98O1", "12K9PL"])]
    LicenseNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("bombDiffusal", "Bomb Diffusal", typeof(SBombDiffusal), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessBombDiffusal(ModuleData module)
    {
        var comp = GetComponent(module, "bombDiffusalScript");

        yield return WaitForSolve;

        var fldLicenseNumber = GetField<string>(comp, "licenseNo");
        var mthGenerateRandomLicenseNumber = GetMethod(comp, "GenerateLicenseNo", 0);
        var answers = new HashSet<string>();
        var correctAnswer = fldLicenseNumber.Get(x => x.Length != 6 || x.Any(c => !char.IsLetterOrDigit(c)) ? "expected 6 alphanumeric characters" : null);

        while (answers.Count < 4)
        {
            mthGenerateRandomLicenseNumber.Invoke();
            answers.Add(fldLicenseNumber.Get(x => x.Length != 6 || x.Any(c => !char.IsLetterOrDigit(c)) ? "expected 6 alphanumeric characters" : null));
        }
        fldLicenseNumber.Set(correctAnswer); // Set the license number back to what it was to allow other Souvenir modules to access it.
        addQuestion(module, Question.BombDiffusalLicenseNumber, correctAnswers: new[] { correctAnswer }, preferredWrongAnswers: answers.ToArray());
    }
}