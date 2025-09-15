using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDoofenshmirtzEvilInc
{
    [SouvenirQuestion("What jingle played in {0}?", OneColumn4Answers, Type = AnswerType.Audio, ForeignAudioID = "doofenshmirtzEvilIncModule", AudioSizeMultiplier = 8)]
    Jingles,

    [SouvenirQuestion("Which image was shown in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    Inators
}

public partial class SouvenirModule
{
    [SouvenirHandler("doofenshmirtzEvilIncModule", "Doofenshmirtz Evil Inc.", typeof(SDoofenshmirtzEvilInc), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessDoofenshmirtzEvilInc(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "DoofenshmirtzEvilIncScript");
        var allJingles = GetArrayField<AudioClip>(comp, "jingleclips", true).Get(expectedLength: 49);

        // I was TOLD that I can change the - to a –
        var usedJingles = GetListField<int>(comp, "selectedjingles").Get(expectedLength: 3, validator: v => v is < 0 or > 48 ? "Out of range 0–48" : null);

        // If I had a nickel for every time I changed - to a –, I would have two nickels! Which is not a lot, but it’s weird that it happened twice.
        var usedInators = GetArrayField<int>(comp, "selectedimages").Get(expectedLength: 2, validator: v => v is < 0 or > 63 ? "Out of range 0–63" : null);

        var allInators = GetArrayField<Sprite>(comp, "images", true).Get(expectedLength: 64).TranslateSpritesScaled(13f).ToArray();
        var inatorRenderers = GetArrayField<SpriteRenderer>(comp, "imageRends", true).Get(expectedLength: 2);
        foreach (var rend in inatorRenderers)
            rend.enabled = false;

        addQuestions(module,
            makeQuestion(Question.DoofenshmirtzEvilIncJingles, module, allAnswers: allJingles, correctAnswers: usedJingles.Select(i => allJingles[i]).ToArray()),
            makeQuestion(Question.DoofenshmirtzEvilIncInators, module, allAnswers: allInators, correctAnswers: usedInators.Select(i => allInators[i]).ToArray()));
    }
}