using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SV
{
    [Question("Which word {1} shown in {0}?", OneColumn4Answers, Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true], ExampleAnswers = ["Vacant", "Valorous", "Volition", "Vermin", "Vanity", "Visage", "Voracious", "Veers", "Vengeance", "Violation", "Vigilant", "Veteran", "Vanguarding", "Villain"])]
    QWords,

    [Discriminator("the V that had the word {0} on it", Arguments = ["Vacant", "Valorous", "Volition", "Vermin", "Vanity", "Visage", "Voracious", "Veers", "Vengeance", "Violation", "Vigilant", "Veteran", "Vanguarding", "Villain"], ArgumentGroupSize = 1)]
    DWords
}

public partial class SouvenirModule
{
    [Handler("V", "V", typeof(SV), "BigCrunch22")]
    [ManualQuestion("Which words were shown?")]
    private IEnumerator<SouvenirInstruction> ProcessV(ModuleData module)
    {
        var comp = GetComponent(module, "qkV");

        yield return WaitForSolve;

        // If none of the rules apply, don't ask a question
        if (!Enumerable.Range(1, 5).Any(i => GetField<bool>(comp, $"rule{i}").Get()))
            yield return legitimatelyNoQuestion(module, "None of the rules applied.");

        var allWords = GetArrayField<string>(comp, "allWords").Get();
        var currentWords = GetListField<string>(comp, "currentWords").Get(expectedLength: 6);

        yield return question(SV.QWords, args: ["was"]).Answers(currentWords.ToArray(), preferredWrong: allWords);
        yield return question(SV.QWords, args: ["was not"]).Answers(allWords.Where(a => !currentWords.Contains(a)).ToArray(), preferredWrong: allWords);
        foreach (var word in currentWords)
            yield return new Discriminator(SV.DWords, $"V-{word}", args: [word], avoidAnswers: [word]);
    }
}
