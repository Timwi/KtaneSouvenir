using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMetamorse
{
    [Question("What was the extracted letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z")]
    ExtractedLetter
}

public partial class SouvenirModule
{
    [Handler("metamorse", "Metamorse", typeof(SMetamorse), "tandyCake")]
    [ManualQuestion("What was the extracted letter?")]
    private IEnumerator<SouvenirInstruction> ProcessMetamorse(ModuleData module)
    {
        var comp = GetComponent(module, "MetamorseScript");
        var fldBigChar = GetField<char>(comp, "greaterLetter");

        yield return WaitForSolve;
        yield return question(SMetamorse.ExtractedLetter).Answers(fldBigChar.Get().ToString());
    }
}