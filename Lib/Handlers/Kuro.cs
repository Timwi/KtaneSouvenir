using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SKuro
{
    [SouvenirQuestion("What was Kuro’s mood in {0}?", TwoColumns4Answers, "Angry", "Happy", "Neutral", "Curious", "Devious")]
    Mood
}

public partial class SouvenirModule
{
    [SouvenirHandler("Kuro", "Kuro", typeof(SKuro), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessKuro(ModuleData module)
    {
        var comp = GetComponent(module, "Kuro");
        yield return WaitForSolve;

        var desiredTask = GetField<Enum>(comp, "desiredTask").Get().ToString();
        var moods = GetArrayField<Texture2D>(comp, "kuroMoods", isPublic: true).Get(expectedLength: 5).Select(texture => texture.name);

        if (desiredTask is not "Eat" and not "PlayKTANE")
            yield return legitimatelyNoQuestion(module.Module, "Mood is not relevant to the answer");

        var currentMood = GetField<Enum>(comp, "currentMood").Get().ToString();
        yield return question(SKuro.Mood).Answers(currentMood, preferredWrong: moods.ToArray());
    }
}