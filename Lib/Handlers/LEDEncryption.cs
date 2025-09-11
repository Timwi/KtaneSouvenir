using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLEDEncryption
{
    [SouvenirQuestion("What was the correct letter you pressed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    PressedLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("LEDEnc", "LED Encryption", typeof(SLEDEncryption), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessLEDEncryption(ModuleData module)
    {
        var comp = GetComponent(module, "LEDEncryption");

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var buttons = GetArrayField<KMSelectable>(comp, "buttons", true).Get(expectedLength: 4);
        var buttonLabels = buttons.Select(btn => btn.GetComponentInChildren<TextMesh>()).ToArray();
        if (buttonLabels.Any(x => x == null))
            throw new AbandonModuleException("At least one of the buttons’ TextMesh is null.");

        var multipliers = GetArrayField<int>(comp, "layerMultipliers").Get(minLength: 2, maxLength: 5, validator: m => m is < 2 or > 7 ? "expected range 2–7" : null);
        var numStages = multipliers.Length;
        var pressedLetters = new string[numStages];
        var wrongLetters = new HashSet<string>();
        var fldStage = GetIntField(comp, "layer");

        void reassignButton(KMSelectable btn, TextMesh lbl)
        {
            var prevInteract = btn.OnInteract;
            var stage = fldStage.Get();
            btn.OnInteract = delegate
            {
                var label = lbl.text;
                var ret = prevInteract();
                if (fldStage.Get() > stage)
                    pressedLetters[stage] = label;
                return ret;
            };
        }

        while (fldStage.Get() < numStages)
        {
            foreach (var lbl in buttonLabels)
                wrongLetters.Add(lbl.text);

            // LED Encryption re-hooks the buttons at every press, so we have to re-hook it at each stage as well
            for (var i = 0; i < 4; i++)
                reassignButton(buttons[i], buttonLabels[i]);

            var stage = fldStage.Get();
            while (fldStage.Get() == stage)
                yield return null;
        }

        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, pressedLetters.Length - 1)
            .Where(i => pressedLetters[i] != null)
            .Select(stage => makeQuestion(Question.LEDEncryptionPressedLetters, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { pressedLetters[stage] }, preferredWrongAnswers: wrongLetters.ToArray())));
    }
}