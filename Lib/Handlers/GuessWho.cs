using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGuessWho
{
    [SouvenirQuestion("Did {1} flash “YES” in {0}?", TwoColumns2Answers, "Yes", "No", TranslateAnswers = true, Arguments = ["Red", "Orange", "Yellow", "Green", "Blue", "Violet", "Cyan", "Pink"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("GuessWho", "Guess Who?", typeof(SGuessWho), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessGuessWho(ModuleData module)
    {
        var comp = GetComponent(module, "GuessWhoScript");
        var bases = GetField<int[]>(comp, "Bases").Get();

        var colors = new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Violet", "Cyan", "Pink" };

        yield return WaitForSolve;
        for (var i = 0; i < colors.Length; i++)
            yield return question(SGuessWho.Colors, args: [colors[i]]).Answers(bases[i] == 1 ? "Yes" : "No");
    }
}
