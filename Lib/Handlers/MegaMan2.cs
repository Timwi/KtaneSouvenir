using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMegaMan2
{
    [SouvenirQuestion("Which master was shown in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    Master,
    
    [SouvenirQuestion("Which weapon was shown in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    Weapon
}

public partial class SouvenirModule
{
    [SouvenirHandler("megaMan2", "Mega Man 2", typeof(SMegaMan2), "Goofy")]
    private IEnumerator<SouvenirInstruction> ProcessMegaMan2(ModuleData module)
    {
        var comp = GetComponent(module, "Megaman2");

        // This array contains all of the robot masters, but only the first 8 are used by the current rule seed
        var robotMasters = GetArrayField<string>(comp, "robotMasters").Get().Take(8).ToArray();

        // Make sure to use only the sprites relevant to the current rule seed
        Sprite[] GetSprites(string fieldName) => GetArrayField<Texture2D>(comp, fieldName, isPublic: true).Get()
            .Where(tx => robotMasters.Contains(tx.name))
            .Select(Sprites.ToSprite)
            .ToArray();

        var applicableMasters = GetSprites("RobotMasters");
        var applicableWeapons = GetSprites("Weapons");
        var selectedMaster = GetIntField(comp, "selectedMaster").Get(min: 0, max: robotMasters.Length - 1);
        var selectedWeapon = GetIntField(comp, "selectedWeapon").Get(min: 0, max: robotMasters.Length - 1);

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.MegaMan2Master, module, correctAnswers: new[] { applicableMasters.First(spr => spr.name == robotMasters[selectedMaster]) }, preferredWrongAnswers: applicableMasters),
            makeQuestion(Question.MegaMan2Weapon, module, correctAnswers: new[] { applicableWeapons.First(spr => spr.name == robotMasters[selectedWeapon]) }, preferredWrongAnswers: applicableWeapons));
    }
}