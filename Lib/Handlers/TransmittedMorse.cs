using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum STransmittedMorse
{
    [SouvenirQuestion("What was the {1} received message in {0}?", TwoColumns4Answers, "BOMBS", "SHORT", "UNDERSTOOD", "W1RES", "SOS", "MANUAL", "STRIKED", "WEREDEAD", "GOTASOUV", "EXPLOSION", "EXPERT", "RIP", "LISTEN", "DETONATE", "ROGER", "WELOSTBRO", "AMIDEAF", "KEYPAD", "DEFUSER", "NUCLEARWEAPONS", "KAPPA", "DELTA", "PI3", "SMOKE", "SENDHELP", "LOST", "SWAN", "NOMNOM", "BLUE", "BOOM", "CANCEL", "DEFUSED", "BROKEN", "MEMORY", "R6S8T", "TRANSMISSION", "UMWHAT", "GREEN", "EQUATIONSX", "RED", "ENERGY", "JESTER", "CONTACT", "LONG", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Message
}

public partial class SouvenirModule
{
    [SouvenirHandler("transmittedMorseModule", "Transmitted Morse", typeof(STransmittedMorse), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessTransmittedMorse(ModuleData module)
    {
        var comp = GetComponent(module, "TransmittedMorseScript");
        var fldMessage = GetField<string>(comp, "messagetrans");
        var fldStage = GetIntField(comp, "stage");

        var messages = new string[2];
        var stage = 0;

        while (module.Unsolved)
        {
            stage = fldStage.Get(min: 1, max: 2);
            messages[stage - 1] = fldMessage.Get();
            yield return new WaitForSeconds(.1f);
        }

        addQuestions(module, messages.Select((msg, index) => makeQuestion(Question.TransmittedMorseMessage, module,
            formatArgs: new[] { Ordinal(index + 1) },
            correctAnswers: new[] { msg },
            preferredWrongAnswers: messages)));
    }
}