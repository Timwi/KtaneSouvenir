using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SListening
{
    [SouvenirQuestion("What clip was played in {0}?", ThreeColumns6Answers, AudioFieldName = "ListeningAudio", Type = AnswerType.Audio)]
    Sound
}

public partial class SouvenirModule
{
    [SouvenirHandler("Listening", "Listening", typeof(SListening), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessListening(ModuleData module)
    {
        var comp = GetComponent(module, "Listening");
        var fldCode = GetArrayField<char>(comp, "codeInput");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        yield return WaitForSolve;

        var button = GetField<KMSelectable>(comp, "PlayButton", true).Get();
        button.OnInteract = () =>
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, button.transform);
            return false;
        };
        var correctCode = fldCode.Get(expectedLength: 5).JoinString();

        var codes = "$#$#*|$*$**|*&*&&|###&$|&#**&|**$*#|&&$&*|&#&&#|$$*$*|&$#$&|*#&*&|#$#&$|$#$*&|$&$$*|*$*$*|#&$&&|&*$*$|&$**&|&#$$#|&$$&*|**###|*#$&&|$&**#|$&&**|$&#$$|#&&*#|##*$*|$*&##|#$$&*|*$$&$|$#*$&|&&&**|$&&*&|**$$$|**#**|#&&&&|#$$**|#&$##|#&$*&|&**$$|&$&##".Split('|');

        yield return question(SListening.Sound).Answers(ListeningAudio[codes.IndexOf(s => s.Equals(correctCode))]);
    }
}