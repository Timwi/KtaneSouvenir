using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SFaerieFires
{
    [SouvenirQuestion("What color was the {1} faerie in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "Cyan", "Magenta", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Color,

    [SouvenirQuestion("What pitch did the {1} faerie sing in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "FaerieFiresAudio", AudioSizeMultiplier = 8, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    PitchOrdinal,

    [SouvenirQuestion("What pitch did the {1} faerie sing in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "FaerieFiresAudio", AudioSizeMultiplier = 8, Arguments = ["red", "green", "blue", "yellow", "cyan", "magenta"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    PitchColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("FaerieFiresModule", "Faerie Fires", typeof(SFaerieFires), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessFaerieFires(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "FaerieFiresScript");
        var fires = GetField<IList>(comp, "FaerieFires").Get(v => v.Count is not 6 ? "Expected 6 fires" : v.Cast<object>().Any(o => o is null) ? "Unexpected null fire" : null);
        var fldOrder = GetIntField(fires[0], "Order", true);
        var fldSound = GetField<string>(fires[0], "Sound", true);
        var fldName = GetField<string>(fires[0], "Name", true);

        var faeries = fires.Cast<object>().Select(f => (
            Order: fldOrder.GetFrom(f, 0, 5),
            Sound: fldSound.GetFrom(f, v => !Regex.IsMatch(v, "^FaerieGlitter[1-6]$") ? "Expected sound to match “^FaerieGlitter[1-6]$”" : null),
            Name: fldName.GetFrom(f, v => !SFaerieFires.Color.GetAnswers().Contains(v) ? "Unexpected color name" : null)));

        foreach (var f in faeries)
        {
            yield return question(SFaerieFires.PitchOrdinal, args: [Ordinal(f.Order + 1)]).Answers(FaerieFiresAudio[f.Sound.Last() - '1']);
            yield return question(SFaerieFires.PitchColor, args: [f.Name]).Answers(FaerieFiresAudio[f.Sound.Last() - '1']);
            yield return question(SFaerieFires.Color, args: [Ordinal(f.Order + 1)]).Answers(f.Name);
        }
    }
}
