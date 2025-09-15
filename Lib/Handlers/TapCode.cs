using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STapCode
{
    [SouvenirQuestion("What was the received word in {0}?", TwoColumns4Answers, ExampleAnswers = ["child", "style", "shake", "alive", "axion", "wreck", "cause", "pupil", "cheat", "watch"])]
    ReceivedWord
}

public partial class SouvenirModule
{
    [SouvenirHandler("tapCode", "Tap Code", typeof(STapCode), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessTapCode(ModuleData module)
    {
        var comp = GetComponent(module, "TapCodeScript");
        yield return WaitForSolve;

        var words = GetStaticField<string[]>(comp.GetType(), "_wordList").Get();
        var chosenWord = GetField<string>(comp, "_chosenWord").Get(str => !words.Contains(str) ? $"word is not in list: {words.JoinString(", ")}" : null);
        var w = words.Select(i => i.Substring(0, 1).ToUpperInvariant() + i.Substring(1).ToLowerInvariant()).ToArray();
        var cw = chosenWord.Substring(0, 1).ToUpperInvariant() + chosenWord.Substring(1).ToLowerInvariant();
        addQuestion(module, Question.TapCodeReceivedWord, correctAnswers: new[] { cw }, preferredWrongAnswers: w);
    }
}