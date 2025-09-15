using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGlitchedButton
{
    [SouvenirQuestion("What was the cycling bit sequence in {0}?", OneColumn4Answers)]
    [AnswerGenerator.Strings(12, "01")]
    Sequence
}

public partial class SouvenirModule
{
    [SouvenirHandler("GlitchedButtonModule", "Glitched Button", typeof(SGlitchedButton), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessGlitchedButton(ModuleData module)
    {
        var comp = GetComponent(module, "GlitchedButtonScript");
        yield return WaitForSolve;

        var correctAnswer = GetField<string>(comp, "_cyclingBits").Get();
        var wrongAnswers = new List<string>();
        var gen = new AnswerGenerator.Strings(16, "01");
        foreach (var wrong in gen.GetAnswers(this))
        {
            if (Enumerable.Range(0, 16).Any(amount => wrong.Substring(amount) + wrong.Substring(0, amount) == correctAnswer))
                continue;
            wrongAnswers.Add(wrong);
            if (wrongAnswers.Count == 3)
                break;
        }

        yield return question(SGlitchedButton.Sequence).Answers(Enumerable.Range(0, 12).Select(amount => correctAnswer.Substring(amount) + correctAnswer.Substring(0, amount)).ToArray(), preferredWrong: wrongAnswers.ToArray());
    }
}