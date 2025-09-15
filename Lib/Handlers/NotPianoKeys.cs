using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotPianoKeys
{
    [SouvenirQuestion("What was the first displayed symbol on {0}?", TwoColumns4Answers, "b", "n", "#", "", Type = AnswerType.PianoKeysFont)]
    FirstSymbol,

    [SouvenirQuestion("What was the second displayed symbol on {0}?", ThreeColumns6Answers, "c", "C", "^", "v", ">", "", "%", "\"", "*", Type = AnswerType.PianoKeysFont)]
    SecondSymbol,

    [SouvenirQuestion("What was the third displayed symbol on {0}?", ThreeColumns6Answers, "U", "T", "m", "w", "", "B", "x", "", "", Type = AnswerType.PianoKeysFont)]
    ThirdSymbol
}

public partial class SouvenirModule
{
    [SouvenirHandler("notPianoKeys", "Not Piano Keys", typeof(SNotPianoKeys), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessNotPianoKeys(ModuleData module)
    {
        var comp = GetComponent(module, "NotPianoKeysScript");
        yield return WaitForSolve;

        var symbols = GetField<Array>(comp, "displayedSymbols").Get(arr => arr.Length == 3 ? null : "expected length 3");
        var propName = GetProperty<char>(symbols.GetValue(0), "symbol", true);

        addQuestions(module,
            makeQuestion(Question.NotPianoKeysFirstSymbol, module, correctAnswers: new[] { propName.GetFrom(symbols.GetValue(0)).ToString() }),
            makeQuestion(Question.NotPianoKeysSecondSymbol, module, correctAnswers: new[] { propName.GetFrom(symbols.GetValue(1)).ToString() }),
            makeQuestion(Question.NotPianoKeysThirdSymbol, module, correctAnswers: new[] { propName.GetFrom(symbols.GetValue(2)).ToString() }));
    }
}