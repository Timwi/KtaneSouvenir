using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSlowMath
{
    [Question("What was the last triplet of letters in {0}?", ThreeColumns6Answers, ExampleAnswers = ["ABC", "DEG", "KNP", "STX", "ZAB", "CDE", "GKN", "PST", "XZA", "BCD"])]
    [AnswerGenerator.Strings(3, "ABCDEGKNPSTXZ")]
    LastLetters
}

public partial class SouvenirModule
{
    [Handler("SlowMathModule", "Slow Math", typeof(SSlowMath), "Quinn Wuest")]
    [ManualQuestion("What was the last triplet of letters?")]
    private IEnumerator<SouvenirInstruction> ProcessSlowMath(ModuleData module)
    {
        var comp = GetComponent(module, "SlowMathScript");
        yield return WaitForSolve;

        var ogLetters = GetListField<string>(comp, "_chosenLetters").Get(minLength: 3, maxLength: 5);
        yield return question(SSlowMath.LastLetters).Answers(ogLetters.Last());
    }
}