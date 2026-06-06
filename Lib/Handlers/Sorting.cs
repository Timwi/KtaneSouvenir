using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSorting
{
    [Question("Which sorting algorithm was used in {0}?", TwoColumns4Answers, "BUBBLE", "SELECTION", "INSERTION", "RADIX", "MERGE", "COMB", "HEAP", "COCKTAIL", "ODDEVEN", "CYCLE", "FIVE", "QUICK", "SLOW", "SHELL", "STOOGE")]
    Algorithm
}

public partial class SouvenirModule
{
    [Handler("sorting", "Sorting", typeof(SSorting), "Espik")]
    [ManualQuestion("Which sorting algorithm was used?")]
    private IEnumerator<SouvenirInstruction> ProcessSorting(ModuleData module)
    {
        var comp = GetComponent(module, "Sorting");
        yield return WaitForSolve;

        if (GetField<bool>(comp, "_bogoSort").Get())
            yield return legitimatelyNoQuestion(module, "The module was solved with a bogo sort.");

        var algorithm = GetField<string>(comp, "_currentAlgorithm").Get();
        yield return question(SSorting.Algorithm).Answers(algorithm);
    }
}
