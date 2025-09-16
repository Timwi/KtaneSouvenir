using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGoofysGame
{
    [SouvenirQuestion("What number was flashed by the {1} LED in {0}?", ThreeColumns6Answers, Arguments = ["left", "right", "center"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(0, 9)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("goofysgame", "Goofy’s Game", typeof(SGoofysGame), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessGoofysGame(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "GoofysGameScript");
        var nums = GetListField<int>(comp, "lightCodes").Get(expectedLength: 3, validator: v => v is < 0 or > 9 ? "Out of range [0, 9]" : null);
        var directions = new[] { "left", "center", "right" };

        for (var i = 0; i < nums.Count; i++)
            yield return question(SGoofysGame.Number, args: [directions[i]]).Answers(nums[i].ToString());
    }
}
