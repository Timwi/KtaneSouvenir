using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SExoplanets
{
    [SouvenirQuestion("What was the starting target planet in {0}?", TwoColumns4Answers, "outer", "middle", "inner", "none", TranslateAnswers = true)]
    StartingTargetPlanet,

    [SouvenirQuestion("What was the starting target digit in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    StartingTargetDigit,

    [SouvenirQuestion("What was the final target planet in {0}?", TwoColumns4Answers, "outer", "middle", "inner", "none", TranslateAnswers = true)]
    TargetPlanet,

    [SouvenirQuestion("What was the final target digit in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    TargetDigit
}

public partial class SouvenirModule
{
    [SouvenirHandler("exoplanets", "Exoplanets", typeof(SExoplanets), "Brawlboxgaming")]
    private IEnumerator<SouvenirInstruction> ProcessExoplanets(ModuleData module)
    {
        var comp = GetComponent(module, "exoplanets");
        yield return WaitForSolve;

        var targetPlanet = GetIntField(comp, "targetPlanet").Get(0, 2);
        var targetDigit = GetIntField(comp, "targetDigit").Get(0, 9);

        var startingTargetPlanet = GetIntField(comp, "startingTargetPlanet").Get(0, 2);
        var startingTargetDigit = GetIntField(comp, "startingTargetDigit").Get(0, 9);

        var positionNames = GetStaticField<string[]>(comp.GetType(), "positionNames").Get(validator: arr => arr.Length != 3 ? "expected length 3" : null);

        yield return question(SExoplanets.StartingTargetPlanet).Answers(positionNames[startingTargetPlanet]);
        yield return question(SExoplanets.StartingTargetDigit).Answers(startingTargetDigit.ToString());
        yield return question(SExoplanets.TargetPlanet).Answers(positionNames[targetPlanet]);
        yield return question(SExoplanets.TargetDigit).Answers(targetDigit.ToString());
    }
}