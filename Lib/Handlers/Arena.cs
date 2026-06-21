using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SArena
{
    [Question("What was the maximum weapon damage of the attack phase in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 99)]
    QDamage,

    [Question("Which enemy was present in the defend phase of {0}?", TwoColumns4Answers, "Bat", "Snake", "Spider", "Cobra", "Scorpion", "Mole", "Creeper", "Goblin", "Golem", "Robo-Mouse", "Skeleton", "Undead Guard", "The Reaper", "The Mole’s Dad")]
    QEnemies,

    [Question("Which was a number present in the grab phase of {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(10, 99)]
    QNumbers,

    [Discriminator("the Arena where the maximum weapon damage of the attack phase was {0}", Arguments = ["1", "2", "99"], ArgumentGroupSize = 1)]
    DDamage,

    [Discriminator("the Arena which had {0} in the defend phase", Arguments = ["Bat", "Snake", "Spider", "Cobra", "Scorpion", "Mole", "Creeper", "Goblin", "Golem", "Robo-Mouse", "Skeleton", "Undead Guard", "The Reaper", "The Mole’s Dad"], ArgumentGroupSize = 1)]
    DEnemies,

    [Discriminator("the Arena which had a {0} in the grab phase", Arguments = ["10", "47", "99"], ArgumentGroupSize = 1)]
    DNumbers
}

public partial class SouvenirModule
{
    [Handler("TheArena", "Arena", typeof(SArena), "Hawker", AddThe = true)]
    [ManualQuestion("What was the maximum weapon damage of the attack phase?")]
    [ManualQuestion("What enemies were present in the defend phase?")]
    [ManualQuestion("What numbers were present in the grab phase?")]
    private IEnumerator<SouvenirInstruction> ProcessArena(ModuleData module)
    {
        var comp = GetComponent(module, "TheArena");
        yield return WaitForSolve;

        var grabNums = GetArrayField<TextMesh>(comp, "GrbNums", isPublic: true).Get(expectedLength: 9).Select(textMesh => textMesh.text).ToArray();
        var enemyNames = GetField<TextMesh>(comp, "DefEnemies", isPublic: true).Get().text.Split('\n').ToArray();
        var maxNum = GetField<TextMesh>(comp, "AtkNum", isPublic: true).Get().text.Replace("[", "").Replace("]", "");

        yield return question(SArena.QDamage).AvoidDiscriminators(SArena.DDamage).Answers(maxNum);
        yield return question(SArena.QEnemies).AvoidDiscriminators(SArena.DEnemies).Answers(enemyNames);
        yield return question(SArena.QNumbers).AvoidDiscriminators(SArena.DNumbers).Answers(grabNums);

        yield return new Discriminator(SArena.DDamage, "damage", maxNum, args: [maxNum]);
        foreach (var en in enemyNames)
            yield return new Discriminator(SArena.DEnemies, $"en-{en}", args: [en]);
        foreach (var num in grabNums)
            yield return new Discriminator(SArena.DNumbers, $"num-{num}", args: [num]);
    }
}
