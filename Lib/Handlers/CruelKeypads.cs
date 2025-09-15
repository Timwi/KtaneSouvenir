using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCruelKeypads
{
    [SouvenirQuestion("What was the color of the bar in the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "White", Arguments = [QandA.Ordinal], TranslateAnswers = true, ArgumentGroupSize = 1)]
    Colors,

    [SouvenirQuestion("Which of these characters appeared in the {1} stage of {0}?", ThreeColumns6Answers, "ㄹ", "ㅁ", "ㅂ", "ㄱ", "ㄲ", "ㄷ", "ㅈ", "ㅉ", "ㅟ", "ㅋ", "ㅌ", "ㅍ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅢ", "ㄴ", "ㄸ", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayedSymbols
}

public partial class SouvenirModule
{
    [SouvenirHandler("CruelKeypads", "Cruel Keypads", typeof(SCruelKeypads), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessCruelKeypads(ModuleData module)
    {
        var comp = GetComponent(module, "CruelKeypadScript");

        yield return WaitForSolve;

        var firstTwoColors = GetField<Array>(comp, "StageColor").Get(arr => arr.Length != 2 ? "expected length 2" : null);
        var colors = new string[]
        {
            firstTwoColors.GetValue(0).ToString(),
            firstTwoColors.GetValue(1).ToString(),
            GetField<Enum>(comp, "stripColor").Get().ToString()
        };
        var fieldNames = new[] { "Stage1Symbols", "Stage2Symbols", "pickedSymbols" };
        // Unfortunately, these are stored as IList<char> types instead of just List<char>, so we can't use GetListField.
        var displayedSymbolSets = fieldNames.Select(name => GetField<IList<char>>(comp, name).Get(list => list.Count != 4 ? "expected length 4" : null).Select(c => c.ToString()).ToArray()).ToArray();

        var qs = new List<QandA>();
        for (var stage = 0; stage < 3; stage++)
        {
            var stageNum = Ordinal(stage + 1);
            qs.Add(makeQuestion(Question.CruelKeypadsColors, module, formatArgs: new[] { stageNum }, correctAnswers: new[] { colors[stage] }));
            qs.Add(makeQuestion(Question.CruelKeypadsDisplayedSymbols, module, formatArgs: new[] { stageNum }, correctAnswers: displayedSymbolSets[stage]));
        }
        addQuestions(module, qs);
    }
}