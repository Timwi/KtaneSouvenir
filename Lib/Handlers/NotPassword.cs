using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNotPassword
{
    [SouvenirQuestion("Which letter was missing from {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotPassword", "Not Password", typeof(SNotPassword), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessNotPassword(ModuleData module)
    {
        var comp = GetComponent(module, "NotPassword");
        yield return WaitForSolve;

        var connector = GetProperty<object>(comp, "Connector").Get();
        var spinners = GetField<IEnumerable>(connector, "spinners").Get().Cast<object>().ToArray();
        var options = GetListField<char>(spinners[0], "Options", isPublic: true);
        foreach (var spinner in spinners)
            options.SetTo(spinner, null);

        var letter = GetField<char>(comp, "MissingLetter", isPublic: true).Get(c => c is < 'A' or > 'Z' ? $"Bad letter {c}" : null);
        addQuestion(module, Question.NotPasswordLetter, correctAnswers: new[] { letter.ToString() });
    }
}