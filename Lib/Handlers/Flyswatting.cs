using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFlyswatting
{
    [SouvenirQuestion("Which fly was present, but not in the solution in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    Unpressed
}

public partial class SouvenirModule
{
    [SouvenirHandler("flyswatting", "Flyswatting", typeof(SFlyswatting), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessFlyswatting(ModuleData module)
    {
        var comp = GetComponent(module, "flyswattingScript");

        var swatted = GetArrayField<int>(comp, "answers").Get(expectedLength: 5).Select(x => x == 1).ToArray();

        yield return WaitForSolve;

        var letters = GetArrayField<string>(comp, "chosens").Get(expectedLength: 5);
        var outsideLetters = letters.Where((_, pos) => !swatted[pos]).ToArray();
        if (outsideLetters.Length == 0)
            legitimatelyNoQuestion(module, "Every fly was part of the solution.");
        else
            addQuestion(module, Question.FlyswattingUnpressed, correctAnswers: outsideLetters);
    }
}