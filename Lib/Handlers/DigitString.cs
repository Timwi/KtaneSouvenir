using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDigitString
{
    [Question("What was the initial number in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings("1-9", "6*0-9", "1-9")]
    InitialNumber
}

public partial class SouvenirModule
{
    [Handler("digitString", "Digit String", typeof(SDigitString), "GoodHood")]
    [ManualQuestion("What was the displayed digit string?")]
    private IEnumerator<SouvenirInstruction> ProcessDigitString(ModuleData module)
    {
        var comp = GetComponent(module, "digitString");
        yield return WaitForSolve;

        var storedInitialString = GetField<string>(comp, "shownString").Get(x => x.Length != 8 ? "Expected length 8" : null);

        // Disables the numbered buttons. This prevents the defuser from entering a number into the display to see their answer
        var numberButtons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get();

        foreach (KMSelectable button in numberButtons)
        {
            button.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
                button.AddInteractionPunch(0.2f);
                return false;
            };
        }

        yield return question(SDigitString.InitialNumber).Answers(storedInitialString);
    }
}
