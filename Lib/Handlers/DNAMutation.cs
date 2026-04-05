using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SDNAMutation
{
    [Question("What was the letter of the {1} given nucleotide in {0}?", TwoColumns4Answers, "G", "C", "A", "T", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Letter,

    [Question("What was the color of the {1} given nucleotide in {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    NucleotideColor,

    [Question("What was the color of the {1} given DNA strand in {0}?", TwoColumns4Answers, "Green", "Red", "Yellow", "Blue", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    StrandColor
}

public partial class SouvenirModule
{
    [Handler("DNAMutation", "DNA Mutation", typeof(SDNAMutation), "thunder725")]
    [ManualQuestion("What were the DNA strands’ colors?")]
    [ManualQuestion("What were the given nucleotides’ letters and colors?")]
    private IEnumerator<SouvenirInstruction> ProcessDNAMutation(ModuleData module)
    {
        var comp = GetComponent(module, "DNAMutationScript");

        yield return WaitForSolve;

        var answerChemicals = GetArrayField<int>(comp, "Chemical").Get(expectedLength: 9);
        var answerNucleotideColor = GetArrayField<int>(comp, "Color").Get(expectedLength: 9);
        var answerStrandColor = GetArrayField<int>(comp, "StrandColors").Get(expectedLength: 9);

        var letters = SDNAMutation.Letter.GetAnswers();
        var nucleotideColors = SDNAMutation.NucleotideColor.GetAnswers();
        var strandColors = SDNAMutation.StrandColor.GetAnswers();

        for (var i = 0; i < 9; i++)
        {
            yield return question(SDNAMutation.Letter, args: [Ordinal(i + 1)]).Answers(letters[answerChemicals[i]]);
            yield return question(SDNAMutation.NucleotideColor, args: [Ordinal(i + 1)]).Answers(nucleotideColors[answerNucleotideColor[i]]);
            yield return question(SDNAMutation.StrandColor, args: [Ordinal(i + 1)]).Answers(strandColors[answerStrandColor[i]]);
        }
    }
}
