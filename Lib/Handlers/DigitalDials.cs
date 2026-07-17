using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SDigitalDials
{
    [Question("What number was on the {1} display when the dials were in their initial calculated positions in {0}?", ThreeColumns6Answers, Arguments = ["left", "middle", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(0, 99, "00")]
    QNumber,

    [Question("What number was on the large display in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 999, "000")]
    QLargeDisplay,

    [Discriminator("the Digital Dials where {0} was on the {1} display when the dials were in their initial calculated positions", Arguments = ["47", "left", "82", "middle", "69", "right"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DNumber,

    [Discriminator("the Digital Dials where {0} was on the large display", Arguments = ["47", "82", "69"], ArgumentGroupSize = 1)]
    DLargeDisplay
}

public partial class SouvenirModule
{
    [Handler("digitalDials", "Digital Dials", typeof(SDigitalDials), "Timwi")]
    [ManualQuestion("What numbers were on the display when the dials were in their initial calculated positions?")]
    private IEnumerator<SouvenirInstruction> ProcessDigitalDials(ModuleData module)
    {
        var comp = GetComponent(module, "digitalDials");
        var mainScreen = GetField<TextMesh>(comp, "mainScreen", isPublic: true).Get().text;
        var dialValues = GetArrayField<int[]>(comp, "DialVals").Get(expectedLength: 3, validator: v => v.Length != 10 ? "expected inner array of length 10" : null);

        if (!mainScreen.RegexMatch(@"^\d\d\d$", out _))
            throw new AbandonModuleException($"Expected three digits on the main screen, got {mainScreen}");

        // Calculate the “initial calculated positions” of the dials because those are not stored in fields
        var serialNumberDigits = Bomb.GetSerialNumberNumbers().JoinString();
        var ans =
            (serialNumberDigits[0] - '0' + mainScreen[0] - '0') *
            (serialNumberDigits[1] - '0' + mainScreen[1] - '0') *
            ((serialNumberDigits.Length == 2 ? Bomb.GetPortCount() : serialNumberDigits[2] - '0') + mainScreen[2] - '0') % 1000;
        int[] initialDialPositions = [ans / 100, ans / 10 % 10, ans % 10];

        yield return WaitForSolve;

        var posNames = new[] { "left", "middle", "right" };
        for (var dial = 0; dial < 3; dial++)
        {
            yield return new Discriminator(SDigitalDials.DNumber, $"num-{dial}", dialValues[dial][initialDialPositions[dial]], args: [dialValues[dial][initialDialPositions[dial]].ToString("00"), posNames[dial]]);
            yield return question(SDigitalDials.QNumber, args: [posNames[dial]]).AvoidDiscriminators($"num-{dial}").Answers(dialValues[dial][initialDialPositions[dial]].ToString("00"));
        }
        yield return new Discriminator(SDigitalDials.DLargeDisplay, "large", mainScreen, args: [mainScreen]);
        yield return question(SDigitalDials.QLargeDisplay).AvoidDiscriminators("large").Answers(mainScreen);
    }
}
