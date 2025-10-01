using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMissingLetter
{
    [SouvenirQuestion("What letter was missing in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    MissingLetter
}

public partial class SouvenirModule
{
    [SouvenirHandler("theMissingLetter", "Missing Letter", typeof(SMissingLetter), "KiloBites", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessMissingLetter(ModuleData module)
    {
        var comp = GetComponent(module, "TheMissingLetterScript");
        yield return WaitForSolve;

        var letters = GetListField<char>(comp, "alph").Get(expectedLength: 26);
        var n = GetIntField(comp, "resultN").Get(min: 1, max: 5);

        yield return question(SMissingLetter.MissingLetter).Answers(letters.Last().ToString());
    }
}
