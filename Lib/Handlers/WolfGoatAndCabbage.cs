using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWolfGoatAndCabbage
{
    [Question("Which of these was {1} on {0}?", ThreeColumns6Answers, "Cat", "Wolf", "Rabbit", "Berry", "Fish", "Dog", "Duck", "Goat", "Fox", "Grass", "Rice", "Mouse", "Bear", "Cabbage", "Chicken", "Goose", "Corn", "Carrot", "Horse", "Earthworm", "Kiwi", "Seeds", Arguments = ["present", "not present"], ArgumentGroupSize = 1, TranslateArguments = [true], TranslateAnswers = true)]
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

        var allAnimals = SWolfGoatAndCabbage.Animals.GetAnswers();
        foreach (var present in new[] { false, true })
            yield return question(SWolfGoatAndCabbage.Animals, args: [present ? "present" : "not present"]).Answers(present ? animalsPresent : allAnimals.Except(animalsPresent).ToArray(), preferredWrong: present ? allAnimals : animalsPresent);
    }
}
