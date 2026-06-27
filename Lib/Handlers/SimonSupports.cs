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

        var selectedAndNone = new string[6];
        for (var i = 0; i < selectedColors.Length; i++)
            selectedAndNone[i] = selectedColors[i];

        selectedAndNone[5] = "none";

        var topicsByColor = GetArrayField<List<int>>(comp, "flashes").Get(expectedLength: 5);
        var colorsByTopic = new List<string>[3].Select(x => new List<string>()).ToArray();

        for (var i = 0; i < colorsByTopic.Length; i++)
        {
            for (var j = 0; j < topicsByColor.Length; j++)
            {
                if (topicsByColor[j].Contains(i))
                    colorsByTopic[i].Add(selectedColors[j]);
            }
        }

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
