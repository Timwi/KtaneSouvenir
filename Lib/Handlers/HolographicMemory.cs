using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SHolographicMemory
{
    [SouvenirQuestion("Which side did this symbol appear in {0}?", TwoColumns2Answers, "Light", "Dark", UsesQuestionSprite = true, TranslateAnswers = true)]
    InitialGrid
}

public partial class SouvenirModule
{
    [SouvenirHandler("holographicMemory", "Holographic Memory", typeof(SHolographicMemory), "KiloBites")]
    private IEnumerator<SouvenirInstruction> ProcessHolographicMemory(ModuleData module)
    {
        var comp = GetComponent(module, "HoloScript");
        var symbolSelected = GetArrayField<int>(comp, "symbselect").Get(expectedLength: 32);
        var sprites = GetArrayField<Material>(comp, "symbols", isPublic: true).Get(expectedLength: 56)
            .Select(mat => ((Texture2D) mat.mainTexture).ToSprite()).ToArray();

        yield return WaitForSolve;

        var answerSprites = GetField<int[][]>(comp, "ans").Get().Select(ans => sprites[symbolSelected[16 * ans[2] + 4 * ans[0] + ans[1]]]).ToArray();

        for (var side = 0; side < 2; side++)
            for (var row = 0; row < 4; row++)
                for (var col = 0; col < 4; col++)
                    yield return question(SHolographicMemory.InitialGrid, questionSprite: sprites[symbolSelected[16 * side + 4 * row + col]]).Answers(side == 0 ? "Light" : "Dark");
    }
}
