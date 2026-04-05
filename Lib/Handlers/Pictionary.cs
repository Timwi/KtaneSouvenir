using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPictionary
{
    [SouvenirQuestion("What were the colors of the pixels in the {1} quadrant in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "PictionarySprites", Arguments = ["top left", "top right", "bottom left", "bottom right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("pictionaryModule", "Pictionary", typeof(SPictionary), "Quinn Wuest")]
    [SouvenirManualQuestion("What was the code?")]
    private IEnumerator<SouvenirInstruction> ProcessPictionary(ModuleData module)
    {
        var comp = GetComponent(module, "pictionaryModuleScript");

        yield return WaitForSolve;

        var dict = new Dictionary<(int, int), int>
        {
            [(0, 0)] = 0b0101,
            [(0, 1)] = 0b1111,
            [(0, 2)] = 0b0001,
            [(0, 3)] = 0b1010,
            [(0, 4)] = 0b0111,
            [(0, 5)] = 0b1101,
            [(0, 7)] = 0b0001,
            [(0, 9)] = 0b0000,
            [(1, 0)] = 0b1011,
            [(1, 1)] = 0b0101,
            [(1, 2)] = 0b1111,
            [(1, 3)] = 0b1110,
            [(1, 4)] = 0b0010,
            [(1, 5)] = 0b1101,
            [(1, 6)] = 0b0110,
            [(1, 8)] = 0b0000,
            [(2, 0)] = 0b0110,
            [(2, 1)] = 0b1011,
            [(2, 2)] = 0b0011,
            [(2, 3)] = 0b1111,
            [(2, 4)] = 0b1001,
            [(2, 5)] = 0b1101,
            [(2, 6)] = 0b0100,
            [(2, 7)] = 0b0000,
            [(3, 0)] = 0b0011,
            [(3, 1)] = 0b1001,
            [(3, 2)] = 0b0110,
            [(3, 3)] = 0b0111,
            [(3, 4)] = 0b1111,
            [(3, 5)] = 0b1101,
            [(3, 6)] = 0b0000,
            [(3, 8)] = 0b1000
        };

        var code = GetField<string>(comp, "code").Get(c => c.Length != 4 || c.Any(ch => !char.IsDigit(ch)) ? "expected a sequence of four digits" : null);
        for (int ix = 0; ix < code.Length; ix++)
        {
            int c = code[ix] - '0';
            var spr = dict[(ix, c)];
            yield return question(SPictionary.Colors, args: [new[] { "top left", "top right", "bottom left", "bottom right" }[ix]]).Answers(PictionarySprites[spr], preferredWrong: PictionarySprites);
        }
    }
}