using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SFunctions
{
    [SouvenirQuestion("What was the last digit of your first query’s result in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    LastDigit,

    [SouvenirQuestion("What number was to the left of the displayed letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 999)]
    LeftNumber,

    [SouvenirQuestion("What letter was displayed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter,

    [SouvenirQuestion("What number was to the right of the displayed letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 999)]
    RightNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("qFunctions", "Functions", typeof(SFunctions), "JerryEris")]
    [SouvenirManualQuestion("What was the last digit of the first query result?")]
    [SouvenirManualQuestion("What were the numbers and letter shown at the bottom?")]
    private IEnumerator<SouvenirInstruction> ProcessFunctions(ModuleData module)
    {
        var comp = GetComponent(module, "qFunctions");
        yield return WaitForSolve;

        // Disables the numbered buttons. This prevents the defuser from entering a number into the display to see their answer
        var numberButtons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get();
        var commaButton = GetField<KMSelectable>(comp, "buttonComma", isPublic: true).Get();

        foreach (KMSelectable button in numberButtons)
        {
            button.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
                button.AddInteractionPunch(0.2f);
                return false;
            };
        }

        commaButton.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
            commaButton.AddInteractionPunch(0.2f);
            return false;
        };

        var lNum = GetIntField(comp, "numberA").Get(1, 999);
        var rNum = GetIntField(comp, "numberB").Get(1, 999);
        var theLetter = GetField<string>(comp, "ruleLetter").Get(s => s.Length != 1 ? "expected length 1" : null);

        var lastDigit = GetIntField(comp, "firstLastDigit").Get(-1, 9);
        if (lastDigit != -1)
            yield return question(SFunctions.LastDigit).Answers(lastDigit.ToString());
        yield return question(SFunctions.LeftNumber).Answers(lNum.ToString(), preferredWrong: Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray());
        yield return question(SFunctions.Letter).Answers(theLetter);
        yield return question(SFunctions.RightNumber).Answers(rNum.ToString(), preferredWrong: Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray());
    }
}
