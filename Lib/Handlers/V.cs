using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SV
{
    [SouvenirQuestion("Which word {1} shown in {0}?", OneColumn4Answers, Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true], ExampleAnswers = ["Vacant", "Valorous", "Volition", "Vermin", "Vanity", "Visage", "Voracious", "Veers", "Vengeance", "Violation", "Vigilant", "Veteran", "Vanguarding", "Villain"])]
    Words
}

public partial class SouvenirModule
{
    [SouvenirHandler("V", "V", typeof(SV), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessV(ModuleData module)
    {
        var comp = GetComponent(module, "qkV");

        yield return WaitForSolve;

        var allWords = GetArrayField<string>(comp, "allWords").Get();
        var currentWords = GetField<List<string>>(comp, "currentWords").Get();

        yield return question(SV.Words, args: ["was"]).Answers(currentWords.ToArray(), preferredWrong: allWords);
        yield return question(SV.Words, args: ["was not"]).Answers(allWords.Where(a => !currentWords.Contains(a)).ToArray(), preferredWrong: allWords);
    }
}