using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMalfunctions
{
    [Question("What number was to the {1} of the displayed letter in {0}?", ThreeColumns6Answers, Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(1, 999)]
    QNumber,

    [Question("What letter was displayed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    QLetter,

    [Discriminator("the Malfunctions whose {1} number was {0}", Arguments = ["123", "left", "456", "right"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DNumber,

    [Discriminator("the Malfunctions whose letter was {0}", Arguments = ["A", "B", "C"], ArgumentGroupSize = 1)]
    DLetter,
}

public partial class SouvenirModule
{
    [Handler("malfunctions", "Malfunctions", typeof(SMalfunctions), "Timwi")]
    [ManualQuestion("What were the numbers and letter shown at the bottom?")]
    private IEnumerator<SouvenirInstruction> ProcessMalfunctions(ModuleData module)
    {
        var comp = GetComponent(module, "malfunctions");
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
        var theLetter = GetField<string>(comp, "ruleLetter").Get(s => s.Length != 1 ? "expected length 1" : s[0] < 'A' || s[0] > 'Z' ? "expected A–Z" : null);

        yield return question(SMalfunctions.QNumber, args: ["left"]).AvoidDiscriminators("left").Answers(lNum.ToString());
        yield return question(SMalfunctions.QNumber, args: ["right"]).AvoidDiscriminators("right").Answers(rNum.ToString());
        yield return question(SMalfunctions.QLetter).AvoidDiscriminators(SMalfunctions.DLetter).Answers(theLetter);
        yield return new Discriminator(SMalfunctions.DNumber, "left", lNum, args: [lNum.ToString(), "left"]);
        yield return new Discriminator(SMalfunctions.DNumber, "right", rNum, args: [rNum.ToString(), "right"]);
        yield return new Discriminator(SMalfunctions.DLetter, "letter", theLetter, args: [theLetter]);
    }
}
