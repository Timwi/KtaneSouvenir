using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWhosOnMorse
{
    [SouvenirQuestion("What word was transmitted in the {1} stage on {0}?", ThreeColumns6Answers, "SHELL", "HALLS", "SLICK", "TRICK", "BOXES", "LEAKS", "STROBE", "BISTRO", "FLICK", "BOMBS", "BREAK", "BRICK", "STEAK", "STING", "VECTOR", "BEATS", "CURSE", "NICE", "VERB", "NEARLY", "CREEK", "TRIBE", "CYBER", "CINEMA", "KOALA", "WATER", "WHISK", "MATTER", "KEYS", "STUCK", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    TransmitDisplay
}

public partial class SouvenirModule
{
    [SouvenirHandler("whosOnMorseModule", "Who’s on Morse", typeof(SWhosOnMorse), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessWhosOnMorse(ModuleData module)
    {
        var comp = GetComponent(module, "WhosOnMorseScript");
        var curStageField = GetField<int>(comp, "stage");
        var wordBank = GetField<string[]>(comp, "aWords").Get(); // The entire word bank from Who's On Morse. 
        var idxMorseWord = GetField<int>(comp, "lightMorsePos");
        var storedIdxDisplays = new int?[3];

        for (var x = 0; x < 3 && module.Unsolved; x++)
            while (curStageField.Get() == x && module.Unsolved)
            {
                storedIdxDisplays[x] = idxMorseWord.Get();
                yield return new WaitForSeconds(0.1f);
            }
        yield return WaitForSolve;
        for (var stage = 0; stage < storedIdxDisplays.Length; stage++)
            if (storedIdxDisplays[stage] != null)
                yield return question(SWhosOnMorse.TransmitDisplay, args: [Ordinal(stage + 1)]).Answers(wordBank[storedIdxDisplays[stage].Value], preferredWrong: storedIdxDisplays.Select(a => a == null ? null : wordBank[a.Value]).Where(s => s != null).ToArray());
    }
}