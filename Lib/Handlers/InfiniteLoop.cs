using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SInfiniteLoop
{
    [Question("Which of these sequences was transmitted in {0}?", OneColumn4Answers)]
    [AnswerGenerator.Strings("●▬", " ", "●▬", " ", "●▬", " ", "●▬", " ", "●▬", " ", "●▬", " ", "●▬")]
    Morse
}

public partial class SouvenirModule
{
    [Handler("InfiniteLoop", "Infinite Loop", typeof(SInfiniteLoop), "Espik")]
    [ManualQuestion("What was the Morse code?")]
    private IEnumerator<SouvenirInstruction> ProcessInfiniteLoop(ModuleData module)
    {
        var comp = GetComponent(module, "InfiniteLoop");
        yield return WaitForSolve;

        var selectedMorse = GetField<string>(comp, "MorseVersion").Get().Replace(".", "●").Replace("-", "▬");
        var morseSnippets = new string[selectedMorse.Length];

        for (var i = 0; i < morseSnippets.Length; i++)
        {
            morseSnippets[i] = Enumerable.Range(i, 7).Select(x => selectedMorse[x % selectedMorse.Length]).JoinString();
            morseSnippets[i] = morseSnippets[i].JoinString(" ");
        }

        yield return question(SInfiniteLoop.Morse).Answers(morseSnippets);
    }
}
