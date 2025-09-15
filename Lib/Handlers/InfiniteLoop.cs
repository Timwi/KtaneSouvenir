using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SInfiniteLoop
{
    [SouvenirQuestion("What was the selected word in {0}?", TwoColumns4Answers, "anchor", "axions", "brutal", "bunker", "ceased", "cypher", "demote", "devoid", "ejects", "expend", "fixate", "fondly", "geyser", "guitar", "hexing", "hybrid", "incite", "inject", "jacked", "jigsaw", "kayaks", "komodo", "lazuli", "logjam", "maimed", "musket", "nebula", "nuking", "overdo", "oblong", "photon", "probed", "quartz", "quebec", "refute", "regime", "sierra", "swerve", "tenacy", "thymes", "ultima", "utopia", "valved", "viable", "wither", "wrench", "xenons", "xylose", "yanked", "yellow", "zigged", "zodiac")]
    SelectedWord
}

public partial class SouvenirModule
{
    [SouvenirHandler("InfiniteLoop", "Infinite Loop", typeof(SInfiniteLoop), "Eltrick")]
    private IEnumerator<SouvenirInstruction> ProcessInfiniteLoop(ModuleData module)
    {
        var comp = GetComponent(module, "InfiniteLoop");
        yield return WaitForSolve;

        var selectedWord = GetField<string>(comp, "SelectedWord").Get();
        yield return question(SInfiniteLoop.SelectedWord).Answers(selectedWord);
    }
}