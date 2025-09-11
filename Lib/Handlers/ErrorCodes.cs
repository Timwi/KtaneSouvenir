using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SErrorCodes
{
    [SouvenirQuestion("What was the active error code in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 101, 1, "X2")]
    ActiveError
}

public partial class SouvenirModule
{
    [SouvenirHandler("errorCodes", "Error Codes", typeof(SErrorCodes), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessErrorCodes(ModuleData module)
    {
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var comp = GetComponent(module, "ErrorCodes");
        var errorTextDisplay = GetField<TextMesh>(comp, "errorText", isPublic: true).Get();
        var fixTextDisplay = GetField<TextMesh>(comp, "fixText", isPublic: true).Get();
        var errorPrefix = GetStaticField<string>(comp.GetType(), "ERROR_PREFIX").Get();
        var fixPrefix = GetStaticField<string>(comp.GetType(), "FIX_PREFIX").Get();
        var displayCodes = errorTextDisplay.text.Replace(errorPrefix + " ", "").Split(' ');
        var serialNumberHasVowel = Bomb.GetSerialNumber().Any(c => "AEIOU".Contains(c));
        var batteryEven = Bomb.GetBatteryCount() % 2 == 0;

        var conditionTable = new[] { serialNumberHasVowel && batteryEven,
                                        serialNumberHasVowel && !batteryEven,
                                        !serialNumberHasVowel && batteryEven,
                                        !serialNumberHasVowel && !batteryEven };
        var activeErrorIndex = Array.IndexOf(conditionTable, true);

        yield return WaitForSolve;

        addQuestion(module, Question.ErrorCodesActiveError,
            correctAnswers: new[] { displayCodes[activeErrorIndex] },
            preferredWrongAnswers: displayCodes);

        // Change the displays to blank
        errorTextDisplay.text = errorPrefix;
        fixTextDisplay.text = fixPrefix;
    }
}