using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWolfGoatAndCabbage
{
    [SouvenirQuestion("Which of these was {1} on {0}?", ThreeColumns6Answers, "Cat", "Wolf", "Rabbit", "Berry", "Fish", "Dog", "Duck", "Goat", "Fox", "Grass", "Rice", "Mouse", "Bear", "Cabbage", "Chicken", "Goose", "Corn", "Carrot", "Horse", "Earthworm", "Kiwi", "Seeds", Arguments = ["present", "not present"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Animals,

    [SouvenirQuestion("What was the boat size in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    BoatSize
}

public partial class SouvenirModule
{
    [SouvenirHandler("wolfGoatCabbageModule", "Wolf, Goat, and Cabbage", typeof(SWolfGoatAndCabbage), "Marksam")]
    private IEnumerator<SouvenirInstruction> ProcessWolfGoatAndCabbage(ModuleData module)
    {
        var comp = GetComponent(module, "WolfGoatCabbageScript");

        yield return null;

        var animalsPresent = GetListField<string>(comp, "_startShore").Get().ToArray();

        yield return WaitForSolve;

        var boatSize = GetIntField(comp, "_boatSize").Get();
        var allAnimals = Question.WolfGoatAndCabbageAnimals.GetAnswers();
        foreach (var present in new[] { false, true })
        {
            yield return question(SWolfGoatAndCabbage.Animals, args: [present ? "present" : "not present"]).Answers(present ? animalsPresent : allAnimals.Except(animalsPresent).ToArray(), preferredWrong: present ? allAnimals : animalsPresent);
        }
        yield return question(SWolfGoatAndCabbage.BoatSize, args: null).Answers(boatSize.ToString());
    }
}