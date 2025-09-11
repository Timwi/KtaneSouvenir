using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SQuaver
{
    [SouvenirQuestion("What was the {1} sequence’s answer in {0}?", OneColumn4Answers, ExampleAnswers = ["4", "10", "87", "320", "3, 3, 2, 3", "87, 85, 82, 84"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Arrows
}

public partial class SouvenirModule
{
    [SouvenirHandler("Quaver", "Quaver", typeof(SQuaver), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessQuaver(ModuleData module)
    {
        var comp = GetComponent(module, "QuaverScript");
        var init = GetField<object>(comp, "init").Get();
        var fldCorrectValues = GetListField<int[]>(init, "correctValues");

        yield return WaitForSolve;

        var correctValues = fldCorrectValues.Get(minLength: 1, maxLength: 20, validator: arr => arr.Length is not 1 and not 4 ? "expected array of length 1 or 4" : null);
        var qs = new List<QandA>();

        for (var i = 0; i < correctValues.Count; i++)
        {
            var preferredWrongAnswers = new HashSet<string>();
            while (preferredWrongAnswers.Count < 6)
                preferredWrongAnswers.Add(correctValues[i].Select(x => Math.Max(x + Rnd.Range(-4, 5), 1)).JoinString(", "));
            qs.Add(makeQuestion(Question.QuaverArrows, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { correctValues[i].JoinString(", ") }, preferredWrongAnswers: preferredWrongAnswers.ToArray()));
        }
        addQuestions(module, qs);
    }
}