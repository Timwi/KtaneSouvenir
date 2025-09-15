using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSbemailSongs
{
    [SouvenirQuestion("What was the displayed song for stage {1} (hexadecimal) of {0}?", OneColumn4Answers, ExampleAnswers = ["Oh, who is the guy that…", "I'm gonna check my email all of the time…", "Checkin' my email, checkin' my email…", "I check the email once…", "Checkin' emails is like the best thing I do.", "I check, you check, we all check…", "I am going to check my email.", "I remember the time when I checked my email.", "I've carefully set aside this time…", "I'm totally checking my email…"], Arguments = ["01", "02"], ArgumentGroupSize = 1, TranslatableStrings = ["the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)"])]
    Songs
}

public partial class SouvenirModule
{
    [SouvenirHandler("sbemailsongs", "Sbemail Songs", typeof(SSbemailSongs), "ObjectsCountries")]
    private IEnumerator<SouvenirInstruction> ProcessSbemailSongs(ModuleData module)
    {
        var comp = GetComponent(module, "_sbemailsongs");
        const string moduleId = "sbemailsongs";

        var fldDisplayedSongNumbers = GetListField<int>(comp, "stages", isPublic: true);
        yield return WaitForActivate;
        yield return null; // Wait one frame to make sure the Display field has been set.

        var myDisplay = fldDisplayedSongNumbers.Get(minLength: 0, validator: d => d is < 1 or > 209 ? "expected range 1-209" : null);
        if (_sbemailSongsDisplays.Any() && myDisplay.Count != _sbemailSongsDisplays[0].Count)
            throw new AbandonModuleException("The number of stages in each ‘Display’ is inconsistent.");
        _sbemailSongsDisplays.Add(myDisplay);

        var totalNonIgnoredSbemailSongs = GetIntField(comp, "totalNonIgnored").Get();

        if (myDisplay.Count == 0 || totalNonIgnoredSbemailSongs == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        yield return WaitForUnignoredModules;
        module.SolveIndex = 1;

        var myIgnoredList = GetArrayField<string>(comp, "ignoredModules").Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));

        if (_sbemailSongsDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException("The number of displays did not match the number of Sbemail Songs modules.");

        var transcriptionsAbridged = GetArrayField<string>(comp, "transcriptionsAbridged").Get(expectedLength: 209);

        if (_moduleCounts[moduleId] == 1)
            addQuestions(module, myDisplay.Take(displayedStageCount).Select((digit, ix) => makeQuestion(Question.SbemailSongsSongs, module, formatArgs: new[] { (ix + 1).ToString("X2") }, correctAnswers: new[] { transcriptionsAbridged[digit - 1] }, preferredWrongAnswers: transcriptionsAbridged)));
        else
        {
            var uniqueStages = Enumerable.Range(1, displayedStageCount).Where(stage => _sbemailSongsDisplays.Count(display => display[stage - 1] == myDisplay[stage - 1]) == 1).Take(2).ToArray();
            if (uniqueStages.Length == 0 || displayedStageCount == 1)
                yield return legitimatelyNoQuestion(module, "There are not enough stages at which at least one of them had a unique displayed number.");

            var qs = new List<QandA>();
            for (var stage = 0; stage < displayedStageCount; stage++)
            {
                var uniqueStage = uniqueStages.FirstOrDefault(s => s != stage + 1);
                if (uniqueStage != 0)
                    qs.Add(makeQuestion(Question.SbemailSongsSongs, moduleId, 0,
                        formattedModuleName: string.Format(translateString(Question.SbemailSongsSongs,
                            "the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)"),
                            transcriptionsAbridged[myDisplay[uniqueStage - 1] - 1],
                            uniqueStage.ToString("X2")), formatArgs: new[] { (stage + 1).ToString("X2") },
                        correctAnswers: new[] { transcriptionsAbridged[myDisplay[stage] - 1] },
                        preferredWrongAnswers: transcriptionsAbridged));
            }
            addQuestions(module, qs);
        }
    }
}