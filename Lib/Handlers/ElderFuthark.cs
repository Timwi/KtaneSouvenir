using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SElderFuthark
{
    [SouvenirQuestion("What was the {1} rune shown on {0}?", TwoColumns4Answers, "Algiz", "Ansuz", "Berkana", "Dagaz", "Ehwaz", "Eihwaz", "Fehu", "Gebo", "Hagalaz", "Isa", "Jera", "Kenaz", "Laguz", "Mannaz", "Nauthiz", "Othila", "Perthro", "Raido", "Sowulo", "Teiwaz", "Thurisaz", "Uruz", "Wunjo", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Runes
}

public partial class SouvenirModule
{
    [SouvenirHandler("elderFuthark", "Elder Futhark", typeof(SElderFuthark), "Goofy")]
    private IEnumerator<SouvenirInstruction> ProcessElderFuthark(ModuleData module)
    {
        var comp = GetComponent(module, "ElderFutharkScript");
        yield return WaitForSolve;

        var pickedRuneNames = GetArrayField<string>(comp, "pickedRuneNames").Get(expectedLength: 3);

        addQuestions(module,
            makeQuestion(Question.ElderFutharkRunes, module, correctAnswers: new[] { pickedRuneNames[0] }, formatArgs: new[] { "first" }, preferredWrongAnswers: pickedRuneNames),
            makeQuestion(Question.ElderFutharkRunes, module, correctAnswers: new[] { pickedRuneNames[1] }, formatArgs: new[] { "second" }, preferredWrongAnswers: pickedRuneNames),
            makeQuestion(Question.ElderFutharkRunes, module, correctAnswers: new[] { pickedRuneNames[2] }, formatArgs: new[] { "third" }, preferredWrongAnswers: pickedRuneNames));
    }
}