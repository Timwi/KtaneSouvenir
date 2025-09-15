using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SIdentityParade
{
    [SouvenirQuestion("Which hair color {1} listed in {0}?", TwoColumns4Answers, "Black", "Blonde", "Brown", "Grey", "Red", "White", Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    HairColors,

    [SouvenirQuestion("Which build {1} listed in {0}?", TwoColumns4Answers, "Fat", "Hunched", "Muscular", "Short", "Slim", "Tall", Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Builds,

    [SouvenirQuestion("Which attire {1} listed in {0}?", TwoColumns4Answers, "Blazer", "Hoodie", "Jumper", "Suit", "T-shirt", "Tank top", Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Attires
}

public partial class SouvenirModule
{
    [SouvenirHandler("identityParade", "Identity Parade", typeof(SIdentityParade), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessIdentityParade(ModuleData module)
    {
        var comp = GetComponent(module, "identityParadeScript");

        yield return WaitForSolve;

        foreach (var btnFieldName in new[] { "hairLeft", "hairRight", "buildLeft", "buildRight", "attireLeft", "attireRight", "suspectLeft", "suspectRight", "convictBut" })
        {
            var btn = GetField<KMSelectable>(comp, btnFieldName, isPublic: true).Get();
            btn.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
                btn.AddInteractionPunch(0.5f);
                return false;
            };
        }

        var textMeshes = new[] { "hairText", "buildText", "attireText", "suspectText" }.Select(fldName => GetField<TextMesh>(comp, fldName, isPublic: true).Get()).ToArray();
        textMeshes[0].text = "Identity";
        textMeshes[1].text = "Parade";
        textMeshes[2].text = "has been";
        textMeshes[3].text = "solved";

        var hairs = GetListField<string>(comp, "hairEntries").Get(expectedLength: 3);
        var builds = GetListField<string>(comp, "buildEntries").Get(expectedLength: 3);
        var attires = GetListField<string>(comp, "attireEntries").Get(expectedLength: 3);

        var validHairs = new[] { "Black", "Blonde", "Brown", "Grey", "Red", "White" };
        var validBuilds = new[] { "Fat", "Hunched", "Muscular", "Short", "Slim", "Tall" };
        var validAttires = new[] { "Blazer", "Hoodie", "Jumper", "Suit", "T-shirt", "Tank top" };

        addQuestions(module,
            makeQuestion(Question.IdentityParadeHairColors, module, formatArgs: new[] { "was" }, correctAnswers: hairs.ToArray()),
            makeQuestion(Question.IdentityParadeHairColors, module, formatArgs: new[] { "was not" }, correctAnswers: validHairs.Except(hairs).ToArray()),
            makeQuestion(Question.IdentityParadeBuilds, module, formatArgs: new[] { "was" }, correctAnswers: builds.ToArray()),
            makeQuestion(Question.IdentityParadeBuilds, module, formatArgs: new[] { "was not" }, correctAnswers: validBuilds.Except(builds).ToArray()),
            makeQuestion(Question.IdentityParadeAttires, module, formatArgs: new[] { "was" }, correctAnswers: attires.ToArray()),
            makeQuestion(Question.IdentityParadeAttires, module, formatArgs: new[] { "was not" }, correctAnswers: validAttires.Except(attires).ToArray()));
    }
}