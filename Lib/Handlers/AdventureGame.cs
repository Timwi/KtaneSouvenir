using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAdventureGame
{
    [SouvenirQuestion("Which item was present in {0}?", TwoColumns4Answers, "Balloon", "Battery", "Bellows", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy")]
    QPresentItem,

    [SouvenirDiscriminator("the Adventure Game where the {0} was present", Arguments = ["Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy"], ArgumentGroupSize = 1)]
    DPresentItem
}

public partial class SouvenirModule
{
    [SouvenirHandler("spwizAdventureGame", "Adventure Game", typeof(SAdventureGame), "Quinn Wuest")]
    [SouvenirManualQuestion("Which items were present?")]
    private IEnumerator<SouvenirInstruction> ProcessAdventureGame(ModuleData module)
    {
        var comp = GetComponent(module, "AdventureGameModule");
        var fldInvWeaponCount = GetIntField(comp, "InvWeaponCount");
        var fldSelectedItem = GetIntField(comp, "SelectedItem");
        var mthItemName = GetMethod<string>(comp, "ItemName", 1);
        var mthShouldUseItem = GetMethod<bool>(comp, "ShouldUseItem", 1);

        yield return WaitForActivate;

        string[] itemNames = ["Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy"];
        int[] unimportantObjs = [3, 9];

        var invValues = GetField<IList>(comp, "InvValues").Get().Cast<int>().ToArray();   // actually List<AdventureGameModule.ITEM>
        var items = invValues.Skip(3).Select(i => i - 6).Except(unimportantObjs).ToArray();

        foreach (var item in items)
            yield return new Discriminator(SAdventureGame.DPresentItem, $"item-{item}", args: [itemNames[item]], avoidAnswers: [itemNames[item]]);
        yield return question(SAdventureGame.QPresentItem).Answers(items.Select(i => itemNames[i]).ToArray());

        yield return WaitForSolve;

        KMSelectable[] buttons = [
            GetField<KMSelectable>(comp, "ButtonStatLeft", isPublic: true).Get(),
            GetField<KMSelectable>(comp, "ButtonStatRight", isPublic: true).Get(),
            GetField<KMSelectable>(comp, "ButtonInvLeft", isPublic: true).Get(),
            GetField<KMSelectable>(comp, "ButtonInvRight", isPublic: true).Get(),
            GetField<KMSelectable>(comp, "ButtonUse", isPublic: true).Get()
        ];

        for (var i = 0; i < buttons.Length; i++)
        {
            var j = i;
            buttons[i].OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
                buttons[j].AddInteractionPunch(j < 4 ? 0.25f : 1f);
                return false;
            };
        }
    }
}
