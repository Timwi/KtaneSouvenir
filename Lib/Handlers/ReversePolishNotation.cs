using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SReversePolishNotation
{
    [SouvenirQuestion("What character was used in the {1} round of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G0-9")]
    Character
}

public partial class SouvenirModule
{
    [SouvenirHandler("revPolNot", "Reverse Polish Notation", typeof(SReversePolishNotation), "shortc1rcuit")]
    private IEnumerator<SouvenirInstruction> ProcessReversePolishNotation(ModuleData module)
    {
        var comp = GetComponent(module, "ReversePolishNotation");
        yield return WaitForSolve;

        var usedChars = GetArrayField<string[]>(comp, "usedChars")
            .Get(expectedLength: 3, validator: x => x.Any(character => !Regex.IsMatch(character, @"^[0-9A-G]$")) ? "expected character to be in the range of 0-9 or A-G" : null);

        var qs = new List<QandA>();
        for (var i = 0; i < 3; i++)
        {
            if (usedChars[i].Length != i + 3)
                throw new AbandonModuleException($"‘usedChars[{i}]’ is of an unexpected length (expected {i + 3}): [{string.Join(", ", usedChars[i])}]");
            qs.Add(makeQuestion(Question.ReversePolishNotationCharacter, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: usedChars[i]));
        }
        addQuestions(module, qs);
    }
}