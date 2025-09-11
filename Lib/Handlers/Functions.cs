using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFunctions
{
    [SouvenirQuestion("What was the last digit of your first query’s result in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    LastDigit,
    
    [SouvenirQuestion("What number was to the left of the displayed letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 999)]
    LeftNumber,
    
    [SouvenirQuestion("What letter was displayed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter,
    
    [SouvenirQuestion("What number was to the right of the displayed letter in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 999)]
    RightNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("qFunctions", "Functions", typeof(SFunctions), "JerryEris")]
    private IEnumerator<SouvenirInstruction> ProcessFunctions(ModuleData module)
    {
        var comp = GetComponent(module, "qFunctions");
        yield return WaitForSolve;

        var lastDigit = GetIntField(comp, "firstLastDigit").Get(-1, 9);
        if (lastDigit == -1)
            yield return legitimatelyNoQuestion(module, "It was solved with no queries! This isn’t a bug, just impressive (or cheating).");

        var lNum = GetIntField(comp, "numberA").Get(1, 999);
        var rNum = GetIntField(comp, "numberB").Get(1, 999);
        var theLetter = GetField<string>(comp, "ruleLetter").Get(s => s.Length != 1 ? "expected length 1" : null);

        addQuestions(module,
            makeQuestion(Question.FunctionsLastDigit, module, correctAnswers: new[] { lastDigit.ToString() }),
            makeQuestion(Question.FunctionsLeftNumber, module, correctAnswers: new[] { lNum.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray()),
            makeQuestion(Question.FunctionsLetter, module, correctAnswers: new[] { theLetter }),
            makeQuestion(Question.FunctionsRightNumber, module, correctAnswers: new[] { rNum.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray()));
    }
}