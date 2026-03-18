using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWire
{
    [SouvenirQuestion("What was the color of the {1} dial in {0}?", ThreeColumns6Answers, "blue", "green", "grey", "orange", "purple", "red", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["top", "bottom-left", "bottom-right"], ArgumentGroupSize = 1)]
    QDialColors,

    [SouvenirQuestion("What was the displayed number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    QDisplayedNumber,

    [SouvenirDiscriminator("the Wire whose {0} dial was {1}", Arguments = ["top", "blue", "bottom-left", "green", "bottom-right", "grey", "top", "orange", "bottom-left", "purple", "bottom-right", "red"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    DDialColors,

    [SouvenirDiscriminator("the Wire whose displayed number was {0}", Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    DDisplayedNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("wire", "Wire", typeof(SWire), "Timwi", AddThe = true)]
    [SouvenirManualQuestion("What were the colors of the dials?")]
    [SouvenirManualQuestion("What was the displayed number?")]
    private IEnumerator<SouvenirInstruction> ProcessWire(ModuleData module)
    {
        var comp = GetComponent(module, "wireScript");
        yield return WaitForSolve;

        var dials = GetArrayField<Renderer>(comp, "renderers", isPublic: true).Get(expectedLength: 3);
        var dialColors = dials.Select(dial => dial.material.mainTexture.name.Replace("Mat", "")).ToArray();
        var displayedNumber = GetIntField(comp, "displayedNumber").Get().ToString();
        var dialNames = new[] { "top", "bottom-left", "bottom-right" };

        // Questions
        for (var dial = 0; dial < 3; dial++)
            yield return question(SWire.QDialColors, args: [dialNames[dial]]).Answers(dialColors[dial]);
        yield return question(SWire.QDisplayedNumber).Answers(displayedNumber);

        // Discriminators
        for (var dial = 0; dial < 3; dial++)
            yield return new Discriminator(SWire.DDialColors, $"dial-{dial}", dialColors[dial], args: [dialNames[dial], dialColors[dial]]);
        yield return new Discriminator(SWire.DDisplayedNumber, "num", displayedNumber, args: [displayedNumber.ToString()]);
    }
}
