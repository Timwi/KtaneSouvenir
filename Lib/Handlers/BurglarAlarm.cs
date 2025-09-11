using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBurglarAlarm
{
    [SouvenirQuestion("What was the {1} displayed digit in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("burglarAlarm", "Burglar Alarm", typeof(SBurglarAlarm), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessBurglarAlarm(ModuleData module)
    {
        var comp = GetComponent(module, "BurglarAlarmScript");
        yield return WaitForSolve;

        var displayText = GetField<TextMesh>(comp, "DisplayText", isPublic: true).Get();
        displayText.text = "";

        var moduleNumber = GetArrayField<int>(comp, "moduleNumber").Get(expectedLength: 8, validator: mn => mn is < 0 or > 9 ? "expected 0–9" : null);
        addQuestions(module, moduleNumber.Select((mn, ix) => makeQuestion(Question.BurglarAlarmDigits, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { mn.ToString() }, preferredWrongAnswers: moduleNumber.Select(n => n.ToString()).ToArray())));
    }
}