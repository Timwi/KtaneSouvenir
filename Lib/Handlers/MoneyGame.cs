using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMoneyGame
{
    [SouvenirQuestion("What were the first and second words in the {1} phrase in {0}?", TwoColumns4Answers, "she sells", "she shells", "sea shells", "sea sells", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Question1,

    [SouvenirQuestion("What were the third and fourth words in the {1} phrase in {0}?", TwoColumns4Answers, "sea shells", "she shells", "sea sells", "she sells", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Question2,

    [SouvenirQuestion("What was the end of the {1} phrase in {0}?", TwoColumns4Answers, "sea shore", "she sore", "she sure", "seesaw", "seizure", "shell sea", "steep store", "sheer sort", "speed spore", "sieve horn", "steel sword", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Question3
}

public partial class SouvenirModule
{
    [SouvenirHandler("MoneyGame", "Money Game", typeof(SMoneyGame), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessMoneyGame(ModuleData module)
    {
        var comp = GetComponent(module, "MoneyGame");
        var fldAns = GetArrayField<int>(comp, "Answer");
        var fldStage = GetIntField(comp, "Stage");
        var fldDisplay = GetField<TextMesh>(comp, "DisplayText", isPublic: true);

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var data = new int[3][];

        while (true)
        {
            while (fldDisplay.Get().text == " ")
            {
                yield return new WaitForSeconds(.1f);
                if (module.IsSolved)
                    goto solved;
            }

            var stage = fldStage.Get(min: 0, max: 2);
            data[stage] = fldAns.Get(expectedLength: 3).ToArray(); // Make a copy so the module doesn't mutate it
            if (data[stage][0] is < 0 or > 3 || data[stage][1] is < 0 or > 3 || data[stage][2] is < 0 or > 10)
                throw new AbandonModuleException($"Answer data out of range. Got: {data[stage][0]}, {data[stage][1]}, {data[stage][2]} Expected: 0-3, 0-3, 0-10");

            while (fldDisplay.Get().text != " ")
            {
                yield return new WaitForSeconds(.1f);
                if (module.IsSolved)
                    goto solved;
            }
        }

        solved:
        yield return WaitForSolve;

        var possibleAnswers = new[] {
            new[] { "she sells", "she shells", "sea shells", "sea sells" },
            new[] { "sea shells", "she shells", "sea sells", "she sells" },
            new[] { "sea shore", "she sore", "she sure", "seesaw", "seizure", "shell sea", "steep store", "sheer sort", "seed spore", "sieve horn", "steel sword" }
        };

        addQuestions(module, Enumerable.Range(0, 3).SelectMany(i =>
            new[] { Question.MoneyGame1, Question.MoneyGame2, Question.MoneyGame3 }.Select((q, qi) =>
                makeQuestion(q, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { possibleAnswers[qi][data[i][qi]] })
            )));
    }
}
