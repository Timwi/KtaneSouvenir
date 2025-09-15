using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRGBSequences
{
    [SouvenirQuestion("What was the color of the {1} LED in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Cyan", "Yellow", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("RGBSequences", "RGB Sequences", typeof(SRGBSequences), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessRGBSequences(ModuleData module)
    {
        var comp = GetComponent(module, "RGBSequences");
        yield return WaitForSolve;

        var colorDic = new Dictionary<char, string> { ['R'] = "Red", ['G'] = "Green", ['B'] = "Blue", ['C'] = "Cyan", ['M'] = "Magenta", ['Y'] = "Yellow", ['W'] = "White" };
        var displayStr = GetField<string>(comp, "StringFour").Get(val => val.Length != 10 ? "expected length of 10" : val.Any(ch => !colorDic.ContainsKey(ch)) ? $"expected characters {colorDic.Keys.JoinString()}" : null);

        addQuestions(module, Enumerable.Range(0, 10).Select(i =>
            makeQuestion(SRGBSequences.Display, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colorDic[displayStr[i]] })));
    }
}