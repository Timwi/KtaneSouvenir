using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SReverseMorse
{
    [SouvenirQuestion("What was the {1} symbol in the {2} message of {0}?", ThreeColumns6Answers, "A", "L", "Q", "T", "X", "Z", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2, Type = AnswerType.CubeFont)]
    Symbols,

    [SouvenirQuestion("What was the color of the {1} symbol in the {2} message of {0}?", ThreeColumns6Answers, "red", "green", "blue", "purple", "yellow", "orange", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("reverseMorse", "Reverse Morse", typeof(SReverseMorse), "Quinn Wuest")]
    [SouvenirManualQuestion("What were the displayed symbols and their colors?")]
    private IEnumerator<SouvenirInstruction> ProcessReverseMorse(ModuleData module)
    {
        var comp = GetComponent(module, "reverseMorseScript");
        var message1 = GetListField<string>(comp, "selectedLetters1", isPublic: true).Get(expectedLength: 6);
        var message2 = GetListField<string>(comp, "selectedLetters2", isPublic: true).Get(expectedLength: 6);

        var messages = new[] { message1, message2 };
        string table = "XKOY9E4P1BWJI8FNVZQUA50G7DHMT3SC62LR";

        yield return WaitForSolve;
        for (int msg = 0; msg < messages.Length; msg++)
        {
            for (var ix = 0; ix < 6; ix++)
            {
                int x = table.IndexOf(messages[msg][ix]) % 6;
                int y = table.IndexOf(messages[msg][ix]) / 6;
                yield return question(SReverseMorse.Symbols, args: [Ordinal(ix + 1), Ordinal(msg + 1)]).Answers(SReverseMorse.Symbols.GetAnswers()[y].ToString());
                yield return question(SReverseMorse.Colors, args: [Ordinal(ix + 1), Ordinal(msg + 1)]).Answers(SReverseMorse.Colors.GetAnswers()[x].ToString());
            }
        }
    }
}