using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWolfGoatAndCabbage
{
    [Question("Which of these was {1} on {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "WolfGoatAndCabbageSprites", Arguments = ["present", "not present"], ArgumentGroupSize = 1, TranslateArguments = [true])]
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

        var animalsPresent = GetListField<string>(comp, "_startShore").Get().ToArray();

        yield return WaitForSolve;

        var presentAnimalSprites = animalsPresent.Select(txt => WolfGoatAndCabbageSprites.First(spr => spr.name == txt)).ToArray();

        foreach (var present in new[] { false, true })
            yield return question(SWolfGoatAndCabbage.Animals, args: [present ? "present" : "not present"]).Answers(present ? presentAnimalSprites : WolfGoatAndCabbageSprites.Except(presentAnimalSprites).ToArray(), all: WolfGoatAndCabbageSprites);
    }
}
