using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNavinums
{
    [SouvenirQuestion("What was the {1} directional button pressed in {0}?", TwoColumns4Answers, "up", "left", "right", "down", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DirectionalButtons,

    [SouvenirQuestion("What was the initial middle digit in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    MiddleDigit
}

public partial class SouvenirModule
{
    [SouvenirHandler("navinums", "Navinums", typeof(SNavinums), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessNavinums(ModuleData module)
    {
        var comp = GetComponent(module, "navinumsScript");
        var fldStage = GetIntField(comp, "stage");
        var fldDirections = GetListField<int>(comp, "directions");
        var lookUp = GetArrayField<int[]>(comp, "lookUp").Get(expectedLength: 9, validator: ar => ar.Length != 8 ? "expected length 8" : null);
        var directionsSorted = GetListField<int>(comp, "directionsSorted").Get(expectedLength: 4);
        var centerDigit = GetIntField(comp, "center").Get(min: 1, max: 9);

        var curStage = -1;
        var answers = new int[8];
        while (true)
        {
            yield return null;
            var newStage = fldStage.Get();
            if (newStage != curStage)
            {
                if (newStage == 8)
                    break;
                var newDirections = fldDirections.Get();
                if (newDirections.Count != 4)
                    throw new AbandonModuleException($"‘directions’ has unexpected length {newDirections.Count} (expected 4).");

                answers[newStage] = newDirections.IndexOf(directionsSorted[lookUp[centerDigit - 1][newStage] - 1]);
                if (answers[newStage] == -1)
                    throw new AbandonModuleException($"‘directions’ ({newDirections.JoinString(", ")}) does not contain the value from ‘directionsSorted’ ({directionsSorted[lookUp[centerDigit - 1][newStage] - 1]}).");
                curStage = newStage;
            }
        }

        yield return WaitForSolve;

        var directionNames = new[] { "up", "left", "right", "down" };

        var qs = new List<QandA>();
        for (var stage = 0; stage < 8; stage++)
            qs.Add(makeQuestion(Question.NavinumsDirectionalButtons, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { directionNames[answers[stage]] }));
        qs.Add(makeQuestion(Question.NavinumsMiddleDigit, module, correctAnswers: new[] { centerDigit.ToString() }));
        addQuestions(module, qs);
    }
}