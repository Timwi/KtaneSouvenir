using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSbemailSongs
{
    [SouvenirQuestion("What was the displayed song for stage {1} (hexadecimal) of {0}?", OneColumn4Answers, ExampleAnswers = ["Oh, who is the guy that…", "I'm gonna check my email all of the time…", "Checkin' my email, checkin' my email…", "I check the email once…", "Checkin' emails is like the best thing I do.", "I check, you check, we all check…", "I am going to check my email.", "I remember the time when I checked my email.", "I've carefully set aside this time…", "I'm totally checking my email…"], Arguments = ["01", "02"], ArgumentGroupSize = 1)]
    Songs,

    [SouvenirDiscriminator("the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)", Arguments = ["Oh, who is the guy that…", "01", "I'm gonna check my email all of the time…", "02", "Checkin' my email, checkin' my email…", "0A"], ArgumentGroupSize = 2)]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("sbemailsongs", "Sbemail Songs", typeof(SSbemailSongs), "ObjectsCountries", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessSbemailSongs(ModuleData module)
    {
        var comp = GetComponent(module, "_sbemailsongs");

        var fldDisplayedSongNumbers = GetListField<int>(comp, "stages", isPublic: true);
        yield return WaitForActivate;
        yield return null; // Wait one frame to make sure the Display field has been set.

        var myDisplay = fldDisplayedSongNumbers.Get(minLength: 0, validator: d => d is < 1 or > 209 ? "expected range 1-209" : null);
        var totalNonIgnoredSbemailSongs = GetIntField(comp, "totalNonIgnored").Get();

        if (myDisplay.Count == 0 || totalNonIgnoredSbemailSongs == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        yield return WaitForUnignoredModules;

        var myIgnoredList = GetArrayField<string>(comp, "ignoredModules").Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));
        var transcriptionsAbridged = GetArrayField<string>(comp, "transcriptionsAbridged").Get(expectedLength: 209);

        for (var stage = 0; stage < myDisplay.Count && stage < displayedStageCount; stage++)
        {
            yield return new Discriminator(SSbemailSongs.Digits, $"digit{stage}", myDisplay[stage], [transcriptionsAbridged[myDisplay[stage] - 1], (stage + 1).ToString("X2")]);
            yield return question(SSbemailSongs.Songs, args: [(stage + 1).ToString("X2")])
                .AvoidDiscriminators($"digit{stage}")
                .Answers(transcriptionsAbridged[myDisplay[stage] - 1], preferredWrong: transcriptionsAbridged);
        }
    }
}
