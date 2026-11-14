using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SStackedSequences
{
    [Question("Which of these flashes appeared in the combined sequence in {0}?", OneColumn4Answers, AnswerType = InfoType.Sprites)]
    [AnswerGenerator.StackedSequences(6, 40, 40)]
    SequenceParts
}

namespace Souvenir
{
    public static partial class AnswerGenerator
    {
        /// <summary>
        ///     Generates sprites in which circles are arranged in a rectilinear grid.</summary>
        /// <param name="length">
        ///     Specifies the number of circles per row.</param>
        /// <param name="radius">
        ///     Specifies the radius of each circle, in pixels.</param>
        /// <param name="gap">
        ///     Specifies the gap between circles, in pixels.</param>
        public class StackedSequences(int length, int radius, int gap) : AnswerGeneratorAttribute<Sprite>
        {
            public override IEnumerable<Sprite> GetAnswers(SouvenirModule module)
            {
                while (true)
                    yield return Sprites.GenerateStackedSequencesSprite(length, Enumerable.Range(0, 6).Select(x => Random.Range(0, 3)).ToArray(), radius, gap);
            }

            public override int Count => length;
        }
    }
}

public partial class SouvenirModule
{
    [Handler("stackedSequences", "Stacked Sequences", typeof(SStackedSequences), "Espik")]
    [ManualQuestion("What flashes appeared in the combined sequence?")]
    private IEnumerator<SouvenirInstruction> ProcessStackedSequences(ModuleData module)
    {
        var comp = GetComponent(module, "stackedSequencesScript");
        yield return WaitForSolve;

        var sequences = GetArrayField<List<int>>(comp, "answer").Get(expectedLength: 2);
        var sequenceCounts = sequences.Select(x => x.Count).ToArray();

        var totalLength = sequenceCounts[0] * sequenceCounts[1];
        var combinedSequence = new int[totalLength];

        for (var i = 0; i < sequenceCounts[1]; i++)
            for (var j = 0; j < sequences[0].Count(); j++)
                combinedSequence[i * sequenceCounts[0] + j] += sequences[0][j];

        for (var i = 0; i < sequenceCounts[0]; i++)
            for (var j = 0; j < sequences[1].Count(); j++)
                combinedSequence[i * sequenceCounts[1] + j] += sequences[1][j];

        var sequenceSnippets = new Sprite[combinedSequence.Length];

        for (var i = 0; i < combinedSequence.Length; i++)
            sequenceSnippets[i] = Sprites.GenerateStackedSequencesSprite(6, Enumerable.Range(i, 6).Select(x => combinedSequence[x % combinedSequence.Length]).ToArray(), 40, 40);

        yield return question(SStackedSequences.SequenceParts).Answers(sequenceSnippets);
    }
}
