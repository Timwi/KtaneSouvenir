using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S100LevelsOfDefusal
{
    [SouvenirQuestion("What was the {1} displayed letter in {0}?", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Letters
}

public partial class SouvenirModule
{
    [SouvenirHandler("100LevelsOfDefusal", "100 Levels of Defusal", typeof(S100LevelsOfDefusal), "Espik")]
    private IEnumerator<SouvenirInstruction> Process100LevelsOfDefusal(ModuleData module)
    {
        var comp = GetComponent(module, "OneHundredLevelsOfDefusal");

        yield return WaitForSolve;

        var display = GetArrayField<char>(comp, "displayedLetters").Get(expectedLength: 12);

        addQuestions(module, display.Where(c => c != '.').Select((ans, i) =>
            makeQuestion(Question._100LevelsOfDefusalLetters, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { ans.ToString() })));
    }
}