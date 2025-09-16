using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMazematics
{
    [SouvenirQuestion("Which was the {1} value in {0}?", ThreeColumns6Answers, ExampleAnswers = ["30", "42", "51"], Arguments = ["initial", "goal"], ArgumentGroupSize = 1, TranslateArguments = [true])]
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

        yield return question(SMazematics.Value, args: ["initial"]).Answers(startVal, preferredWrong: possibleStartVals);
        yield return question(SMazematics.Value, args: ["goal"]).Answers(goalVal, preferredWrong: possibleGoalVals);
    }
}