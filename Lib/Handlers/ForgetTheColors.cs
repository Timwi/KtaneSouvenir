using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SForgetTheColors
{
    [SouvenirQuestion("What color was the gear during stage {1} of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray", TranslateAnswers = true, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    GearColor,

    [SouvenirQuestion("What number was on the gear during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1, TranslatableStrings = [            "the Forget The Colors whose gear number was {0} in stage {1}",
                "the Forget The Colors which had {0} on its large display in stage {1}",
                "the Forget The Colors whose received sine number in stage {1} ended with a {0}",
                "the Forget The Colors whose gear color was {0} in stage {1}",
                "the Forget The Colors whose rule color was {0} in stage {1}"])]
    [AnswerGenerator.Integers(0, 9)]
    GearNumber,

    [SouvenirQuestion("Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray", TranslateAnswers = true, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    RuleColor,

    [SouvenirQuestion("What number was on the large display during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 990)]
    LargeDisplay,

    [SouvenirQuestion("What was the last decimal in the sine number received during stage {1} of {0}?", ThreeColumns6Answers, Arguments = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    SineNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("ForgetTheColors", "Forget The Colors", typeof(SForgetTheColors), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessForgetTheColors(ModuleData module)
    {
        var comp = GetComponent(module, "FTCScript");
        const string moduleId = "ForgetTheColors";

        var myGearNumbers = GetListField<byte>(comp, "gear").Get();
        var myLargeDisplays = GetListField<short>(comp, "largeDisplay").Get();
        var mySineNumbers = GetListField<int>(comp, "sineNumber").Get();
        var myGearColors = GetListField<string>(comp, "gearColor").Get();
        var myRuleColors = GetListField<string>(comp, "ruleColor").Get();

        _ftcGearNumbers.Add(myGearNumbers);
        _ftcLargeDisplays.Add(myLargeDisplays);
        _ftcSineNumbers.Add(mySineNumbers);
        _ftcGearColors.Add(myGearColors);
        _ftcRuleColors.Add(myRuleColors);

        yield return WaitForUnignoredModules;

        var allLists = new IList[] { _ftcGearNumbers, _ftcLargeDisplays, _ftcSineNumbers, _ftcGearColors, _ftcRuleColors };
        if (allLists.Any(l => l.Count != _ftcGearColors.Count))
            throw new AbandonModuleException($"One or more of the lists of sets of information are not the same length as the others. AllGears: {_ftcGearNumbers.Count}, AllLargeDisplays: {_ftcLargeDisplays.Count}, AllSineNumbers: {_ftcSineNumbers.Count}, AllGearColors: {_ftcGearColors.Count}, AllRuleColors: {_ftcRuleColors.Count}");

        if (myGearColors.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        foreach (var list in allLists)
            for (var ix = 0; ix < list.Count - 1; ix++)
                if ((list[ix] as IList).Count != (list[ix + 1] as IList).Count)
                    throw new AbandonModuleException("One or more of the lists of sets of information have different lengths across modules.");

        if (!new[] { myLargeDisplays.Count, mySineNumbers.Count, myGearColors.Count, myRuleColors.Count }.All(x => x == myGearNumbers.Count))
            throw new AbandonModuleException($"One or more of the lists of information for this module are not the same length as the others. Gears: {myGearNumbers.Count}, LargeDisplays: {myLargeDisplays.Count}, SineNumbers: {mySineNumbers.Count}, GearColors: {myGearColors.Count}, RuleColors: {myRuleColors.Count}");

        var colors = SForgetTheColors.GearColor.GetAnswers();
        for (var i = 0; i < myGearNumbers.Count; i++)
        {
            if (myGearNumbers[i] is < 0 or > 9)
                throw new AbandonModuleException($"‘gear[{i}]’ had an unexpected value. (Expected 0-9): {myGearNumbers[i]}");
            if (myLargeDisplays[i] is < 0 or > 990)
                throw new AbandonModuleException($"‘largeDisplay[{i}]’ had an unexpected value. (Expected 0-990): {myLargeDisplays[i]}");
            if (mySineNumbers[i] is < -99999 or > 99999)
                throw new AbandonModuleException($"‘sineNumber[{i}]’ had an unexpected value. (Expected (-99999)-99999): {mySineNumbers[i]}");
            if (!colors.Contains(myGearColors[i]))
                throw new AbandonModuleException($"‘gearColor[{i}]’ had an unexpected value. (Expected {colors.JoinString(", ")}): {mySineNumbers[i]}");
            if (!colors.Contains(myRuleColors[i]))
                throw new AbandonModuleException($"‘ruleColor[{i}]’ had an unexpected value. (Expected {colors.JoinString(", ")}): {myRuleColors[i]}");
        }

        var chosenStage = Rnd.Range(0, myGearNumbers.Count);
        string formattedName = null;

        if (_moduleCounts[moduleId] > 1)
        {
            for (var ix = 0; ix < myGearNumbers.Count; ix++)
            {
                if (ix == chosenStage)
                    continue;
                var formatCandidates = new List<string>();
                if (_ftcGearNumbers.Count(l => l[ix] == myGearNumbers[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(SForgetTheColors.GearNumber, "the Forget The Colors whose gear number was {0} in stage {1}"), myGearNumbers[ix], ix));
                if (_ftcLargeDisplays.Count(l => l[ix] == myLargeDisplays[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(SForgetTheColors.GearNumber, "the Forget The Colors which had {0} on its large display in stage {1}"), myLargeDisplays[ix], ix));
                if (_ftcSineNumbers.Count(l => l[ix] == mySineNumbers[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(SForgetTheColors.GearNumber, "the Forget The Colors whose received sine number in stage {1} ended with a {0}"), Mathf.Abs(mySineNumbers[ix]) % 10, ix));
                if (_ftcGearColors.Count(l => l[ix] == myGearColors[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(SForgetTheColors.GearNumber, "the Forget The Colors whose gear color was {0} in stage {1}"), translateAnswer(SForgetTheColors.GearColor, myGearColors[ix]), ix));
                if (_ftcRuleColors.Count(l => l[ix] == myRuleColors[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(SForgetTheColors.GearNumber, "the Forget The Colors whose rule color was {0} in stage {1}"), translateAnswer(SForgetTheColors.RuleColor, myRuleColors[ix]), ix));
                if (formatCandidates.Count > 0)
                {
                    formattedName = formatCandidates.PickRandom();
                    break;
                }
            }
            if (formattedName == null)
                yield return legitimatelyNoQuestion(module, $"There are not enough stages at which this one (#{GetIntField(comp, "_moduleId").Get()}) is unique.");
        }
        formattedName ??= _translation?.Translate(SForgetTheColors.GearNumber).ModuleName ?? "Forget The Colors";

        var stage = chosenStage.ToString();
        yield return question(SForgetTheColors.GearNumber, args: [stage]).Answers(myGearNumbers[chosenStage].ToString());
        yield return question(SForgetTheColors.LargeDisplay, args: [stage]).Answers(myLargeDisplays[chosenStage].ToString());
        yield return question(SForgetTheColors.SineNumber, args: [stage]).Answers((Mathf.Abs(mySineNumbers[chosenStage]) % 10).ToString());
        yield return question(SForgetTheColors.GearColor, args: [stage]).Answers(myGearColors[chosenStage].ToString());
        yield return question(SForgetTheColors.RuleColor, args: [stage]).Answers(myRuleColors[chosenStage].ToString());
    }
}
