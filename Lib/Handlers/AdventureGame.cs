using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAdventureGame
{
    [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", TwoColumns4Answers, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QCorrectItem,

    [SouvenirQuestion("What enemy were you fighting in {0}?", TwoColumns4Answers, "Dragon", "Demon", "Eagle", "Goblin", "Troll", "Wizard", "Golem", "Lizard")]
    QEnemy,

    [SouvenirDiscriminator("the Adventure Game where the {0} was used", Arguments = ["Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy"], ArgumentGroupSize = 1)]
    DCorrectItem,

    [SouvenirDiscriminator("the Adventure Game where the enemy was {0}", Arguments = ["Dragon", "Demon", "Eagle", "Goblin", "Troll", "Wizard", "Golem", "Lizard"], ArgumentGroupSize = 1)]
    DEnemy
}

public partial class SouvenirModule
{
    [SouvenirHandler("spwizAdventureGame", "Adventure Game", typeof(SAdventureGame), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessAdventureGame(ModuleData module)
    {
        var comp = GetComponent(module, "AdventureGameModule");
        var fldInvWeaponCount = GetIntField(comp, "InvWeaponCount");
        var fldSelectedItem = GetIntField(comp, "SelectedItem");
        var mthItemName = GetMethod<string>(comp, "ItemName", 1);
        var mthShouldUseItem = GetMethod<bool>(comp, "ShouldUseItem", 1);

        yield return WaitForActivate;

        var invValues = GetField<IList>(comp, "InvValues").Get();   // actually List<AdventureGameModule.ITEM>
        var buttonUse = GetField<KMSelectable>(comp, "ButtonUse", isPublic: true).Get(b => b.OnInteract == null ? "ButtonUse.OnInteract is null" : null);
        var textEnemy = GetField<TextMesh>(comp, "TextEnemy", isPublic: true).Get();
        var invWeaponCount = fldInvWeaponCount.Get(v => v == 0 ? "zero" : null);

        var enemy = GetField<object>(comp, "SelectedEnemy").Get();
        var enemyName = titleCase(enemy.ToString());
        yield return question(SAdventureGame.QEnemy).AvoidDiscriminators(SAdventureGame.DEnemy).Answers(enemyName);
        yield return new Discriminator(SAdventureGame.DEnemy, "enemy", enemy, args: [enemyName]);

        var prevInteract = buttonUse.OnInteract;
        var origInvValues = new List<int>(invValues.Cast<int>());
        var correctItemsUsed = 0;
        var qs = new List<QandAStump>();
        var ds = new List<Discriminator>();

        buttonUse.OnInteract = delegate
        {
            var selectedItem = fldSelectedItem.Get();
            var itemUsed = origInvValues[selectedItem];
            var shouldUse = mthShouldUseItem.Invoke(selectedItem);
            for (var j = invWeaponCount; j < invValues.Count; j++)
                shouldUse &= !mthShouldUseItem.Invoke(j);

            var ret = prevInteract();

            if (invValues.Count != origInvValues.Count)
            {
                // If the length of the inventory has changed, the user used a correct non-weapon item.
                var itemIndex = ++correctItemsUsed;
                var itemName = titleCase(mthItemName.Invoke(itemUsed));
                qs.Add(question(SAdventureGame.QCorrectItem, args: [Ordinal(itemIndex)]).AvoidDiscriminators(SAdventureGame.DCorrectItem).Answers(itemName));
                ds.Add(new(SAdventureGame.DCorrectItem, $"item-{itemUsed}", args: [itemName]));
                origInvValues.Clear();
                origInvValues.AddRange(invValues.Cast<int>());
            }
            else if (shouldUse)
            {
                // The user solved the module.
                textEnemy.text = "Victory!";
            }

            return ret;
        };

        yield return WaitForSolve;

        buttonUse.OnInteract = prevInteract;
        foreach (var q in qs)
            yield return q;
        foreach (var d in ds)
            yield return d;
    }
}
