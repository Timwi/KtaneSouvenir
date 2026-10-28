using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAllmightySinnoh
{
    [Question("Which plate was initially marked by {1} in {0}?", ThreeColumns6Answers, "Blank", "Fist", "Sky", "Toxic", "Earth", "Stone", "Insect", "Spooky", "Flame", "Splash", "Iron", "Meadow", "Zap", "Mind", "Icicle", "Draco", "Dread", "Pixie", TranslateAnswers = true, Arguments = ["time", "space", "antimatter"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Marks,

    [Discriminator("the Allmighty Sinnoh where the {0} Plate was initially marked by {1}", ArgumentGroupSize = 2, Arguments = ["Blank", "time", "Fist", "time", "Sky", "time", "Toxic", "time", "Earth", "space", "Stone", "space", "Insect", "space", "Spooky", "space", "Flame", "space", "Splash", "space", "Iron", "antimatter", "Meadow", "antimatter", "Zap", "antimatter", "Mind", "antimatter", "Icicle", "antimatter", "Draco", "antimatter", "Dread", "antimatter", "Pixie", "antimatter"])]
    MarksDiscriminator
}

public partial class SouvenirModule
{
    [Handler("AllmightySinnoh", "Allmighty Sinnoh", typeof(SAllmightySinnoh), "thunder725")]
    [ManualQuestion("Which plates were initially marked?")]
    private IEnumerator<SouvenirInstruction> ProcessAllmightySinnoh(ModuleData module)
    {
        var comp = GetComponent(module, "AllmightySinnoh");
        var plateNames = GetArrayField<string>(comp, "plateNames").Get(expectedLength: 18);

        yield return WaitForSolve;

        string timeMark = plateNames[GetIntField(comp, "initialTimeMark").Get(min: 0, max: 17)];
        string spaceMark = plateNames[GetIntField(comp, "initialSpaceMark").Get(min: 0, max: 17)];
        string antimatterMark = plateNames[GetIntField(comp, "initialAntimaterMark").Get(min: 0, max: 17)]; // typo in original script

        yield return question(SAllmightySinnoh.Marks, args: ["time"])
            .AvoidDiscriminators(["markTime"])
            .Answers(timeMark, all: plateNames);

        yield return question(SAllmightySinnoh.Marks, args: ["space"])
            .AvoidDiscriminators(["markSpace"])
            .Answers(spaceMark, all: plateNames);

        yield return question(SAllmightySinnoh.Marks, args: ["antimatter"])
            .AvoidDiscriminators(["markAntimatter"])
            .Answers(antimatterMark, all: plateNames);

        yield return new Discriminator(SAllmightySinnoh.MarksDiscriminator, "markTime", timeMark, args: [timeMark, "time"]);
        yield return new Discriminator(SAllmightySinnoh.MarksDiscriminator, "markSpace", spaceMark, args: [spaceMark, "space"]);
        yield return new Discriminator(SAllmightySinnoh.MarksDiscriminator, "markAntimatter", antimatterMark, args: [antimatterMark, "antimatter"]);
    }
}
