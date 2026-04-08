using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPictionary
{
    [Question("What were the colors of the pixels in the {1} quadrant in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "PictionarySprites", Arguments = ["top left", "top right", "bottom left", "bottom right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Colors
}

public partial class SouvenirModule
{
    [Handler("pictionaryModule", "Pictionary", typeof(SPictionary), "Timwi")]
    [ManualQuestion("What were the colors of the pixels?")]
    private IEnumerator<SouvenirInstruction> ProcessPictionary(ModuleData module)
    {
        var comp = GetComponent(module, "pictionaryModuleScript");

        yield return WaitForSolve;
        var quadrants = new[] { "TL", "TR", "BL", "BR" }.Select(q => GetIntField(comp, q).Get(min: 0, max: 7)).ToArray();
        const string cornerTLPatterns = "01101001011111010001111011110000";
        const string cornerTRPatterns = "10010110101111100010110111110000";
        const string cornerBLPatterns = "10010110001111100100101111110000";
        const string cornerBRPatterns = "01101001001111011000011111110000";
        var patterns = new[] { cornerTLPatterns, cornerTRPatterns, cornerBLPatterns, cornerBRPatterns };

        for (var quadrant = 0; quadrant < 4; quadrant++)
        {
            var pattern = patterns[quadrant].Substring(4 * quadrants[quadrant], 4);
            var spriteIx = (pattern[0] - '0') * 8 + (pattern[1] - '0') * 4 + (pattern[2] - '0') * 2 + (pattern[3] - '0');
            yield return question(SPictionary.Colors, args: [new[] { "top left", "top right", "bottom left", "bottom right" }[quadrant]]).Answers(PictionarySprites[spriteIx]);
        }
    }
}
