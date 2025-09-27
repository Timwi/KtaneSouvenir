using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SHolographicMemory
{
    [SouvenirQuestion("Which side did this symbol appear in {0}?", TwoColumns2Answers, "Light", "Dark", UsesQuestionSprite = true)]
    InitialGrid,
    [SouvenirQuestion("Which symbol was selected in the {1} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "HolographicMemorySprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    StageSelected,
}

public partial class SouvenirModule
{
    [SouvenirHandler("holographicMemory", "Holographic Memory", typeof(SHolographicMemory), "KiloBites")]

    private IEnumerator<SouvenirInstruction> ProcessHolographicMemory(ModuleData module)
    {
        var comp = GetComponent(module, "HoloScript");
        var fldSymbolSelected = GetArrayField<int>(comp, "symbselect").Get(expectedLength: 32);
        yield return WaitForSolve;

        var fldAnswers = GetField<int[][]>(comp, "ans");

        var getAnswerSprites = fldAnswers.Get().Select(x => HolographicMemorySprites[fldSymbolSelected[x[2] * 16 + 4 * x[0] + x[1]]]).ToArray();

        for (int side = 0; side < 2; side++)
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    yield return question(SHolographicMemory.InitialGrid, questionSprite: HolographicMemorySprites[fldSymbolSelected[side * 16 + 4 * row + col]]).Answers(side == 0 ? "Light" : "Dark");


        for (int stage = 0; stage < 5; stage++)
            yield return question(SHolographicMemory.StageSelected, args: [Ordinal(stage + 1)]).Answers(getAnswerSprites[stage], getAnswerSprites);
    }
}
