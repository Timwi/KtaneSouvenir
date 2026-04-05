using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGadgetronVendor
{
    [Question("What was your current weapon in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    CurrentWeapon,

    [Question("What was the weapon up for sale in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    WeaponForSale
}

public partial class SouvenirModule
{
    [Handler("lgndGadgetronVendor", "Gadgetron Vendor", typeof(SGadgetronVendor), "Kuro")]
    [ManualQuestion("What were the displayed weapons?")]
    private IEnumerator<SouvenirInstruction> ProcessGadgetronVendor(ModuleData module)
    {
        var comp = GetComponent(module, "GadgetronVendorScript");

        var iconSprites = GetArrayField<Sprite>(comp, "iconList", isPublic: true).Get(expectedLength: 15)
            .TranslateSprites(200f).ToArray();

        var weaponsRaw = GetArrayField<Sprite>(comp, "weaponImageList", isPublic: true).Get(expectedLength: 16, nullContentAllowed: true);
        if (weaponsRaw.Select((ws, ix) => (ws == null) != (ix == 15)).Any(b => b))
            throw new AbandonModuleException($"Expected ‘weaponImageList’ to have 15 sprites and one null value.");
        var weaponSprites = weaponsRaw.Take(15).TranslateSprites(1200f, new Vector2(.25f, .5f)).ToArray();

        yield return WaitForSolve;

        GetField<TextMesh>(comp, "currentAmmo", isPublic: true).Get().text = "";
        GetField<TextMesh>(comp, "maxAmmo", isPublic: true).Get().text = "";
        GetField<SpriteRenderer>(comp, "yourWeaponIcon", isPublic: true).Get().enabled = false;

        var currentWeaponIndex = GetIntField(comp, "yourWeaponIndex").Get(min: 0, max: iconSprites.Length);
        var saleWeaponIndex = GetIntField(comp, "saleWeaponIndex").Get(min: 0, max: weaponSprites.Length);
        yield return question(SGadgetronVendor.CurrentWeapon).Answers(iconSprites[currentWeaponIndex], all: iconSprites);
        yield return question(SGadgetronVendor.WeaponForSale).Answers(weaponSprites[saleWeaponIndex], all: weaponSprites);
    }
}
