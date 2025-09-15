using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAlphabeticalRuling
{
    [SouvenirQuestion("What was the letter displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(1, 'A', 'Z')]
    Letter,

    [SouvenirQuestion("What was the number displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 9)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("alphabeticalRuling", "Alphabetical Ruling", typeof(SAlphabeticalRuling), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessAlphabeticalRuling(ModuleData module)
    {
        var comp = GetComponent(module, "AlphabeticalRuling");
        var fldStage = GetIntField(comp, "currentStage");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var letterDisplay = GetField<TextMesh>(comp, "LetterDisplay", isPublic: true).Get();
        var numberDisplays = GetArrayField<TextMesh>(comp, "NumberDisplays", isPublic: true).Get(expectedLength: 2);
        var curStage = 0;
        var letters = new char[3];
        var numbers = new int[3];
        while (module.Unsolved)
        {
            var newStage = fldStage.Get();
            if (newStage != curStage)
            {
                if (letterDisplay.text.Length != 1 || letterDisplay.text[0] < 'A' || letterDisplay.text[0] > 'Z')
                    throw new AbandonModuleException($"‘LetterDisplay’ shows {letterDisplay.text} (expected single letter A–Z).");
                letters[newStage - 1] = letterDisplay.text[0];
                if (!int.TryParse(numberDisplays[0].text, out var number) || number < 1 || number > 9)
                    throw new AbandonModuleException($"‘NumberDisplay[0]’ shows {numberDisplays[0].text} (expected integer 1–9).");
                numbers[newStage - 1] = number;
                curStage = newStage;
            }

            yield return null;
        }

        if (letters.Any(l => l is < 'A' or > 'Z') || numbers.Any(n => n is < 1 or > 9))
            throw new AbandonModuleException($"The captured letters/numbers are unexpected (letters: [{letters.JoinString(", ")}], numbers: [{numbers.JoinString(", ")}]).");
        for (var ix = 0; ix < letters.Length; ix++)
            yield return question(SAlphabeticalRuling.Letter, args: [Ordinal(ix + 1)]).Answers(letters[ix].ToString());
        for (var ix = 0; ix < numbers.Length; ix++)
            yield return question(SAlphabeticalRuling.Number, args: [Ordinal(ix + 1)]).Answers(numbers[ix].ToString());
    }
}