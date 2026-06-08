using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SExoplanets
{
    [Question("Which direction was the {1} planet orbiting the star in {0}?", OneColumn2Answers, "clockwise", "counterclockwise", Arguments = ["inner", "middle", "outer"], ArgumentGroupSize = 1, TranslateArguments = [true], TranslateAnswers = true)]
    PlanetDirection
}

public partial class SouvenirModule
{
    [Handler("exoplanets", "Exoplanets", typeof(SExoplanets), "Espik")]
    [ManualQuestion("Which directions were the planets orbiting the star?")]
    private IEnumerator<SouvenirInstruction> ProcessExoplanets(ModuleData module)
    {
        var comp = GetComponent(module, "exoplanets");
        yield return WaitForSolve;

        var planetDirections = GetArrayField<bool>(comp, "planetsCcw").Get(expectedLength: 3);

        yield return question(SExoplanets.PlanetDirection, args: ["inner"]).Answers(planetDirections[0] ? "counterclockwise" : "clockwise");
        yield return question(SExoplanets.PlanetDirection, args: ["middle"]).Answers(planetDirections[1] ? "counterclockwise" : "clockwise");
        yield return question(SExoplanets.PlanetDirection, args: ["outer"]).Answers(planetDirections[2] ? "counterclockwise" : "clockwise");
    }
}
