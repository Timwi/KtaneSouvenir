using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLEDEncryption
{
    [Question("Which of these letters was present in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    PresentLetters
}

public partial class SouvenirModule
{
    [Handler("LEDEnc", "LED Encryption", typeof(SLEDEncryption), "CaitSith2")]
    [ManualQuestion("Which letters were present at each stage")]
    private IEnumerator<SouvenirInstruction> ProcessLEDEncryption(ModuleData module)
    {
        var comp = GetComponent(module, "LEDEncryption");

        yield return WaitForActivate;

        var buttons = GetArrayField<KMSelectable>(comp, "buttons", true).Get(expectedLength: 4);
        var buttonLabels = buttons.Select(btn => btn.GetComponentInChildren<TextMesh>()).ToArray();
        if (buttonLabels.Any(x => x == null))
            throw new AbandonModuleException("At least one of the buttons’ TextMesh is null.");

        var multipliers = GetArrayField<int>(comp, "layerMultipliers").Get(minLength: 2, maxLength: 5, validator: m => m is < 2 or > 7 ? "expected range 2–7" : null);
        var numStages = multipliers.Length;

        var fldStage = GetIntField(comp, "layer");
        var fullLetters = new string[numStages][];

        while (fldStage.Get() < numStages)
        {
            var stage = fldStage.Get();
            fullLetters[stage] = new string[4];

            for (var i = 0; i < 4; i++)
                fullLetters[stage][i] = buttonLabels[i].text;
            
            while (fldStage.Get() == stage)
                yield return null;
        }

        yield return WaitForSolve;

        for (var stage = 0; stage < numStages - 1; stage++)
            yield return question(SLEDEncryption.PresentLetters, args: [Ordinal(stage + 1)]).Answers(fullLetters[stage]);
    }
}
