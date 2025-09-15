using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SEncryptedDice
{
    [SouvenirQuestion("Which of these numbers appeared on a die in the {1} stage of {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    Question
}

public partial class SouvenirModule
{
    [SouvenirHandler("EncryptedDice", "Encrypted Dice", typeof(SEncryptedDice), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessEncryptedDice(ModuleData module)
    {
        var comp = GetComponent(module, "EncrypedDice");

        var fldSolved = GetField<bool>(comp, "solved");
        var fldCanRoll = GetField<bool>(comp, "canRoll");
        var fldRolledValues = GetArrayField<int>(comp, "rolledValues");
        var fldStageNumber = GetIntField(comp, "stagesCompleted");

        var stage = 0;
        var rolledValues = new int[3][];

        module.Module.OnStrike += () => { stage--; return false; };
        module.Module.OnActivate += () => { stage--; };

        while (!fldSolved.Get())
        {
            if (fldStageNumber.Get(min: stage, max: stage + 1) > stage)
            {
                if (stage > 2)
                    throw new AbandonModuleException("Expected 3 stages but we have now exceeded this amount");
                while (!fldCanRoll.Get())
                    yield return null; // Do not wait .1 seconds so we are absolutely sure we get the right stage.
                stage++;
                rolledValues[stage] = fldRolledValues.Get(expectedLength: 3, validator: val => val is < 1 or > 6 ? "expected range 1-6" : null).ToArray();
            }
            yield return new WaitForSeconds(.1f); // Roll animation is much longer than .1 seconds anyway.
        }
        addQuestions(module, rolledValues.Select((vals, ix) => makeQuestion(Question.EncryptedDice, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: vals.Select(val => (val).ToString()).ToArray())));
    }
}