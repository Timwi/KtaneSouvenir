using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBoneAppleTea
{
    [SouvenirQuestion("Which phrase was shown on {0}?", OneColumn4Answers, "Bone Apple Tea", "Seizure Salad", "Hey to break it to ya", "This is oak ward", "Clea Shay", "It's in tents", "Bench watch", "You're an armature", "Man hat in", "Try all and era", "Million Air", "Die of beaties", "Rush and roulette", "Night and shining armour", "What a nice jester", "In some near", "This is my master peace", "I'm in a colder sac", "Cereal killer", "I come here off ten", "Slide of ham", "Test lah", "Refreshing campaign", "I'm being more pacific", "God blast you", "BC soft wear", "Sense in humor", "The three must of tears", "Third da men chin", "Prang mantas", "Hammy downs", "Yum, a case idea", "Dandy long legs", "Can't merge, little lone drive", "My guest is", "Sink", "You lake it", "Emit da feet")]
    Phrase
}

public partial class SouvenirModule
{
    [SouvenirHandler("boneAppleTea", "Bone Apple Tea", typeof(SBoneAppleTea), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBoneAppleTea(ModuleData module)
    {
        yield return WaitForSolve;

        var allAnswers = Question.BoneAppleTeaPhrase.GetAnswers();

        var comp = GetComponent(module, "boneAppleTeaScript");
        var p1 = GetIntField(comp, "phrase1").Get(min: 0, max: allAnswers.Length - 1);
        var p2 = GetIntField(comp, "phrase2").Get(min: 0, max: allAnswers.Length - 1);

        var texts = GetArrayField<TextMesh>(comp, "texts", true).Get(expectedLength: 4);
        texts[0].text = "Matcha";
        texts[1].text = "diffused!";
        texts[2].text = "✓";
        texts[3].text = "✓";

        addQuestion(module, Question.BoneAppleTeaPhrase, correctAnswers: new[] { allAnswers[p1], allAnswers[p2] });
    }
}