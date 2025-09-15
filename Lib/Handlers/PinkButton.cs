using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPinkButton
{
    [SouvenirQuestion("What was the {1} word in {0}?", TwoColumns4Answers, "BLK", "RED", "GRN", "YLW", "BLU", "MGT", "CYN", "WHT", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Words,

    [SouvenirQuestion("What was the {1} color in {0}?", TwoColumns4Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("PinkButtonModule", "Pink Button", typeof(SPinkButton), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessPinkButton(ModuleData module)
    {
        var comp = GetComponent(module, "PinkButtonScript");
        var words = GetArrayField<int>(comp, "_words").Get(expectedLength: 4, validator: v => v is < 0 or > 7 ? "expected range 0–7" : null);
        var colors = GetArrayField<int>(comp, "_colors").Get(expectedLength: 4, validator: v => v is < 0 or > 7 ? "expected range 0–7" : null);

        var abbreviatedColorNames = GetStaticField<string[]>(comp.GetType(), "_abbreviatedColorNames").Get(v => v.Length != 8 ? "expected length 8" : null);
        var colorNames = GetStaticField<string[]>(comp.GetType(), "_colorNames").Get(v => v.Length != 8 ? "expected length 8" : null);

        yield return WaitForSolve;

        addQuestions(module,
            Enumerable.Range(0, 4).SelectMany(ix => Ut.NewArray(
                 makeQuestion(Question.PinkButtonWords, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { abbreviatedColorNames[words[ix]] }),
                 makeQuestion(Question.PinkButtonColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colorNames[colors[ix]] }))));
    }
}