using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SALetter
{
    [SouvenirQuestion("What was the initial letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z")]
    InitialLetter
}

public partial class SouvenirModule
{
    [SouvenirHandler("LetterModule", "A Letter", typeof(SALetter), "Sierra")]
    private IEnumerator<SouvenirInstruction> ProcessALetter(ModuleData module)
    {
        var comp = GetComponent(module, "Letter");
        yield return WaitForSolve;
        var initialLetter = GetField<string>(comp, "LetterList").Get(x => x.Length != 26 ? "expected length 26" : null)[0];

        yield return question(SALetter.InitialLetter).Answers(initialLetter.ToString());
    }
}