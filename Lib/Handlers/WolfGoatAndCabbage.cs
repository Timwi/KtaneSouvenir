using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SWolfGoatAndCabbage
{
    [Question("Which of these was {1} on {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, Arguments = ["present", "not present"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Animals
}

public partial class SouvenirModule
{
    [Handler("wolfGoatCabbageModule", "Wolf, Goat, and Cabbage", typeof(SWolfGoatAndCabbage), "Marksam")]
    [ManualQuestion("Which creatures were present?")]
    private IEnumerator<SouvenirInstruction> ProcessWolfGoatAndCabbage(ModuleData module)
    {
        var comp = GetComponent(module, "WolfGoatCabbageScript");

        yield return null;

        var pixelsPerUnit = new Dictionary<string, int>
        {
            ["Goose 1"] = 1700,
            ["Grass 1"] = 5000,
            ["Rabbit 1"] = 5000,
            ["Seeds 1"] = 5000
        };
        var sprites = GetArrayField<GameObject>(comp, "Images", isPublic: true).Get(expectedLength: 22)
            .Select(obj => obj.GetComponent<SpriteRenderer>().sprite)
            .Select(sprite => sprite.name.EndsWith(" 1") ? sprite : throw new AbandonModuleException("expected sprite names to end with “ 1”"))
            .Select(sprite => sprite.TranslateSprite(pixelsPerUnit.Get(sprite.name, 4700), sprite.name.Replace(" 1", "")))
            .ToArray();

        var animalsPresent = GetListField<string>(comp, "_startShore").Get().ToArray();

        yield return WaitForSolve;

        var presentAnimalSprites = animalsPresent.Select(txt => sprites.First(spr => spr.name == txt)).ToArray();

        foreach (var present in new[] { false, true })
            yield return question(SWolfGoatAndCabbage.Animals, args: [present ? "present" : "not present"]).Answers(present ? presentAnimalSprites : sprites.Except(presentAnimalSprites).ToArray(), all: sprites);
    }
}
