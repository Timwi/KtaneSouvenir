using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonSupports
{
    [Question("Which tie color {2} for the {1} topic in {0}?", TwoColumns4Answers, "Red", "Blue", "Yellow", "Orange", "Magenta", "Green", "Pink", "Lime", "Cyan", "White", "none", Arguments = [QandA.Ordinal, "flashed", QandA.Ordinal, "did not flash"], TranslateAnswers = true, TranslateArguments = [false, true], ArgumentGroupSize = 2)]
    Flashes
}

public partial class SouvenirModule
{
    [Handler("simonSupports", "Simon Supports", typeof(SSimonSupports), "Espik")]
    [ManualQuestion("What tie colors flashed for each topic?")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSupports(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSupportsScript");
        yield return WaitForSolve;

        var allColors = SSimonSupports.Flashes.GetAnswers().Take(10).ToArray();
        var selectedColorIndicies = GetArrayField<int>(comp, "col").Get(expectedLength: 10).Take(5).ToArray();
        var selectedColors = selectedColorIndicies.Select(ix => allColors[ix]).ToArray();
        var selectedAndNone = selectedColors.Concat(["none"]).ToArray();

        var topicsByColor = GetArrayField<List<int>>(comp, "flashes").Get(expectedLength: 5);
        var colorsByTopic = Ut.NewArray(3, _ => new List<string>());

        for (var topic = 0; topic < colorsByTopic.Length; topic++)
            for (var color = 0; color < topicsByColor.Length; color++)
                if (topicsByColor[color].Contains(topic))
                    colorsByTopic[topic].Add(selectedColors[color]);

        for (var i = 0; i < colorsByTopic.Length; i++)
        {
            switch (colorsByTopic[i].Count())
            {
                case <= 2: // 1 or 2 flashes
                    yield return question(SSimonSupports.Flashes, args: [Ordinal(i + 1), "flashed"]).Answers(colorsByTopic[i].ToArray(), all: selectedColors);
                    break;

                case >= 5: // 5 flashes
                    yield return question(SSimonSupports.Flashes, args: [Ordinal(i + 1), "did not flash"]).Answers("none", all: selectedAndNone);
                    break;

                default: // 3 or 4 flashes
                    yield return question(SSimonSupports.Flashes, args: [Ordinal(i + 1), "did not flash"]).Answers(selectedColors.Except(colorsByTopic[i].ToArray()).ToArray(), all: selectedAndNone);
                    break;
            }
        }
    }
}
