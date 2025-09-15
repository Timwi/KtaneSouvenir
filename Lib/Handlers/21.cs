using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum S21
{
    [SouvenirQuestion("What was the displayed number in {0}?", ThreeColumns6Answers, Type = AnswerType.SixtyFourFont, ExampleAnswers = ["A0A3", "K1I1", "3000", "83F1", "ABCD", "1234"])]
    DisplayedNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("TwennyWan", "21", typeof(S21), "Anonymous")]
    private IEnumerator<SouvenirInstruction> Process21(ModuleData module) =>
        process6421(module, "TwennyWan", "numberin21", "0123456789ABCDEFGHIJK", 21, 9261, 194480, Question._21DisplayedNumber);
}