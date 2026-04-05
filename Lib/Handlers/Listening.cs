using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SListening
{
    [Question("What clip was played in {0}?", ThreeColumns6Answers, AudioFieldName = "ListeningAudio", Type = AnswerType.Audio)]
    Sound
}

public partial class SouvenirModule
{
    [Handler("Listening", "Listening", typeof(SListening), "Timwi")]
    [ManualQuestion("What sound played?")]
    private IEnumerator<SouvenirInstruction> ProcessListening(ModuleData module)
    {
        var comp = GetComponent(module, "Listening");
        var fldCode = GetArrayField<char>(comp, "codeInput");

        yield return WaitForActivate;

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
