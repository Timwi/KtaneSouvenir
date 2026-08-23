using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRGBSequences
{
    [Question("What color was the LED at index {1} in {0}?", ThreeColumns6Answers, "Black", "Red", "Green", "Blue", "Magenta", "Cyan", "Yellow", "White", TranslateAnswers = true, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [Handler("RGBSequences", "RGB Sequences", typeof(SRGBSequences), "Hawker")]
    [ManualQuestion("What were the colors of each button?")]
    private IEnumerator<SouvenirInstruction> ProcessRGBSequences(ModuleData module)
    {
        var comp = GetComponent(module, "RGBSequences");
        yield return WaitForSolve;

        var colorDic = new Dictionary<char, string> { ['K'] = "Black", ['R'] = "Red", ['G'] = "Green", ['B'] = "Blue", ['C'] = "Cyan", ['M'] = "Magenta", ['Y'] = "Yellow", ['W'] = "White" };
        var displayStr = GetField<string>(comp, "StringFour").Get(val => val.Length != 10 ? "expected length of 10" : val.Any(ch => !colorDic.ContainsKey(ch)) ? $"expected characters {colorDic.Keys.JoinString()}" : null);

        for (var i = 0; i < 10; i++)
            yield return question(SRGBSequences.Display, args: [i.ToString()]).Answers(colorDic[displayStr[i]]);
    }
}
