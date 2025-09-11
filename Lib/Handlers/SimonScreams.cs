using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonScreams
{
    [SouvenirQuestion("Which color flashed {1} in the final sequence in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Flashing,
    
    [SouvenirQuestion("In which stage(s) of {0} was “{1}” the applicable rule?", TwoColumns4Answers, "first", "second", "third", "first and second", "first and third", "second and third", "all of them", TranslateAnswers = true, TranslateFormatArgs = [true], Arguments = [            "a color flashed, then a color two away, then the first again",
                "a color flashed, then a color two away, then the one opposite that",
                "a color flashed, then a color two away, then the one opposite the first",
                "a color flashed, then an adjacent color, then the first again",
                "a color flashed, then another color, then the first",
                "a color flashed, then one adjacent, then the one opposite that",
                "a color flashed, then one adjacent, then the one opposite the first",
                "a color flashed, then the one opposite, then one adjacent to that",
                "a color flashed, then the one opposite, then one adjacent to the first",
                "a color flashed, then the one opposite, then the first again",
                "every color flashed at least once",
                "exactly one color flashed exactly twice",
                "exactly one color flashed more than once",
                "exactly two colors flashed exactly twice",
                "exactly two colors flashed more than once",
                "no color flashed exactly twice",
                "no color flashed more than once",
                "no two adjacent colors flashed in clockwise order",
                "no two adjacent colors flashed in counter-clockwise order",
                "no two colors two apart flashed in clockwise order",
                "no two colors two apart flashed in counter-clockwise order",
                "the colors flashing first and last are adjacent",
                "the colors flashing first and last are different and not adjacent",
                "the colors flashing first and last are the same",
                "the number of distinct colors that flashed is even",
                "the number of distinct colors that flashed is odd",
                "there are at least three colors that didn’t flash",
                "there are exactly two colors that didn’t flash",
                "there are two colors adjacent to each other that didn’t flash",
                "there are two colors opposite each other that didn’t flash",
                "there are two colors two away from each other that didn’t flash",
                "there is exactly one color that didn’t flash",
                "three adjacent colors did not flash",
                "three adjacent colors flashed in clockwise order",
                "three adjacent colors flashed in counter-clockwise order",
                "three colors, each two apart, flashed in clockwise order",
                "three colors, each two apart, flashed in counter-clockwise order",
                "two adjacent colors flashed in clockwise order",
                "two adjacent colors flashed in counter-clockwise order",
                "two colors two apart flashed in clockwise order",
                "two colors two apart flashed in counter-clockwise order"], ArgumentGroupSize = 1)]
    RuleSimple,
    
    [SouvenirQuestion("In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?", TwoColumns4Answers, "first", "second", "third", "first and second", "first and third", "second and third", "all of them", TranslateAnswers = true, TranslateFormatArgs = [true, true, true, true], Arguments = [            "at most one color", "Red", "Orange", "Yellow",
                "at least two colors", "Green", "Blue", "Purple"], ArgumentGroupSize = 4)]
    RuleComplex
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonScreamsModule", "Simon Screams", typeof(SSimonScreams), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonScreams(ModuleData module)
    {
        var comp = GetComponent(module, "SimonScreamsModule");
        yield return WaitForSolve;

        var seqs = GetArrayField<int[]>(comp, "_sequences").Get(expectedLength: 3);
        var stageIxs = GetArrayField<int>(comp, "_stageIxs").Get(expectedLength: 3);
        var rules = GetField<Array>(comp, "_rowCriteria").Get(ar => ar.Length != 6 ? "expected length 6" : null);
        var colorsRaw = GetField<Array>(comp, "_colors").Get(ar => ar.Length != 6 ? "expected length 6" : null);     // array of enum values
        var colors = colorsRaw.Cast<object>().Select(obj => obj.ToString()).ToArray();

        var qs = new List<QandA>();
        var lastSeq = seqs.Last();
        foreach (var i in stageIxs)     // Only ask about the flashing colors that were relevant in the big table
            qs.Add(makeQuestion(Question.SimonScreamsFlashing, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colors[lastSeq[i]] }));

        // First determine which rule applied in which stage
        var fldCheck = GetField<Func<int[], bool>>(rules.GetValue(0), "Check", isPublic: true);
        var fldRuleName = GetField<string>(rules.GetValue(0), "Name", isPublic: true);
        var fldSouvenirCode = GetField<int>(rules.GetValue(0), "SouvenirCode", isPublic: true);
        var stageRules = new int[seqs.Length];
        for (var i = 0; i < seqs.Length; i++)
        {
            stageRules[i] = rules.Cast<object>().IndexOf(rule => fldCheck.GetFrom(rule)(seqs[i]));
            if (stageRules[i] == -1)
                throw new AbandonModuleException($"Apparently none of the criteria applies to Stage {i + 1} ([{seqs[i].Select(ix => colors[ix]).JoinString(", ")}]).");
        }

        // Now set the questions
        // Skip the last rule because it’s the “otherwise” row
        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple" };
        for (var rule = 0; rule < rules.Length - 1; rule++)
        {
            var applicableStages = new List<string>();
            for (var stage = 0; stage < stageRules.Length; stage++)
                if (stageRules[stage] == rule)
                    applicableStages.Add(Ordinal(stage + 1));
            if (applicableStages.Count > 0)
            {
                var code = fldSouvenirCode.GetFrom(rules.GetValue(rule));
                if (code == 0)
                    qs.Add(makeQuestion(Question.SimonScreamsRuleSimple, module,
                        formatArgs: new[] { fldRuleName.GetFrom(rules.GetValue(rule)) },
                        correctAnswers: new[] { applicableStages.Count == stageRules.Length ? "all of them" : applicableStages.JoinString(", ", lastSeparator: " and ") }));
                else
                {
                    var color1 = colorNames[code >> 7];
                    var color2 = colorNames[(code >> 4) & 7];
                    var color3 = colorNames[(code >> 1) & 7];
                    qs.Add(makeQuestion(Question.SimonScreamsRuleComplex, module,
                        formatArgs: new[] { (code & 1) != 0 ? "at least two colors" : "at most one color", color1, color2, color3 },
                        correctAnswers: new[] { applicableStages.Count == stageRules.Length ? "all of them" : applicableStages.JoinString(", ", lastSeparator: " and ") }));
                }
            }
        }

        addQuestions(module, qs);
    }
}