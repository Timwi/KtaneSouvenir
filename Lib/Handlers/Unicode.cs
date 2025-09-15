using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUnicode
{
    [SouvenirQuestion("What was the {1} submitted code in {0}?", ThreeColumns6Answers, "00A7", "00B6", "0126", "04D4", "017F", "01F6", "01F7", "2042", "037C", "03C2", "040B", "20AA", "042E", "0460", "046C", "20B0", "222F", "222B", "2569", "04EC", "260A", "04A6", "2626", "FB21", "0428", "03A9", "0583", "2592", "254B", "2318", "2234", "2205", "2104", "04A8", "2605", "019B", "03EA", "062A", "067C", "063A", "06BA", "00FE", "0194", "0239", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    SortedAnswer
}

public partial class SouvenirModule
{
    [SouvenirHandler("UnicodeModule", "Unicode", typeof(SUnicode), "Marksam")]
    private IEnumerator<SouvenirInstruction> ProcessUnicode(ModuleData module)
    {
        var comp = GetComponent(module, "UnicodeScript");
        yield return WaitForSolve;

        PropertyInfo<string> propCode = null;
        var symbols = GetField<IEnumerable>(comp, "SelectedSymbols").Get().Cast<object>().Select(x => (propCode ??= GetProperty<string>(x, "Code", isPublic: true)).GetFrom(x)).ToList();

        if (symbols.Count != 4)
            throw new AbandonModuleException($"‘SelectedSymbols’ has unexpected length {symbols.Count} (expected 4).");

        addQuestions(module,
            makeQuestion(Question.UnicodeSortedAnswer, module, formatArgs: new[] { "first" }, correctAnswers: new[] { symbols[0] }),
            makeQuestion(Question.UnicodeSortedAnswer, module, formatArgs: new[] { "second" }, correctAnswers: new[] { symbols[1] }),
            makeQuestion(Question.UnicodeSortedAnswer, module, formatArgs: new[] { "third" }, correctAnswers: new[] { symbols[2] }),
            makeQuestion(Question.UnicodeSortedAnswer, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { symbols[3] }));
    }
}