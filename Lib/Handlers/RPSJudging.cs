using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SRPSJudging
{
    [Question("What was the {2} team’s gesture in the {1} round of {0}?", TwoColumns4Answers, "Dynamite", "Tornado", "Quicksand", "Pit", "Chain", "Gun", "Law", "Whip", "Sword", "Rock", "Death", "Wall", "Sun", "Camera", "Fire", "Chainsaw", "School", "Scissors", "Poison", "Cage", "Axe", "Peace", "Computer", "Castle", "Snake", "Blood", "Porcupine", "Vulture", "Monkey", "King", "Queen", "Prince", "Princess", "Police", "Woman", "Baby", "Man", "Home", "Train", "Car", "Noise", "Bicycle", "Tree", "Turnip", "Duck", "Wolf", "Cat", "Bird", "Fish", "Spider", "Cockroach", "Brain", "Community", "Cross", "Money", "Vampire", "Sponge", "Church", "Butter", "Book", "Paper", "Cloud", "Airplane", "Moon", "Grass", "Film", "Toilet", "Air", "Planet", "Guitar", "Bowl", "Cup", "Beer", "Rain", "Water", "TV", "Rainbow", "UFO", "Alien", "Prayer", "Mountain", "Satan", "Dragon", "Diamond", "Platinum", "Gold", "Devil", "Fence", "Video Game", "Math", "Robot", "Heart", "Electricity", "Lightning", "Medusa", "Power", "Laser", "Nuke", "Sky", "Tank", "Helicopter", Arguments = [QandA.Ordinal, "blue", QandA.Ordinal, "red"], ArgumentGroupSize = 2, TranslateArguments = [false, true], TranslateAnswers = true)]
    QGesture,

    [Discriminator("the RPS Judging where the {2} team’s gesture was {0} the {1} round", Arguments = ["Dynamite", QandA.Ordinal, "blue", "Tornado", QandA.Ordinal, "red"], ArgumentGroupSize = 3, TranslateArguments = [false, false, true])]
    DGesture,
}

public partial class SouvenirModule
{
    [Handler("RPSJudging", "RPS Judging", typeof(SRPSJudging), "Anonymous", IsBossModule = true)]
    [ManualQuestion("What were the gestures in each round?")]
    private IEnumerator<SouvenirInstruction> ProcessRPSJudging(ModuleData module)
    {
        var comp = GetComponent(module, "RPSJudgingScript");

        while (!_noUnignoredModulesLeft)
            yield return null;

        var leftDisplays = GetListField<int>(comp, "LeftDisplays").Get(minLength: 0, validator: v => v is < 0 or > 100 ? "Expected range [0, 100]" : null);
        var rightDisplays = GetListField<int>(comp, "RightDisplays").Get(expectedLength: leftDisplays.Count, validator: v => v is < 0 or > 100 ? "Expected range [0, 100]" : null);

        if (leftDisplays.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        var allGestures = SRPSJudging.QGesture.GetAnswers();

        for (var stage = 0; stage < leftDisplays.Count; stage++)
        {
            yield return new Discriminator(SRPSJudging.DGesture, $"blue-{stage}", leftDisplays[stage],
                args: [TranslateAnswer(SRPSJudging.QGesture, allGestures[leftDisplays[stage]]), Ordinal(stage + 1), "blue"]);
            yield return new Discriminator(SRPSJudging.DGesture, $"red-{stage}", rightDisplays[stage],
                args: [TranslateAnswer(SRPSJudging.QGesture, allGestures[rightDisplays[stage]]), Ordinal(stage + 1), "red"]);
            yield return question(SRPSJudging.QGesture, args: [Ordinal(stage + 1), "blue"]).AvoidDiscriminators($"blue-{stage}").Answers(allGestures[leftDisplays[stage]]);
            yield return question(SRPSJudging.QGesture, args: [Ordinal(stage + 1), "red"]).AvoidDiscriminators($"red-{stage}").Answers(allGestures[rightDisplays[stage]]);
        }
    }
}
