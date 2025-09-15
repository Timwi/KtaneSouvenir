using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S64
{
    [SouvenirQuestion("What was the displayed number in {0}?", ThreeColumns6Answers, Type = AnswerType.SixtyFourFont, ExampleAnswers = ["A0A3", "bbda", "30", "h3X1", "ABCD", "1234"])]
    DisplayedNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("64", "64", typeof(S64), "Kuro")]
    private IEnumerator<SouvenirInstruction> Process64(ModuleData module) =>
        process6421(module, "SixtyFourScript", "numberIn64", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/", 64, 0, 16777216, Question._64DisplayedNumber);
}
