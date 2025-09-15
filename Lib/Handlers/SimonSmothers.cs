using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonSmothers
{
    [SouvenirQuestion("What was the color of the {1} flash in {0}?", ThreeColumns6Answers, "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Colors,

    [SouvenirQuestion("What was the direction of the {1} flash in {0}?", TwoColumns4Answers, "Up", "Down", "Left", "Right", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Directions
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSmothers", "Simon Smothers", typeof(SSimonSmothers), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSmothers(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSmothersScript");

        yield return WaitForSolve;

        var flashes = GetField<IList>(comp, "flashes").Get();
        var qs = new List<QandA>();
        for (int pos = 0, length = flashes.Count; pos < length; pos++)
        {
            var position = Ordinal(pos + 1);
            qs.Add(makeQuestion(Question.SimonSmothersColors, module, formatArgs: new[] { position }, correctAnswers: new[] { GetField<Enum>(flashes[pos], "color", isPublic: true).Get().ToString() }));
            qs.Add(makeQuestion(Question.SimonSmothersDirections, module, formatArgs: new[] { position }, correctAnswers: new[] { GetField<Enum>(flashes[pos], "direction", isPublic: true).Get().ToString() }));
        }
        addQuestions(module, qs);
    }
}