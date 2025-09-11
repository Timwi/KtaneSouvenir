using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHinges
{
    [SouvenirQuestion("Which of these hinges was initially {1} {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "HingesSprites", Arguments = ["present on", "absent from"], ArgumentGroupSize = 1, TranslateFormatArgs = [true], TranslatableStrings = ["the Hinges where this hinge was initally present", "the Hinges where this hinge was initally absent"])]
    InitialHinges
}

public partial class SouvenirModule
{
    [SouvenirHandler("hinges", "Hinges", typeof(SHinges), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessHinges(ModuleData module)
    {
        var comp = GetComponent(module, "Hinges");
        var initialHingesStatus = GetArrayField<int>(comp, "hingeStatus").Get(expectedLength: 8, validator: i => i is not 0 and not 1 ? "expected value 0 or 1" : null).ToArray();
        _hingesInitialStates.Add(initialHingesStatus);

        yield return WaitForSolve;

        int? disambiguator = null;
        string format = null;
        Sprite formatSprite = null;
        if (_moduleCounts["hinges"] > 1 && Rnd.Range(0, 3) != 0)
        {
            var presentCount = initialHingesStatus.Count(x => x != 0);
            var disambiguators = Enumerable.Range(0, 8)
                .Where(d =>
                    (presentCount == 4 || presentCount < 4 && initialHingesStatus[d] != 0 || presentCount > 4 && initialHingesStatus[d] == 0) &&
                    _hingesInitialStates.Count(s => s[d] == initialHingesStatus[d]) == 1).ToArray();
            if (disambiguators.Any())
            {
                disambiguator = disambiguators.PickRandom();
                format = initialHingesStatus[disambiguator.Value] == 1 ? "the Hinges where this hinge was initally present" : "the Hinges where this hinge was initally absent";
                formatSprite = HingesSprites[disambiguator.Value];
            }
        }

        var presentHinges = new List<Sprite>();
        var absentHinges = new List<Sprite>();
        for (var pos = 0; pos < 8; pos++)
            if (pos != disambiguator)
                (initialHingesStatus[pos] == 1 ? presentHinges : absentHinges).Add(HingesSprites[pos]);
        var allHinges = presentHinges.Concat(absentHinges).ToArray();

        var qs = new List<QandA>();
        // There are eight hinges in total, so at least one question will generate.
        if (presentHinges.Count is <= 4 and >= 1)
            qs.Add(makeQuestion(Question.HingesInitialHinges, module, formattedModuleName: format, questionSprite: formatSprite, formatArgs: new[] { "present on" }, correctAnswers: presentHinges.ToArray(), allAnswers: allHinges));
        if (absentHinges.Count is <= 4 and >= 1)
            qs.Add(makeQuestion(Question.HingesInitialHinges, module, formattedModuleName: format, questionSprite: formatSprite, formatArgs: new[] { "absent from" }, correctAnswers: absentHinges.ToArray(), allAnswers: allHinges));
        addQuestions(module, qs);
    }
}