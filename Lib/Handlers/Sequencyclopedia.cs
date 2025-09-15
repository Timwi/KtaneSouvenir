using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSequencyclopedia
{
    [SouvenirQuestion("What sequence was used in {0}?", TwoColumns4Answers, ExampleAnswers = ["A000001", "A069420", "A111111"])]
    [AnswerGenerator.Integers(0, 1000000, "'A'000000")]
    Sequence
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheSequencyclopedia", "Sequencyclopedia", typeof(SSequencyclopedia), "BigCrunch22", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessSequencyclopedia(ModuleData module)
    {
        var comp = GetComponent(module, "TheSequencyclopediaScript");
        yield return WaitForSolve;

        var maxSeqId = int.Parse(GetField<string>(comp, "Tridal").Get(str => str == "" ? "Tridal is empty, meaning module was unable to gather the amount of sequence" : null));
        var answer = GetField<string>(comp, "APass").Get();
        var wrongAnswers = new HashSet<string>();
        wrongAnswers.Add(answer);
        while (wrongAnswers.Count < 6)
            foreach (var ans in new AnswerGenerator.Integers(0, maxSeqId, "'A'000000").GetAnswers(this).Take(6 - wrongAnswers.Count))
                wrongAnswers.Add(ans);

        yield return question(SSequencyclopedia.Sequence).Answers(answer, preferredWrong: wrongAnswers.ToArray());
    }
}