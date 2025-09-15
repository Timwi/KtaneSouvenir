using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLetterMath
{
    [SouvenirQuestion("What was the letter on the {1} display in {0}?", ThreeColumns6Answers, Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings("A-Z")]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("letterMath", "Letter Math", typeof(SLetterMath), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessLetterMath(ModuleData module)
    {
        var comp = GetComponent(module, "LetterMathModule");

        var characters = GetArrayField<int>(comp, "characters").Get();
        var letters = Enumerable.Range(0, 2).ToArray().Select(i => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(characters[i], 1)).ToArray();

        yield return WaitForSolve;

        var wrongLetters = Enumerable.Range(0, 26).ToArray().Select(i => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(i, 1)).ToArray();

        yield return question(SLetterMath.Display, args: ["left"]).Answers(letters[0], preferredWrong: wrongLetters);
        yield return question(SLetterMath.Display, args: ["right"]).Answers(letters[1], preferredWrong: wrongLetters);
    }
}