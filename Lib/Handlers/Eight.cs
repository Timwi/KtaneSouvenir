using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEight
{
    [Question("What was the last digit on the small display in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    LastSmallDisplayDigit,

    [Question("What was the position of the last broken digit in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 8)]
    LastBrokenDigitPosition,

    [Question("What were the last resulting digits in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(50, 99)]
    LastResultingDigits,

    [Question("What was the last displayed number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(10, 99)]
    LastDisplayedNumber
}

public partial class SouvenirModule
{
    [Handler("eight", "Eight", typeof(SEight), "NickLatkovich")]
    [ManualQuestion("What was the last digit on the small display?")]
    [ManualQuestion("What was the position of the last broken digit?")]
    [ManualQuestion("What were the last resulting digits?")]
    [ManualQuestion("What was the last displayed number?")]
    private IEnumerator<SouvenirInstruction> ProcessEight(ModuleData module)
    {
        var comp = GetComponent(module, "EightModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
            yield return legitimatelyNoQuestion(module, "The module was force-solved.");

        yield return question(SEight.LastSmallDisplayDigit).Answers(GetProperty<int>(comp, "souvenirLastStageDigit", true).Get().ToString());
        yield return question(SEight.LastBrokenDigitPosition).Answers((GetProperty<int>(comp, "souvenirLastBrokenDigitPosition", true).Get() + 1).ToString());
        yield return question(SEight.LastResultingDigits).Answers(GetProperty<int>(comp, "souvenirLastResultingDigits", true).Get().ToString(), preferredWrong: GetProperty<HashSet<int>>(comp, "souvenirPossibleLastResultingDigits", true).Get().Select(n => n.ToString().PadLeft(2, '0')).ToArray());
        var lastDisplayedNumber = GetProperty<int>(comp, "souvenirLastDisplayedNumber", true).Get();
        if (lastDisplayedNumber != -1)
            yield return question(SEight.LastDisplayedNumber).Answers(lastDisplayedNumber.ToString(), preferredWrong: GetProperty<HashSet<int>>(comp, "souvenirPossibleLastNumbers", true).Get().Select(n => n.ToString().PadLeft(2, '0')).ToArray());
    }
}