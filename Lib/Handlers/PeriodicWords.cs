using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPeriodicWords
{
    [SouvenirQuestion("What word was on the display in the {1} stage of {0}?", OneColumn4Answers, ExampleAnswers = ["ATTACKERS", "BUY", "SUPERPOSITION", "WHO"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayedWords
}

public partial class SouvenirModule
{
    [SouvenirHandler("periodicWordsRB", "Periodic Words", typeof(SPeriodicWords), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessPeriodicWords(ModuleData module)
    {
        var comp = GetComponent(module, "PeriodicWordsScript");

        yield return WaitForSolve;

        var words = GetArrayField<string>(comp, "Words").Get().Take(4).ToArray();
        for (var stage = 0; stage < 3; stage++)
            yield return question(SPeriodicWords.DisplayedWords, args: [Ordinal(stage + 1)]).Answers(words[stage], preferredWrong: words);
    }
}