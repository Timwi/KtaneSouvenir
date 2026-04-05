using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SQuizBuzz
{
    [Question("What was the number initially on the display in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(6, 74)]
    StartingNumber
}

public partial class SouvenirModule
{
    [Handler("quizBuzz", "Quiz Buzz", typeof(SQuizBuzz), "Kuro")]
    [ManualQuestion("What was the number initially on the display?")]
    private IEnumerator<SouvenirInstruction> ProcessQuizBuzz(ModuleData module)
    {
        var comp = GetComponent(module, "quizBuzz");

        yield return WaitForSolve;

        // Disables the numbered buttons. This prevents the defuser from entering a number into the display to see their answer
        var numberButtons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get();
        var deleteButon = GetField<KMSelectable>(comp, "deleteButton", isPublic: true).Get();

        foreach (KMSelectable button in numberButtons)
        {
            button.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
                button.AddInteractionPunch(0.2f);
                return false;
            };
        }

        deleteButon.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
            deleteButon.AddInteractionPunch(0.2f);
            return false;
        };

        var startingNumber = GetIntField(comp, "startNumber").Get(min: 6, max: 74);
        yield return question(SQuizBuzz.StartingNumber).Answers(startingNumber.ToString());
    }
}
