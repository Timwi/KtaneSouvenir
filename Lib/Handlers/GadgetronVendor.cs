using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGadgetronVendor
{
    [SouvenirQuestion("What was your current weapon in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "GadgetronVendorIconSprites")]
    CurrentWeapon,

    [SouvenirQuestion("What was the weapon up for sale in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "GadgetronVendorWeaponSprites")]
    WeaponForSale
}

public partial class SouvenirModule
{
    [SouvenirHandler("lgndGadgetronVendor", "Gadgetron Vendor", typeof(SGadgetronVendor), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessGadgetronVendor(ModuleData module)
    {
        var comp = GetComponent(module, "GadgetronVendorScript");

        yield return WaitForSolve;

        GetField<TextMesh>(comp, "currentAmmo", isPublic: true).Get().text = "";
        GetField<TextMesh>(comp, "maxAmmo", isPublic: true).Get().text = "";
        GetField<SpriteRenderer>(comp, "yourWeaponIcon", isPublic: true).Get().enabled = false;

        var currentWeaponIndex = GetIntField(comp, "yourWeaponIndex").Get(min: 0, max: GadgetronVendorIconSprites.Length);
        var saleWeaponIndex = GetIntField(comp, "saleWeaponIndex").Get(min: 0, max: GadgetronVendorWeaponSprites.Length);
        addQuestions(
            module,
            makeQuestion(Question.GadgetronVendorCurrentWeapon, module, correctAnswers: new[] { GadgetronVendorIconSprites[currentWeaponIndex] }, preferredWrongAnswers: GadgetronVendorIconSprites),
            makeQuestion(Question.GadgetronVendorWeaponForSale, module, correctAnswers: new[] { GadgetronVendorWeaponSprites[saleWeaponIndex] }, preferredWrongAnswers: GadgetronVendorWeaponSprites)
        );
    }
}