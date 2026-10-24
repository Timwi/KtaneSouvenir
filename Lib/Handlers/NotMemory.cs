using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotMemory
{
    [Question("How many LEDs were lit in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 5)]
    QLitLeds,

    [Question("Which digit was on the upside-down display in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(1, 4)]
    QDisplayedDigit,

    [Discriminator("the Not Memory with {0}", Arguments = ["no lit LEDs", "one lit LED", "two lit LEDs", "three lit LEDs", "four lit LEDs", "five lit LEDs"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DLitLeds
}

public partial class SouvenirModule
{
    [Handler("NotMemory", "Not Memory", typeof(SNotMemory), "Timwi")]
    [ManualQuestion("What digit was on the upside-down display?")]
    [ManualQuestion("How many LEDs were lit?")]
    private IEnumerator<SouvenirInstruction> ProcessNotMemory(ModuleData module)
    {
        var comp = GetComponent(module, "NotMemory");
        var propLightCount = GetProperty<int>(comp, "LightCount", isPublic: true);
        var propDisplay = GetProperty<int>(comp, "Display", isPublic: true);
        var propConnector = GetProperty<object>(comp, "Connector");

        yield return WaitForSolve;

        var lightCount = propLightCount.Get(validator: val => val < 0 || val > 5 ? "LightCount out of range (expected 0–5)" : null);
        var displayedDigit = propDisplay.Get(validator: val => val < 1 || val > 4 ? "Display digit out of range (expected 1–4)" : null);

        var connector = propConnector.Get();
        var propDisplayText = GetProperty<string>(connector, "DisplayText", isPublic: true);
        var propStage = GetProperty<int>(connector, "Stage", isPublic: true);

        yield return question(SNotMemory.QLitLeds).AvoidDiscriminators(SNotMemory.DLitLeds).Answers(lightCount.ToString());

        // This discriminator can only happen if the displayed-digit question (below) is asked
        string[] litNames = ["no lit LEDs", "one lit LED", "two lit LEDs", "three lit LEDs", "four lit LEDs", "five lit LEDs"];
        yield return new Discriminator(SNotMemory.DLitLeds, "lit", lightCount, args: [litNames[lightCount]]);

        if ((lightCount != 1 || Bomb.GetBatteryCount() != 0) &&
            (lightCount != 3 || !Bomb.IsPortPresent(Port.Parallel)) &&
            (lightCount != 5 || Bomb.GetPortCount() != 0))
        {
            // We can only ask this question if the displayed digit was relevant,
            // which means that it also can’t have a discriminator
            yield return question(SNotMemory.QDisplayedDigit).Answers(displayedDigit.ToString());
        }

        propDisplayText.Set("");
        propStage.Set(0);
    }
}
