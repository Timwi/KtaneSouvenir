using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMazematics
{
    [SouvenirQuestion("Which was the {1} value in {0}?", ThreeColumns6Answers, ExampleAnswers = ["30", "42", "51"], Arguments = ["initial", "goal"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Value
}

public partial class SouvenirModule
{
    [SouvenirHandler("mazematics", "Mazematics", typeof(SMazematics), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessMazematics(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "Mazematics");
        var startVal = GetIntField(comp, "startValue").Get(17, 49).ToString();
        var goalVal = GetIntField(comp, "goalValue").Get(0, 49).ToString();

        var possibleStartVals = Enumerable.Range(17, 33).Select(x => x.ToString()).ToArray();
        var possibleGoalVals = Enumerable.Range(0, 50).Select(x => x.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.MazematicsValue, module, formatArgs: new[] { "initial" }, correctAnswers: new[] { startVal }, preferredWrongAnswers: possibleStartVals),
            makeQuestion(Question.MazematicsValue, module, formatArgs: new[] { "goal" }, correctAnswers: new[] { goalVal }, preferredWrongAnswers: possibleGoalVals));
    }
}