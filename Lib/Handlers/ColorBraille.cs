using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SColorBraille
{
    [SouvenirQuestion("What color was this dot in {0}?", ThreeColumns6Answers, "Black", "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", "White", TranslateAnswers = true, UsesQuestionSprite = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("ColorBrailleModule", "Color Braille", typeof(SColorBraille), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessColorBraille(ModuleData module)
    {
        var comp = GetComponent(module, "ColorBrailleModule");
        yield return WaitForSolve;

        var colorIxs = GetArrayField<int>(comp, "_colorIxs").Get(expectedLength: 5 * 6);
        var colorNames = Question.ColorBrailleColor.GetAnswers();

        addQuestions(module, Enumerable.Range(0, 5 * 6).Select(ix =>
            makeQuestion(Question.ColorBrailleColor, module,
                questionSprite: Sprites.GenerateCirclesSprite(5 * 2, 3, 1 << ix, 20, 5, outline: true, vertical: true),
                correctAnswers: new[] { colorNames[colorIxs[ix]] })));
    }
}