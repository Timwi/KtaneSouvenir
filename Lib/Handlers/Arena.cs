using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SArena
{
    [SouvenirQuestion("What was the maximum weapon damage of the attack phase in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 99)]
    Damage,

    [SouvenirQuestion("Which enemy was present in the defend phase of {0}?", TwoColumns4Answers, "Bat", "Snake", "Spider", "Cobra", "Scorpion", "Mole", "Creeper", "Goblin", "Golem", "Robo-Mouse", "Skeleton", "Undead Guard", "The Reaper", "The Mole’s Dad")]
    Enemies,

    [SouvenirQuestion("Which was a number present in the grab phase of {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(10, 99)]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheArena", "Arena", typeof(SArena), "Hawker", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessArena(ModuleData module)
    {
        var comp = GetComponent(module, "TheArena");
        yield return WaitForSolve;

        var grabNums = GetArrayField<TextMesh>(comp, "GrbNums", isPublic: true).Get(expectedLength: 9).Select(textMesh => textMesh.text).ToArray();
        var enemyNames = GetField<TextMesh>(comp, "DefEnemies", isPublic: true).Get().text.Split('\n').ToArray();
        var maxNum = GetField<TextMesh>(comp, "AtkNum", isPublic: true).Get().text.Replace("[", "").Replace("]", "");

        yield return question(SArena.Damage).Answers(maxNum);
        yield return question(SArena.Enemies).Answers(enemyNames);
        yield return question(SArena.Numbers).Answers(grabNums);
    }
}