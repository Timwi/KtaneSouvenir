using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSkewers
{
    [SouvenirQuestion("What color was this gem in {0}?", ThreeColumns6Answers, "Black", "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", "White", UsesQuestionSprite = true, TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("Skewers", "Skewers", typeof(SSkewers), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSkewers(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "Skewers");

        var color = GetListField<int>(comp, "GemColors").Get(expectedLength: 16, validator: v => v is < 0 or > 7 ? "Out of range [0, 7]" : null);
        for (var i = 0; i < color.Length; i++)
            yield return question(SSkewers.Color, questionSprite: Sprites.GenerateGridSprite(4, 4, i)).Answers(SSkewers.Color.GetAnswers()[color[i]]);
    }
}