using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SH
{
    [Question("What was the transmitted letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z")]
    Letter
}

public partial class SouvenirModule
{
    [Handler("Averageh", "h", typeof(SH), "Hawker")]
    [ManualQuestion("What was the transmitted letter?")]
    private IEnumerator<SouvenirInstruction> ProcessH(ModuleData module)
    {
        var comp = GetComponent(module, "HexOS");
        yield return WaitForSolve;

        var answer = ((char) ('A' + GetIntField(comp, "WhatToSubmit").Get(min: 0, max: 25))).ToString();
        yield return question(SH.Letter).Answers(answer);
    }
}