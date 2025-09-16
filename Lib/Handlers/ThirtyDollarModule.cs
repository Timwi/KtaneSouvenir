using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SThirtyDollarModule
{
    [SouvenirQuestion("Which sound was used in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "ThirtyDollarModule", AudioSizeMultiplier = 5)]
    Sounds
}

public partial class SouvenirModule
{
    [SouvenirHandler("ThirtyDollarModule", "Thirty Dollar Module", typeof(SThirtyDollarModule), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessThirtyDollarModule(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "ThirtyDollarModule");
        var displayTD = GetField<IList>(comp, "displaySounds").Get(v => v.Count is not 5 || v.Cast<object>().Any(o => o is null) ? "Expected 5 played sounds" : null);
        var allTD = GetField<IList>(comp, "sounds").Get(v => v.Count is not 204 || v.Cast<object>().Any(o => o is null) ? "Expected 204 total sounds" : null);
        var fldSound = GetField<string>(displayTD[0], "sound");
        var foreignID = SThirtyDollarModule.Sounds.GetQuestionAttribute().ForeignAudioID;
        var display = displayTD.Cast<object>().Select(o => fldSound.GetFrom(o)).Select(s => Sounds.GetForeignClip(foreignID, s)).ToArray();
        var all = allTD.Cast<object>().Select(o => fldSound.GetFrom(o)).Select(s => Sounds.GetForeignClip(foreignID, s)).ToArray();

        var displays = GetArrayField<Renderer>(comp, "DisplayEmojis", true).Get(expectedLength: 5);
        var emojis = GetArrayField<Texture>(comp, "Emojis", true).Get(expectedLength: 204);
        IEnumerator hideBacksolve()
        {
            for (var i = 0; i < displays.Length; i++)
            {
                yield return new WaitForSeconds(0.1f);
                displays[i].material.mainTexture = emojis[i % 2 == 0 ? 5 : 36];
            }
        }
        StartCoroutine(hideBacksolve());

        yield return question(SThirtyDollarModule.Sounds).Answers(display, all: all);
    }
}
